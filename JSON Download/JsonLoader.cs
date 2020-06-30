using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JsonLoader : MonoBehaviour
{
    QRScanner qrScn;
    CacheMaker cacheMaker;
    CacheChecker cacheChecker;
    ImageDownload imageDownload;


    public InstitutionJsonLoader.InstitutionList institution;
    public string jsonUrl;
    public string id; 
    private bool mustCahe = true;

    void Start()
    {
        qrScn = GetComponent<QRScanner>();
        cacheMaker = GetComponent<CacheMaker>();
        cacheChecker = GetComponent<CacheChecker>();
        imageDownload = GetComponent<ImageDownload>();
        StartLoad();
    }

    public void StartLoad() 
    {
        //var cacheText = cacheChecker.GetTextFromCache("json",Convert.ToString(qrScn.checkedID));
        var cacheText = cacheChecker.GetTextFromCache("json", id);
        if (cacheText != null) 
        {
            Debug.LogWarning("Кэш найден");
            processJson(cacheText);
        }
        else 
        {
            Debug.LogWarning("Кэш не найден, выполнена загрузка");
            jsonUrl = "http://likholetov.beget.tech/api/institution/" + id;//qrScn.checkedID;
            StartCoroutine(LoadJson());
        }
    }

    IEnumerator LoadJson()
    {
        WWW www = new WWW(jsonUrl);
        yield return www;
        if (www.error == null)
        {
            processJson(www.text);
            UnityEngine.Debug.Log(www.text);
        }
        else
        {
            UnityEngine.Debug.Log("invalid url");
            StartCoroutine(LoadJson());
        }
    }

    private void processJson(string url)
    {
        institution = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(url);
        if (mustCahe) 
        {
            cacheMaker.CacheText("json",Convert.ToString(institution.data.id),url);
        }
        
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter() 
    {
        yield return new WaitForSeconds(2);
        if (institution.data.markers[0].image_set[0].url != null)
        {
            Debug.LogWarning(institution.data.markers[0].image_set[0].url);
        }
        else
        {
            Debug.LogWarning("Всё таки  не работает");
        }
        imageDownload.StartCreate();

    }
}
