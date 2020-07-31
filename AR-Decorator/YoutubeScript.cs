/*using UnityEngine;
using System.IO;
using VideoLibrary;
public class YoutubeScript : MonoBehaviour
{
    public static void SaveVideoToDisk(string link,string cacheFilePath)
    {
        var youTube = YouTube.Default; // starting point for YouTube actions
        var video = youTube.GetVideo(link); // gets a Video object with info about the video
        File.WriteAllBytes(cacheFilePath, video.GetBytes());
    }
}
*/