using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine.Timeline;
using System;
public class ImageDownload : MonoBehaviour
{
    private ChoseDownloaderScript choseDownloader;
    private List<MarkerJsonLoader.Marker> markerInfo;
    int cnt = 0;
    void Start()
    {
        choseDownloader = GetComponent<ChoseDownloaderScript>();
    }

    public void StartCreate() 
    {
        cnt = 0;
        markerInfo = GetComponent<JsonLoader>().institution.data.markers;
        Repeat();
    }

    private void Repeat() 
    {
        if (cnt<markerInfo.Count) 
        {
            string markerURL = markerInfo[cnt].image_set[0].url;
            Debug.LogWarning(markerURL);
            StartCoroutine(CreateImageTargetFromDownloadedTexture(markerURL));
        }
        
    }

    void SetToParent() 
    {
        GameObject obj = GameObject.Find(Convert.ToString(markerInfo[cnt].id)); //Найдём объект по id маркера
        obj.transform.parent = this.transform;
    }
    IEnumerator CreateImageTargetFromDownloadedTexture(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {

                string name = Convert.ToString(markerInfo[cnt].id); // Имя маркера будет являть id 


                var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

                // Get downloaded texture once the web request completes
                var texture = DownloadHandlerTexture.GetContent(uwr);

                // get the runtime image source and set the texture
                var runtimeImageSource = objectTracker.RuntimeImageSource;
                runtimeImageSource.SetImage(texture, 1.5f, name);

                // create a new dataset and use the source to create a new trackable
                var dataset = objectTracker.CreateDataSet();
                var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, name);

                // add the DefaultTrackableEventHandler to the newly created game object
                trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();

                // activate the dataset
                objectTracker.ActivateDataSet(dataset);
                SetToParent();
                // TODO: add virtual content as child object(s)
                choseDownloader.SelectDownload(cnt, trackableBehaviour); //Запустим скрипт по выбору сценария загрузки объекта для триггера
                

                
                cnt++;
                Repeat();
            }
        }
    }
}
