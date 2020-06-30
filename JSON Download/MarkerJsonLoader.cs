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
        public List<Image> image_set;
        public bool is_active;
        public string title;
        public string description;
        public string action_link;
        public int position;
        public ARObjectJsonLoader.ArObject a_r_object;
    }

    [System.Serializable]
    public class MarkerList
    {
        public List<Marker> markers = new List<Marker>();
    }

    [System.Serializable]
    public class Image
    {
        public string url;
    }

    //Глобальные переменные 
    public string jsonUrl;
    public MarkerList Markers;
}
