﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class AssetDownload : MonoBehaviour
{
    #region Публичные переменные, которые хранят параметры объекта
    public string url;
    public string nameObj="";
    public bool isTest = false;

    public string action_link;
    public ARObjectJsonLoader.Transform transform_obj;
    #endregion
    void Start()
    {
        if (isTest) // Если это для теста, то сразу запустит метод загрузки объекта
        {
            StartLoad();
        }
    }

    public void StartLoad() 
    {
        StartCoroutine(LoadAssetURL());
    }
    IEnumerator LoadAssetURL() 
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return request.SendWebRequest();

        AssetBundle bundle = (DownloadHandlerAssetBundle.GetContent(request));

        if (request.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset(nameObj);

            #region Позиционированние объекта
            if (transform_obj.scale.x == 0 || transform_obj.scale.y == 0 || transform_obj.scale.z == 0)
            {
                obj.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            else 
            {
                obj.GetComponent<Transform>().localScale = new Vector3(transform_obj.scale.x, transform_obj.scale.y, transform_obj.scale.z);
            }
                
            obj.GetComponent<Transform>().localPosition = new Vector3(transform_obj.position.x, transform_obj.position.y, transform_obj.position.z);
            #endregion

            GameObject asset = Instantiate(obj, this.transform);

            #region Создание и настройка ActionLink на объекте
            var action = asset.AddComponent<ActionLink>();
            action.action_link = action_link;
            #endregion

            #region Создание и настройка AssetTracker
            var tracker = gameObject.AddComponent<AssetTracker>();
            tracker.model = asset;
            #endregion
        }
        else 
        {
            UnityEngine.Debug.Log(request.error);
        }
    }

}
