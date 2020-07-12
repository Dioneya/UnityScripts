using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Vuforia;
public class ChoseDownloaderScript : MonoBehaviour
{
    public GameObject test;
    JsonLoader jsonLoader;
    ImageDownload marker;
    string link = "http://likholetov.beget.tech";

    public void SelectDownload(int indexOfMarker, ref DataSetTrackableBehaviour trackableBehaviour)
    {

        jsonLoader = GetComponent<JsonLoader>();
        marker = GetComponent<ImageDownload>();
        var obj_path = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path;

        const int Image = 0;
        const int Video = 1;
        const int Model = 2;
        const int Audio = 3;
        const int AssetBundle = 4;
        const int Text = 5;

        int key = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_type.value;


        var action_link = trackableBehaviour.gameObject.AddComponent<ActionLink>();
        action_link.action_link = GlobalVariables.institution.data.markers[indexOfMarker].action_link;

        switch (key)
        {
            case Image:
                {

                    var image = trackableBehaviour.gameObject.AddComponent<ImgDownload>();
                    image.url = link + obj_path.url;
                    image.StartDownload(indexOfMarker);
                    break;
                }

            case Video:
                {
                    trackableBehaviour.gameObject.AddComponent<VideoTracker>();
                    var video = trackableBehaviour.gameObject.AddComponent<VideoDownload>();
                    //video.videoURL = link + obj_path.url;
                    video.StartDownload(indexOfMarker);
                    break;
                }

            case Model:
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(trackableBehaviour.gameObject.transform);
                    break;
                }

            case Audio:
                {

                    var audio = trackableBehaviour.gameObject.AddComponent<AudioDownload>();
                    trackableBehaviour.gameObject.AddComponent<AudioTracker>();
                    string path_ar_object = Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "marker_" + Convert.ToString(GlobalVariables.institution.data.markers[indexOfMarker].id), "arObject_" + GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id); // путь для папки кеширования объектам
                    string fileName = Convert.ToString(GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id);
                    var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", path_ar_object, fileName + ".mp3");
                    audio.url = "file://" + cacheFilePath;
                    audio.StartDownload();
                    break;
                }

            case AssetBundle:
                {
                    var model = trackableBehaviour.gameObject.AddComponent<AssetDownload>();
                    
                    var object_path = Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "marker_" + Convert.ToString(GlobalVariables.institution.data.markers[indexOfMarker].id), "arObject_" + GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id);
                    string fileName = Convert.ToString(GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id);
                    var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", object_path, fileName);

                    model.nameObj = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path.name;
                    model.url = "file://" + cacheFilePath;
                    model.transform.SetParent(trackableBehaviour.gameObject.transform);
                    model.StartLoad();
                    test = trackableBehaviour.gameObject;
                    break;
                }
            case Text:
                {
                    var text = trackableBehaviour.gameObject.AddComponent<TextDownload>();
                    text.text = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_settings.text;
                    text.StartDownload();
                    break;
                }
        }

    }
}
