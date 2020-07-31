using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackArrowSticker : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject Organizations;
    public GameObject ContentSticker;
    public GameObject LocalMap;

    void Start() 
    {
        //GetComponent<Button>().onClick.AddListener(Back);
    }

    public void Back() 
    {


        if (LocalMap.activeSelf) {
            ScrollView.SetActive(true);
            LocalMap.SetActive(false);
            ContentSticker.transform.parent.gameObject.SetActive(true);
        } 
        else if (ScrollView.activeSelf) {
            Organizations.SetActive(true);
            ScrollView.SetActive(false);
        } 
        if (Organizations.activeSelf) {
            GlobalVariables.Images.Clear();
            for (int i = 0; i < ContentSticker.transform.childCount; i++)
            {
                GameObject sticker = ContentSticker.transform.GetChild(i).gameObject;
                Destroy(sticker);
            }
        }


    }
}
