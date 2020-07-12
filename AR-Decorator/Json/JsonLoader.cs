using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JsonLoader : MonoBehaviour
{
   
    CacheMaker cacheMaker;
    CacheChecker cacheChecker;
    SceneLoader sceneLoader;

    public string jsonUrl;
    public string id; 
    private bool mustCahe = true;

    void Start()
    {
        cacheMaker = GetComponent<CacheMaker>();
        cacheChecker = GetComponent<CacheChecker>();
        sceneLoader = GetComponent<SceneLoader>();
        StartLoad();
    }

    public void StartLoad() 
    {
        //var cacheText = cacheChecker.GetTextFromCache("json",Convert.ToString(qrScn.checkedID));
        var cacheText = cacheChecker.GetTextFromCache("json", "institution_"+id);
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
        GlobalVariables.institution = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(url);

        Debug.LogWarning(GlobalVariables.institution.data.title);
        if (mustCahe) 
        {
            cacheMaker.CacheText("institution_" + Convert.ToString(GlobalVariables.institution.data.id),"json",url);
        }

        sceneLoader.LoadScene(sceneLoader.SceneNmb);
    }
}
