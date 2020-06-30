using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StickerImage : MonoBehaviour
{
    public string url;
    List<GameObject> Images = new List<GameObject>();
    // Start is called before the first frame update
    public void StartDownload(string markerId)
    {
        //Создание и настройка стикера 
        GameObject image = new GameObject();
        image.name = "Sticker Image " + markerId;
        image.transform.SetParent(this.gameObject.transform);
        var rt = image.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200,200);
        RawImage rawImage = image.AddComponent<RawImage>();
        
        
        Images.Add(image);

        StartCoroutine(DownloadImage(url,rawImage));
    }

    IEnumerator DownloadImage(string MediaUrl, RawImage rawImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
