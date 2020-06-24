using UnityEngine;
using System.IO;

public class CacheChecker : MonoBehaviour
{
    public string GetTextFromCache(string fileName, string folder)
    {
        var cacheFilePath = Path.Combine(Application.dataPath, @"Cache\" + folder, fileName + ".txt");

        if (File.Exists(cacheFilePath))
        {
            return File.ReadAllText(cacheFilePath);
        }

        return null;
    }
}
