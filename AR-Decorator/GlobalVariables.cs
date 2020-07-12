using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class GlobalVariables : MonoBehaviour
{
    public static InstitutionJsonLoader.InstitutionList institution; // данные текущего института с маркерами + объектами

    public static int checkedID; //переменная хранящая отсканированный ID комнаты

    public static List<GameObject> Images = new List<GameObject>();

    public static string nameInst;
    public static string descInst;
    public static string countInst;
}
