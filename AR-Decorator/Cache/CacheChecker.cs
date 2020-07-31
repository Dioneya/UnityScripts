using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CacheChecker : MonoBehaviour
{
    public string GetTextFromCache(string fileName, string folder)
    {
        var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", folder, fileName + ".txt");
        
        if (File.Exists(cacheFilePath))
        {
            return File.ReadAllText(cacheFilePath);
        }

        return null;
    }
    public bool Check(out List<int> missingStickers, out List<int> missingMarkerImages, out List<int> missingARObjects, out List<int> missingMarkers)
    {
        var isAllCacheDownloaded = true;
        missingStickers = new List<int>();
        missingMarkers = new List<int>();
        missingARObjects = new List<int>();
        missingMarkerImages = new List<int>();
        var cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache", $"institution_{GlobalVariables.institution.data.id}");
        foreach (var marker in GlobalVariables.institution.data.markers)
        {
            if (!File.Exists(Path.Combine(cacheFilePath, $"marker_{marker.id}")))
            {
                missingMarkers.Add(marker.id);
                isAllCacheDownloaded = false;
                break;
            }
            if(!File.Exists(Path.Combine(cacheFilePath, $"marker_{marker.id}", "sticker.png")))
            {
                missingStickers.Add(marker.id);
                isAllCacheDownloaded = false;
            }
            if (!File.Exists(Path.Combine(cacheFilePath, $"marker_{marker.id}", $"{marker.id}.png")))
            {
                missingMarkerImages.Add(marker.id);
                isAllCacheDownloaded = false;
            }
            if (!File.Exists(Path.Combine(cacheFilePath, $"marker_{marker.id}", $"arObject_0", $"0+{IdentifyType(marker.a_r_object.object_type.value)}")))
            {
                missingARObjects.Add(marker.id);
                isAllCacheDownloaded = false;
            }
        }
        return isAllCacheDownloaded;
    }
    public bool CheckFile(string path) 
    {
        if (!File.Exists(Path.Combine(path)))
            return false;
        else
            return true;
    }
    string IdentifyType(int type)
    {
        const int Image = 0;
        const int Video = 1;
        const int Model = 2;
        const int Audio = 3;
        const int AssetBundle = 4;
        const int Text = 5;

        switch (type)
        {
            case Image:
                return ".png";
            case Video:
                return ".mp4";
            case Model:
                return "";
            case Audio:
                return ".mp3";
            case AssetBundle:
                return "";
            case Text:
                return ".txt";
            default:
                return "";
        }
    }
}
