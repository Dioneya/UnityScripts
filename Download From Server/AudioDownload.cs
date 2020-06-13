using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDownload : MonoBehaviour
{
    public string url;
    void Start()
    {
        StartCoroutine(LoadAudio());
    }

    IEnumerator LoadAudio() 
    {
        WWW www = new WWW(url);
        AudioClip audio = www.GetAudioClip(false,true, AudioType.WAV);
        if (audio==null || audio.loadState==AudioDataLoadState.Unloaded) 
        {
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<AudioSource>().clip = audio;
        GetComponent<AudioSource>().Play();
    }
}
