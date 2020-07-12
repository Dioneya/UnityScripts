using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;
public class StickerImage : MonoBehaviour
{
    public GameObject sticker;

    string link = "http://likholetov.beget.tech";

    
    // Start is called before the first frame update
    public void StartLoad(InstitutionJsonLoader.InstitutionList institution)
    {
        
        for (int i = 0; i < institution.data.markers.Count; i++ ) 
        {
            //Создание и настройка стикера 
            GameObject image = Instantiate(sticker);
            image.SetActive(true);
            image.name = "Sticker Image " + institution.data.markers[i].id;
            image.transform.SetParent(this.gameObject.transform);
            RawImage rawImage = image.GetComponent<RawImage>();
            image.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, image.transform.localPosition.y, 0);
            GlobalVariables.Images.Add(image);
            string path_marker = Path.Combine("institution_" + Convert.ToString(institution.data.id), "marker_" + Convert.ToString(institution.data.markers[i].id)); //путь для папки кеширования маркерам

            var cacheFilePath = Path.Combine(Application.persistentDataPath, @"Cache", path_marker, "sticker" + ".png");
            StartCoroutine(DownloadImage("file://"+cacheFilePath, rawImage));
        }
        
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
