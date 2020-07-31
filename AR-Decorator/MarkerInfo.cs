using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
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
            string jsonPath = Path.Combine(allfolders[i], "json.txt");
            Debug.LogWarning(jsonPath);
            string jsonText = File.ReadAllText(jsonPath);
            var institution = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(jsonText);
            institutionLists.Add(institution);
        }

        for (int i = 0; i < institutionLists.Count; i++)
        {
            institution = institutionLists[i];
            GameObject markerBlock = Instantiate(MarkerBlock);

            InstitutionInfoVariables info = markerBlock.AddComponent<InstitutionInfoVariables>();
            info.cachePath = allfolders[i];
            info.nameInst = institution.data.title;
            info.description = institution.data.description;
            info.count = Convert.ToString(institution.data.markers.Count);

            MarkerBlockInit(markerBlock);

            InformationAboutOrganization organizationInformation = markerBlock.GetComponent<InformationAboutOrganization>();
            Button button = organizationInformation.transform.GetChild(0).GetComponent<Button>();

            button.onClick.AddListener(ButtonProcess);

            string url = Path.Combine(path, "institution_" + institution.data.id, "logo.png");


            organizationInformation.OrganizationName.text = info.nameInst;
            organizationInformation.OrganizationMarkersCount.text = info.count;
            organizationInformation.OrganizationAddress.text = info.description;

            RawImage image = organizationInformation.Image;
            Texture2D texture = new Texture2D(4, 4);
            texture.LoadImage(File.ReadAllBytes(url));
            image.texture = texture;
        }
    }
    private void MarkerBlockInit(GameObject markerBlock) {
        markerBlock.SetActive(true);
        markerBlock.transform.SetParent(gameObject.transform);
        markerBlock.transform.localPosition = new Vector3(markerBlock.transform.localRotation.x, markerBlock.transform.localRotation.y, 0);
        markerBlock.transform.localScale = new Vector3(1, 1, 1);
    }
    void ButtonProcess() 
    {
        ScrollView.SetActive(true);
        ContentSticker.GetComponent<StickerImage>().StartLoad(institution);
        Organizations.SetActive(false);
    }
}
