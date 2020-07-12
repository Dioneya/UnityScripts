using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AssetDownload : MonoBehaviour
{
    public string url;
    public string nameObj="";
    void Start()
    {
    }

    public void StartLoad() 
    {
        StartCoroutine(LoadAssetURL());
        
    }
    IEnumerator LoadAssetURL() 
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        //Send Request and wait
        yield return request.SendWebRequest();
        AssetBundle bundle = (DownloadHandlerAssetBundle.GetContent(request));
        if (request.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset(nameObj);
            obj.GetComponent<Transform>().localScale = new Vector3(1f,1f,1f);
            GameObject asset = Instantiate(obj, this.transform);
            var tracker = gameObject.AddComponent<AssetTracker>();
            tracker.model = asset;
        }
        else 
        {
            UnityEngine.Debug.Log(request.error);
        }
    }

}
