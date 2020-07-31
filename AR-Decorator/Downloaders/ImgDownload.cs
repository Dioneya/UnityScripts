using UnityEngine;
using System.IO;
public class ImgDownload : MonoBehaviour
{
    public string url;

    public void StartDownload(int index)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;

        var cacheFilePath = GlobalVariables.CacheFilePath(index, GlobalVariables.Image);

        Texture2D texture = new Texture2D(4, 4);
        texture.LoadImage(File.ReadAllBytes(cacheFilePath));

        quad.GetComponent<Renderer>().material.mainTexture = texture;
    }

}
