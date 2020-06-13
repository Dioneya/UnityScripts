using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarkerJsonLoader : MonoBehaviour
{
    //Классы
    [System.Serializable]
    public class Marker
    {
        public int id;
        public string sticker_image;
        public int institution_id;
        public ImageSet image_set;
        public bool is_active;
        public string title;
        public string description;
        public string action_link;

    }

    [System.Serializable]
    public class MarkerList
    {
        public List<Marker> data = new List<Marker>();
    }

    [System.Serializable]
    public class ImageSet 
    {
        public List<Image> image_set = new List<Image>();
    }

    [System.Serializable]
    public class Image
    {
        public string url;
    }

    //Глобальные переменные 
    public string jsonUrl;
    public MarkerList Markers;

    //Локльные пременные
    string path;
    
    
    void Start()
    {
        StartCoroutine(LoadJson());
    }
    //Скачивание по ссылке Json
    IEnumerator LoadJson() 
    {
        WWW www = new WWW(jsonUrl);
        yield return www;
        if (www.error == null)
        {
            processJson(www.text);
            UnityEngine.Debug.Log(www.text);
        }
        else 
        {
            UnityEngine.Debug.Log("invalid url");
        }
    }

    private void processJson(string url) 
    {
        Markers = JsonUtility.FromJson<MarkerList>(url);
 
        foreach (Marker marker in Markers.data)
        {
            UnityEngine.Debug.Log("id "+ marker.id);
            UnityEngine.Debug.Log("sticker_image " + marker.sticker_image);
        }
    }


}
