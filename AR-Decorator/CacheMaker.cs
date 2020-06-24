using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CacheMaker : MonoBehaviour
{
    void Start() 
    {
        
    }
    public void CacheText(string fileName, string folder, string data)
    {
        var cacheFilePath = Path.Combine(Application.dataPath,@"Cache\"+folder, fileName+".txt");
        var pathFolder = Path.Combine(Application.dataPath, @"Cache\" + folder);
        if (!Directory.Exists(pathFolder)) 
        {
            Directory.CreateDirectory(pathFolder);
        }
        Debug.LogWarning(cacheFilePath);

        File.WriteAllText(cacheFilePath, data);
    }

}
