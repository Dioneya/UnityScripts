using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
public class ImgDownload : MonoBehaviour
{
    public string url;

    public void StartDownload(int index)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;

        string path_ar_object = Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "marker_" + Convert.ToString(GlobalVariables.institution.data.markers[index].id), "arObject_" + GlobalVariables.institution.data.markers[index].a_r_object.id); // путь для папки кеширования объектам
        string fileName = Convert.ToString(GlobalVariables.institution.data.markers[index].a_r_object.id);
        var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", path_ar_object, fileName + ".png");

        Texture2D texture = new Texture2D(4, 4);
        texture.LoadImage(File.ReadAllBytes(cacheFilePath));

        quad.GetComponent<Renderer>().material.mainTexture = texture;
    }

}
