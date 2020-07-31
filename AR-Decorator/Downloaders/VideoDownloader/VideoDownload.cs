using UnityEngine;
using UnityEngine.Video;

public class VideoDownload : MonoBehaviour
{
    public VideoPlayer video;
    public string videoURL;
    public void StartDownload(int index) 
    {
        #region Создание и настройка места для видео
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        #endregion

        #region Добавление и настройка VideoPlayer
        video = quad.AddComponent<VideoPlayer>();
        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.MaterialOverride;
        #endregion

        var cacheFilePath = GlobalVariables.CacheFilePath(index,1);

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