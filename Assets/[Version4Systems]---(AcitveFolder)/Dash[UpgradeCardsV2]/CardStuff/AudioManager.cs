using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class AudioClipData
    {
        public string soundName;
        public AudioClip audioClip;
        public float volume = 1f;
        public int priority = 128;
    }

    private Dictionary<string, float> lastPlayedAt = new Dictionary<string, float>();
    [SerializeField] private float minPlayInterval = 0.02f; // or whatever interval you want

    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int poolSize = 10;

    [SerializeField] public List<AudioClipData> audioClipDataList;

    private List<AudioSource> audioSourcePool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSourcePool = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource newAudioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
            newAudioSource.gameObject.SetActive(false);
            audioSourcePool.Add(newAudioSource);
        }
    }

    public void PlaySound3D(string soundName, Vector3 position)
    {
        if (lastPlayedAt.ContainsKey(soundName) && Time.time - lastPlayedAt[soundName] < minPlayInterval)
        {
            return;
        }

        lastPlayedAt[soundName] = Time.time;

        //Debug.Log("Playing sound: " + soundName);
        foreach (AudioClipData audioClipData in audioClipDataList)
        {
            if (audioClipData.soundName == soundName)
            {
                AudioSource audioSource = GetPooledAudioSource();
                if (audioSource != null)
                {
                    audioSource.gameObject.SetActive(true);
                    audioSource.clip = audioClipData.audioClip;
                    audioSource.transform.position = position;
                    audioSource.volume = audioClipData.volume;
                    audioSource.priority = audioClipData.priority;
                    audioSource.Play();

                    StartCoroutine(DisableAudioSourceWhenFinished(audioSource));
                    return;
                }
            }
        }

        Debug.LogWarning("Sound not found: " + soundName);
    }

    private AudioSource GetPooledAudioSource()
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (!audioSource.gameObject.activeInHierarchy)
            {
                return audioSource;
            }
        }

        AudioSource newAudioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSourcePool.Add(newAudioSource);

        return newAudioSource;
    }

    private IEnumerator DisableAudioSourceWhenFinished(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        audioSource.gameObject.SetActive(false);
    }
}