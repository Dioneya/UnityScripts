using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDownload : MonoBehaviour
{
    public string url;
    public string nameObj="";
    GameObject objChild;
    void Start()
    {
        objChild = GameObject.Find(nameObj);
    }

    public void StartLoad() 
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
            GameObject obj = (GameObject)bundle.LoadAsset(nameObj);
            obj.GetComponent<Transform>().localScale = new Vector3(0.1f,0.1f,0.1f);
            Instantiate(obj, this.transform);
        }
        else 
        {
            UnityEngine.Debug.Log("Ошибка Asset");
        }
    }
}
