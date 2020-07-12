using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;

public class MarkerInfo : MonoBehaviour
{
    public GameObject MarkerBlock;
    public List<InstitutionJsonLoader.InstitutionList> institutionLists;
    public GameObject ScrollView;
    public GameObject Organizations;
    public GameObject ContentSticker;

    public GameObject OrganizationHeader;
    InstitutionJsonLoader.InstitutionList institution;
    void Start() 
    {
        ScrollView.SetActive(false);
        OrganizationHeader = ScrollView.transform.Find("OrganizationInfo").gameObject.transform.Find("OrganizationHeader").gameObject;

        string path = Path.Combine(Application.persistentDataPath, @"Cache"); //укажем путь к кэшу
        string[] allfolders = Directory.GetDirectories(path); // найдём все папки заведений 

        for (int i = 0; i < allfolders.Length; i++) //обрабатываем все json с элементами
        {
            string jsonPath = Path.Combine(allfolders[i],"json.txt"); 
            Debug.LogWarning(jsonPath);
            string jsonText = File.ReadAllText(jsonPath);
            var institution = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(jsonText);
            institutionLists.Add(institution);
        }

        for (int i = 0; i < institutionLists.Count; i++) 
        {
            GameObject markerBlock = Instantiate(MarkerBlock);

            var info = markerBlock.AddComponent<InstitutionInfoVariables>();
            info.cachePath = allfolders[i];
            info.nameInst = institutionLists[i].data.title;
            info.description = institutionLists[i].data.description;
            info.count = Convert.ToString(institutionLists[i].data.markers.Count);

            markerBlock.SetActive(true);
            markerBlock.transform.SetParent(gameObject.transform);
            markerBlock.transform.localPosition = new Vector3(markerBlock.transform.localRotation.x, markerBlock.transform.localRotation.y,0);
            markerBlock.transform.localScale = new Vector3(1,1,1);

            GameObject button = markerBlock.transform.Find("Button").gameObject;
            institution = institutionLists[i];

            button.GetComponent<Button>().onClick.AddListener(ButtonProcess);

            GameObject organization = button.transform.Find("organization").gameObject;

            GameObject organizationName = organization.transform.Find("OrganizationName").gameObject;
            organizationName.GetComponent<TextMeshProUGUI>().text = info.nameInst;

            GameObject organizationCount = organization.transform.Find("OrganizationMarkersCount").gameObject;
            organizationCount.GetComponent<TextMeshProUGUI>().text = info.count;

            GameObject organizationAdress = organization.transform.Find("OrganizationAddress").gameObject;
            organizationAdress.GetComponent<TextMeshProUGUI>().text = info.description;

        }
    }

    void ButtonProcess() 
    {
        ScrollView.SetActive(true);
        ContentSticker.GetComponent<StickerImage>().StartLoad(institution);
        Organizations.SetActive(false);

    }
}
