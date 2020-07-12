using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AudioDownload : MonoBehaviour
{
    public string url;
    AudioSource audioSource; 
    public void StartDownload()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        StartCoroutine(LoadAudioFromServer(url,AudioType.MPEG));
    }
    IEnumerator LoadAudioFromServer(string url,
                                AudioType audioType)
    {
        var request = UnityWebRequestMultimedia.GetAudioClip(url, audioType);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            audioSource.clip=(DownloadHandlerAudioClip.GetContent(request));
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            
            
        }

        request.Dispose();
    }

    public void StartPlayAudio() 
    {
        audioSource.Play();
    }

    public void StopPlayAudio() 
    {
        audioSource.Stop();
    }
}
