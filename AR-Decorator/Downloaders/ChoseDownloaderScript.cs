using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class ChoseDownloaderScript : MonoBehaviour
{
    JsonLoader jsonLoader;
    ImageDownload marker;
    public void SelectDownload(int indexOfMarker, DataSetTrackableBehaviour trackableBehaviour) 
    {
        jsonLoader = GetComponent<JsonLoader>();
        marker = GetComponent<ImageDownload>();

        const int Image = 0;
        const int Video = 1;
        const int Model = 2;
        const int Audio = 3;
        const int AssetBundle = 4;

        int key = jsonLoader.institution.data.markers[indexOfMarker].a_r_object.object_type.value;

        if (key == Image)
        {

        }
        else if (key == Video) 
        {

        }

        else if (key == Model)
        {

        }
        else if (key == Audio)
        {

        }
        else if (key == AssetBundle)
        {
            trackableBehaviour.gameObject.AddComponent<AssetDownload>(); // создание компонента скрипта для этого маркера

            var model = trackableBehaviour.gameObject.GetComponent<AssetDownload>();

            var object_path = jsonLoader.institution.data.markers[indexOfMarker].a_r_object.object_path;

            model.nameObj = object_path.name;
            model.url = object_path.url;
            model.StartLoad();
        }

    }
}
