using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;
using System.IO;
using System;

public class VideoDownload : MonoBehaviour
{
    public VideoPlayer video;
    public string videoURL;
    private TrackableBehaviour mTrackableBehaviour;

    public void StartDownload(int index) 
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        video = quad.AddComponent<VideoPlayer>();
        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.MaterialOverride;

        string path_ar_object = Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "marker_" + Convert.ToString(GlobalVariables.institution.data.markers[index].id), "arObject_" + GlobalVariables.institution.data.markers[index].a_r_object.id); // путь для папки кеширования объектам
        string fileName = Convert.ToString(GlobalVariables.institution.data.markers[index].a_r_object.id);
        var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", path_ar_object, fileName + ".mp4");

        video.url = cacheFilePath;
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