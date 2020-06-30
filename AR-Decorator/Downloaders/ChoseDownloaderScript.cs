using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Vuforia;
public class ChoseDownloaderScript : MonoBehaviour
{
    JsonLoader jsonLoader;
    ImageDownload marker;
    string link = "http://likholetov.beget.tech";

    public void SelectDownload(int indexOfMarker,ref DataSetTrackableBehaviour trackableBehaviour) 
    {
        
        jsonLoader = GetComponent<JsonLoader>();
        marker = GetComponent<ImageDownload>();
        var obj_path = jsonLoader.institution.data.markers[indexOfMarker].a_r_object.object_path;

        const int Image = 0;
        const int Video = 1;
        const int Model = 2;
        const int Audio = 3;
        const int AssetBundle = 4;
        const int Text = 5;

        int key = jsonLoader.institution.data.markers[indexOfMarker].a_r_object.object_type.value;

        switch (key)
        {
            case Image:
                {

                    var image = trackableBehaviour.gameObject.AddComponent<ImgDownload>();
                    image.url = link + obj_path.url;
                    image.StartDownload();
                    break;
                }

            case Video:
                {
                    trackableBehaviour.gameObject.AddComponent<VideoTracker>();
                    var video = trackableBehaviour.gameObject.AddComponent<VideoDownload>();
                    video.videoURL = link + obj_path.url;
                    video.StartDownload();
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
                    audio.url = link + obj_path.url;
                    audio.StartDownload();
                    break;
                }

            case AssetBundle:
                {
                    //trackableBehaviour.gameObject.AddComponent<AssetDownload>(); // создание компонента скрипта для этого маркера
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(trackableBehaviour.gameObject.transform);
                    break;
                    /*var model = trackableBehaviour.gameObject.GetComponent<AssetDownload>();

                    var object_path = jsonLoader.institution.data.markers[indexOfMarker].a_r_object.object_path;

                    model.nameObj = object_path.name;
                    model.url = object_path.url;
                    model.StartLoad();*/
                }
            case Text:
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(trackableBehaviour.gameObject.transform);
                    break;
                }
        }

    }
}
