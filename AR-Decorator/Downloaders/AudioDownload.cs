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

    /*IEnumerator LoadAudio() 
    {
        WWW www = new WWW(url);
        AudioClip audio = www.GetAudioClip(false,true, AudioType.MPEG);
        if (audio==null || audio.loadState==AudioDataLoadState.Unloaded) 
        {
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.clip = audio;
    }*/

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
