using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDownload : MonoBehaviour
{
    public string url;
    void Start()
    {
        WWW www = new WWW(url);
        StartCoroutine(LoadAssetURL(www));
    }

    IEnumerator LoadAssetURL(WWW www) 
    {
        yield return www;
        AssetBundle bundle = www.assetBundle;
        if (www.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset("revista");
            obj.GetComponent<Transform>().localScale = new Vector3(0.1f,0.1f,0.1f);
            Instantiate(obj);

        }
        else 
        {
            UnityEngine.Debug.Log("Ошибка Asset");
        }
    }
}
