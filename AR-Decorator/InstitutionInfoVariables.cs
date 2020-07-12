using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InstitutionInfoVariables : MonoBehaviour
{
    public string cachePath;
    public string nameInst;
    public string description;
    public string count;
    GameObject obj;
    void Start() 
    {
        obj = this.gameObject;
        GameObject deleteBtn = this.transform.Find("ActionButton").gameObject;
        deleteBtn.GetComponent<Button>().onClick.AddListener(DeleteCache);
        GameObject button = this.transform.Find("Button").gameObject;

        button.GetComponent<Button>().onClick.AddListener(ChangeName);
    }

    void DeleteCache() 
    {
        Directory.Delete(cachePath, true);
        Destroy(obj);
    }

    void ChangeName() 
    {
        GlobalVariables.countInst = count;
        GlobalVariables.nameInst = nameInst;
        GlobalVariables.descInst = description;
    }

   
}
