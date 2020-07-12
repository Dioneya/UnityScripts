using System.Collections;
using UnityEngine;
using System;
using System.Net;
using System.IO;

public class AssetCacheAndDownload : MonoBehaviour
{
    static string url;
    Uri uri = new Uri(url);
    WebClient client = new WebClient();
    public IEnumerator DownLoadAsset(string folder, string fileName)
    {
        var cacheFilePath = Path.Combine(Application.persistentDataPath, @"Cache\" + folder, fileName);
        client.DownloadFileAsync(uri, cacheFilePath);

        while (client.IsBusy)
        {
            yield return null;
        }
    }
}