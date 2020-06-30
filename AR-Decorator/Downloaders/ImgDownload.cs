using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ImgDownload : MonoBehaviour
{
    public string url;

    public void StartDownload()
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        StartCoroutine(DownloadImage(url,quad));
    }


    IEnumerator DownloadImage(string MediaUrl, GameObject obj)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            obj.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
