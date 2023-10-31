using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musicTracks;
    AudioSource audioSource;
    int currentTrack = 0;
    [SerializeField] bool playOnStart;
    [SerializeField] float lerpFPS = 30f;
    [SerializeField] float lerpDuration = 2f;
    float defaultVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = musicTracks[0];
        defaultVolume = audioSource.volume;
    }

    private void Start()
    {
        if (playOnStart)
            PlayMusic();
    }
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }

    IEnumerator LerpMusicVolume(float lowestVolume = 0f, float delay = 0f)
    {
        StartCoroutine(FadeRoutine(false, lowestVolume, delay));
        yield return new WaitForSeconds(lerpDuration);
        audioSource.Stop();
        if (currentTrack < musicTracks.Length)
            audioSource.clip = musicTracks[currentTrack];
        audioSource.Play();
        StartCoroutine(FadeRoutine(true,lowestVolume,delay));
    }

    public void SwitchToNextTrack()
    {
        currentTrack++;
        if (currentTrack < musicTracks.Length)
        {
            StopAllCoroutines();
            StartCoroutine(LerpMusicVolume());
        }
    }

    public void SetMusicVolume(float newVolume, bool updateDefault = false)
    {
        audioSource.volume = newVolume;
        if(updateDefault)
            defaultVolume = newVolume;
    }

    IEnumerator FadeRoutine(bool fadeIn = true, float lowestVolume = 0f, float delay = 0f)
    {
        if(delay>0)
            yield return new WaitForSeconds(delay);

        float currentTime = 0f;
        float timePerFrame = 1 / lerpFPS;
        float currentVolume = 0;
        if (!fadeIn)
            currentVolume = defaultVolume;

        while (currentTime < lerpDuration)
        {
            audioSource.volume = currentVolume;
            currentTime += timePerFrame;
            if (fadeIn)
            {
                currentVolume = Mathf.Lerp(lowestVolume, defaultVolume, currentTime / lerpDuration);
            }
            else
            {
                currentVolume = Mathf.Lerp(defaultVolume, lowestVolume, currentTime / lerpDuration);
            }

            yield return new WaitForSeconds(timePerFrame);
        }
    }
}
