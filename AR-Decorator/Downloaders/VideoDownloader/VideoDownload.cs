using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;


public class VideoDownload : MonoBehaviour
{
    public VideoPlayer video;
    public string videoURL;
    private TrackableBehaviour mTrackableBehaviour;

    public void StartDownload() 
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        video = quad.AddComponent<VideoPlayer>();
        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.MaterialOverride;
        video.url = videoURL;
    }

    public void StartPlayVideo() 
    {
        video.Play();
    }
    public void StopPlayVideo() 
    {
        video.Stop();
    }
}