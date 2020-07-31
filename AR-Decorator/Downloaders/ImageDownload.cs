using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System;
using System.IO;
public class ImageDownload : MonoBehaviour
{
    private ChoseDownloaderScript choseDownloader;
    private List<MarkerJsonLoader.Marker> markerInfo;
    void Start()
    {
        choseDownloader = GetComponent<ChoseDownloaderScript>();
        if (GlobalVariables.institution != null) 
        {
            markerInfo = GlobalVariables.institution.data.markers;

            //Пройдёмся по циклу чтобы выгрузить маркеры
            for (int i = 0; i < markerInfo.Count; i++) 
            {
                CreateImageTargetFromDownloadedTexture(i); //Выгружаем маркер по id
            }
        }
    }

    void SetToParent(int index,int i) 
    {
        GameObject obj = GameObject.Find(Convert.ToString(markerInfo[index].id)+"_"+i); //Найдём объект по id маркера
        obj.transform.parent = this.transform;
    }
    void CreateImageTargetFromDownloadedTexture(int index)
    {
        for (int i = 0; i < markerInfo[index].image_set.Count; i++) 
        {
             
            string name = Convert.ToString(markerInfo[index].id)+"_"+i; // Имя маркера будет являть id 

            var cacheFilePath = GlobalVariables.CacheFilePath(index, GlobalVariables.Marker, Convert.ToString(i));
            Debug.Log(cacheFilePath);

            var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

            #region Получение и создание текстуры
            // Get downloaded texture once the web request completes
            var texture = new Texture2D(4, 4);
            texture.LoadImage(File.ReadAllBytes(cacheFilePath));
            #endregion

            #region Получние Runtime изображние и назначение текстуры 
            // get the runtime image source and set the texture
            var runtimeImageSource = objectTracker.RuntimeImageSource;
            runtimeImageSource.SetImage(texture, 1.5f, name);
            #endregion

            #region Создание нового DataSet и использование для создания новго триггера
            // create a new dataset and use the source to create a new trackable
            var dataset = objectTracker.CreateDataSet();
            var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, name);
            #endregion

            #region Добавление компонентов trackableBehaviour
            // add the DefaultTrackableEventHandler to the newly created game object
            trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
            trackableBehaviour.gameObject.AddComponent<BoxCollider>();
            #endregion

            // activate the dataset
            objectTracker.ActivateDataSet(dataset);

            SetToParent(index,i); //Указание родителя Камере

            choseDownloader.SelectDownload(index, ref trackableBehaviour); //Запустим скрипт по выбору сценария загрузки объекта для триггера

        }
    }
}
