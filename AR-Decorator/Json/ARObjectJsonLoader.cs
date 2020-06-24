using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectJsonLoader : MonoBehaviour
{
    //Классы
    [System.Serializable]
    public class ArObject
    {
        public int id;
        public string title;
        public string description;
        public int marker_id;
        public bool is_geo;
        public GeoCords geo_coords;
        public ObjectSettings object_settings;
        public Transform transform;
        public ObjectType object_type;
        public ObjectPath object_path;
    }

    [System.Serializable]
    public class ArObjectList
    {
        public List<ArObject> data = new List<ArObject>();
    }

    [System.Serializable]
    public class GeoCords
    {
        public int latitude;
        public int longitude;
    }

    [System.Serializable]
    public class ObjectType
    {
        public int value;
        public string key;
        public string description;
    }
    [System.Serializable]
    public class ObjectSettings 
    {
        public string color;
    }



    //Позиционирование
    [System.Serializable]
    public class Transform
    {
        public Position position;
        public Rotation rotation;
        public Scale scale;
    }

    [System.Serializable]
    public class Position 
    { 
        public int x;  public int y;  public int z;
    }

    [System.Serializable]
    public class Rotation
    {
        public int x; public int y; public int z;
    }

    [System.Serializable]
    public class Scale
    {
        public int x; public int y; public int z;
    }


    [System.Serializable]
    public class ObjectPath
    {
        public string name;
        public string url;
    }
}
