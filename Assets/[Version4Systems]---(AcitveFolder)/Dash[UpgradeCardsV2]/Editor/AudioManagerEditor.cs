#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioManager myScript = (AudioManager)target;
        if (GUILayout.Button("Load Audio Clips"))
        {
            string relativePath = "Assets/Audio";
            string absolutePath = Path.Combine(Application.dataPath, "Audio");
            if (Directory.Exists(absolutePath))
            {
                var audioFileTypes = new string[] { "*.wav", "*.mp3" };
                foreach (var fileType in audioFileTypes)
                {
                    var audioClipFiles = Directory.GetFiles(absolutePath, fileType);
                    foreach (var audioFilePath in audioClipFiles)
                    {
                        var audioFileName = Path.GetFileNameWithoutExtension(audioFilePath);
                        var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine(relativePath, audioFileName + Path.GetExtension(audioFilePath)));
                        if (audioClip != null)
                        {
                            var existingClipIndex = myScript.audioClipDataList.FindIndex(a => a.soundName == audioFileName);
                            if (existingClipIndex != -1)
                            {
                                // Replace the existing one
                                var newAudioClipData = new AudioManager.AudioClipData
                                {
                                    soundName = audioFileName,
                                    audioClip = audioClip
                                };
                                myScript.audioClipDataList[existingClipIndex] = newAudioClipData;
                            }
                            else
                            {
                                // Add as a new entry
                                myScript.audioClipDataList.Add(new AudioManager.AudioClipData
                                {
                                    soundName = audioFileName,
                                    audioClip = audioClip
                                });
                            }
                        }
                    }
                }
                EditorUtility.SetDirty(myScript);
                AssetDatabase.SaveAssets();
                Debug.Log("Loaded audio clips from 'Audio' directory into AudioManager.");
            }
            else
            {
                Debug.LogError("Directory 'Audio' not found in the Assets folder.");
            }
        }
    }
}
#endif