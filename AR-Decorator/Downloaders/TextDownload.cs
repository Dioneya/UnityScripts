using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDownload : MonoBehaviour
{
    public string text;

    GameObject TextCanvas;
    public GameObject Text;
    void Start() 
    {
        
    }
    public void StartDownload()
    {
        TextCanvas = Resources.Load<GameObject>("Prefabs/TextCanvas");
        GameObject textObj = (Instantiate(TextCanvas)) as GameObject;
        textObj.transform.SetParent(gameObject.transform);

        for (int i = 0; i < textObj.transform.childCount; i++) {
            if (textObj.transform.GetChild(i).name == "TextContent") 
            {
                GameObject TextContent = textObj.transform.GetChild(i).gameObject;
                Text = TextContent.transform.GetChild(0).gameObject;
                break;

            }
        }

        Text.GetComponent<TextMeshProUGUI>().text = text;
    }
}
