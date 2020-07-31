using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimatedAssetBundle : MonoBehaviour
{
    #region Публичные переменные, которые хранят параметры объекта
    public string url;
    public string nameObj = "";
    public bool isTest = false;

    public string action_link;
    public ARObjectJsonLoader.Transform transform_obj;
    public GameObject mask; 
    #endregion
    void Start()
    {
        mask = Resources.Load<GameObject>("Prefabs/Mask");
        Instantiate(mask, this.transform);
        mask.transform.localPosition = new Vector3(-0.44f, -1.27f, -0.017f);
        mask.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
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

            GameObject asset = Instantiate(obj, this.transform);
            GetComponent<ResetPosition>().model = asset;
            #region Позиционированние объекта
            if (transform_obj.scale.x == 0 || transform_obj.scale.y == 0 || transform_obj.scale.z == 0)
            {
                asset.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            else
            {
                asset.GetComponent<Transform>().localScale = new Vector3(transform_obj.scale.x, transform_obj.scale.y, transform_obj.scale.z);
            }

            asset.GetComponent<Transform>().localPosition = new Vector3(transform_obj.position.x, transform_obj.position.y, transform_obj.position.z);
            #endregion

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
