using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
public class StickerImage : MonoBehaviour
{
    [SerializeField] private GameObject StickerPrefab;
    [SerializeField] private GameObject HintBlock;
    [SerializeField] private RawImage HintImage;
    [SerializeField] private TextMeshProUGUI HintDescription;
    // Start is called before the first frame update
    public void StartLoad(InstitutionJsonLoader.InstitutionList institution)
    {
        
        for (int i = 0; i < institution.data.markers.Count; i++ ) 
        {
            #region Создание стикера и первоначальная настройка
            GameObject image = Instantiate(StickerPrefab);
            image.SetActive(true);
            image.name = "Sticker Image " + institution.data.markers[i].id;
            image.transform.SetParent(transform);
            #endregion

            #region ShowHintImage
            ShowHintImage showbigsticker = image.GetComponent<ShowHintImage>();
            showbigsticker.HintImage = HintImage;
            showbigsticker.HintBlock = HintBlock;
            showbigsticker.HintDescription = HintDescription;
            showbigsticker.text = institution.data.markers[i].description;
            #endregion

            #region Настройка RawImage
            RawImage rawImage = image.GetComponent<RawImage>();
            image.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, image.transform.localPosition.y, 0);
            GlobalVariables.Images.Add(image);
            #endregion

            var cacheFilePath = GlobalVariables.CacheFilePath(i, GlobalVariables.Sticker);

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
