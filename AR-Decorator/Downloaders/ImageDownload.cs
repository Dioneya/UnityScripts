using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine.Timeline;
using System;
using System.IO;
public class ImageDownload : MonoBehaviour
{

    string link = "http://likholetov.beget.tech";
    //public GameObject stickerContent;
    private ChoseDownloaderScript choseDownloader;
    private List<MarkerJsonLoader.Marker> markerInfo;
    int cnt = 0;
    void Start()
    {
        choseDownloader = GetComponent<ChoseDownloaderScript>();
        cnt = 0;
        if (GlobalVariables.institution != null) 
        {
            markerInfo = GlobalVariables.institution.data.markers;
            Repeat();
        }
        
    }

    private void Repeat() 
    {
        if (markerInfo!=null) 
        {
            if (cnt < markerInfo.Count)
            {
                string markerURL = markerInfo[cnt].image_set[0].url;
                Debug.LogWarning(markerURL);
                CreateImageTargetFromDownloadedTexture();
            }
        }
    }

    void SetToParent() 
    {
        GameObject obj = GameObject.Find(Convert.ToString(markerInfo[cnt].id)); //Найдём объект по id маркера
        obj.transform.parent = this.transform;
    }
    void CreateImageTargetFromDownloadedTexture()
    {

        string name = Convert.ToString(markerInfo[cnt].id); // Имя маркера будет являть id 
        string path_marker = Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "marker_" + Convert.ToString(markerInfo[cnt].id)); //путь для папки кеширования маркерам
        string fileName = Convert.ToString(markerInfo[cnt].id);

        var cacheFilePath = Path.Combine(Application.persistentDataPath, @"Cache", path_marker, fileName + ".png");

        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        // Get downloaded texture once the web request completes
        var texture = new Texture2D(4,4);
        texture.LoadImage(File.ReadAllBytes(cacheFilePath));

        // get the runtime image source and set the texture
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        runtimeImageSource.SetImage(texture, 1.5f, name);

        // create a new dataset and use the source to create a new trackable
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, name);

        // add the DefaultTrackableEventHandler to the newly created game object
        trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
        trackableBehaviour.gameObject.AddComponent<BoxCollider>();
        // activate the dataset
        objectTracker.ActivateDataSet(dataset);
        SetToParent();
        // TODO: add virtual content as child object(s)
        choseDownloader.SelectDownload(cnt, ref trackableBehaviour); //Запустим скрипт по выбору сценария загрузки объекта для триггера
        

        cnt++;
        Repeat();
    }
}
