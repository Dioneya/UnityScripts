using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class CacheMaker : MonoBehaviour
{
    public bool isVideoDone;
    public bool isAudioDone;
    public bool isImageDone;
    public bool isAssetDone;
    public void CacheText(string folder, string fileName, string data)
    {
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName+".txt");
        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);
        if (!Directory.Exists(pathFolder)) 
        {
            Directory.CreateDirectory(pathFolder);
        }
        Debug.LogWarning(cacheFilePath);
        
        File.WriteAllText(cacheFilePath, data);
    }
    #region Video
    public void StartDownloadAndCacheVideo (string folder, string fileName, string url,bool isYoutube = false)
    {
        if (isYoutube)
        {
            StartCoroutine(CacheYoutubeVideo(folder, fileName, url));
        }
        else 
        {
            StartCoroutine(CacheVideo(folder, fileName, url));
        }
    }
    IEnumerator CacheVideo(string folder, string fileName, string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName + ".mp4");
        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            File.WriteAllBytes(cacheFilePath, www.downloadHandler.data);
        }
        isVideoDone = true;
    }
    IEnumerator CacheYoutubeVideo(string folder, string fileName, string url) 
    {
        yield return new WaitForSeconds(0.1f);
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName + ".mp4");
        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        //YoutubeScript.SaveVideoToDisk(url, cacheFilePath);
        isVideoDone = true;  
    }
    #endregion
    #region Image
    public void StartDownloadAndCacheImage(string folder, string fileName, string url)
    {
        StartCoroutine(CacheImage(folder, fileName, url));
    }
    IEnumerator CacheImage(string folder, string fileName, string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName + ".png");
        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        Debug.LogWarning(cacheFilePath);
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            File.WriteAllBytes(cacheFilePath, www.downloadHandler.data);
        }
        isImageDone = true;
    }
    #endregion
    #region Text
    public void StartDownloadAndCacheText(string folder, string fileName, string url)
    {
        StartCoroutine(LoadTextFromServer(folder, fileName, url));
    }
    IEnumerator LoadTextFromServer(string folder, string fileName, string url)
    {
        var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName + ".txt");
        if (request.isHttpError && request.isNetworkError)
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);
        }
        else
        {
            File.WriteAllText(cacheFilePath, request.downloadHandler.text);
        }
        request.Dispose();
    }
    #endregion
    #region Audio
    public void StartDownloadAndCacheAudio(string folder, string fileName, string url, AudioType audioType)
    {
        StartCoroutine(LoadAudioFromServer(folder, fileName, url, audioType));
    }
    IEnumerator LoadAudioFromServer(string folder, string fileName, string url, AudioType audioType)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName + ".mp3");
        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            File.WriteAllBytes(cacheFilePath, www.downloadHandler.data);
        }
        isAudioDone = true;
    }
    #endregion
    #region AssetBundle
    public void StartDownloadAsset(string folder, string fileName, string url)
    {
        StartCoroutine(DownloadAsset(folder, fileName, url));
    }
    public IEnumerator DownloadAsset(string folder, string fileName, string url)
    {
        //string url = "http://safebuild.net/hari/bundle/kitchen";
        Debug.Log("Successsssssss");
        UnityWebRequest www = UnityWebRequest.Get(url);
        DownloadHandler handle = www.downloadHandler;

        //Send Request and wait
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("Error while Downloading Data: " + www.error);
        }
        else
        {
            Debug.Log("Success");
            //handle.data
            //Construct path to save it
            var cacheFilePath = Path.Combine(GlobalVariables.cachePathFolder, folder, fileName);
            var pathFolder = Path.Combine(GlobalVariables.cachePathFolder, folder);

            //Save
            Save(handle.data, cacheFilePath);
            isAssetDone = true;
        }
    }
    public void Save(byte[] data, string path)
    {
        //Create the Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        try
        {
            File.WriteAllBytes(path, data);
            Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
        
    }
    #endregion
}
