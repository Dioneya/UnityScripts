using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackArrowSticker : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject Organizations;
    public GameObject ContentSticker;

    void Start() 
    {
        //GetComponent<Button>().onClick.AddListener(Back);
    }

    public void Back() 
    {
        GlobalVariables.Images.Clear();
        for (int i = 1; i < ContentSticker.transform.childCount; i++) 
        {
            GameObject sticker = ContentSticker.transform.GetChild(i).gameObject;
            Destroy(sticker);
        }

        
        Organizations.SetActive(true);
        ScrollView.SetActive(false);

    }
}
