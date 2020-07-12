using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAndCacheAsset : MonoBehaviour
{
    public void StartDownloadAsset(string folder, string fileName, string url)
    {
        DownloadAsset(folder, fileName, url);
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
            var cacheFilePath = Path.Combine(Application.persistentDataPath,"Cache", folder, fileName + ".unity3d");
        	var pathFolder = Path.Combine(Application.persistentDataPath, @"Cache", folder);

            //Save
            Save(handle.data, cacheFilePath);
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
}
