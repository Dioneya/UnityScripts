using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class GlobalVariables : MonoBehaviour
{
    public static InstitutionJsonLoader.InstitutionList institution; // данные текущего института с маркерами + объектами

    public static int checkedID; //переменная хранящая отсканированный ID комнаты

    public static List<GameObject> Images = new List<GameObject>(); // Список хранящий стикеры заведения 

    public static string nameInst; //имя выбранного в меню института
    public static string descInst; //описание выбранного в меню института
    public static string countInst; //кол. стикеров выбранного в меню института

    public static string link = "http://likholetov.beget.tech";

    #region Константы 
    public const int Logo = -3;
    public const int Sticker = -2;
    public const int Marker = -1;
    public const int Image = 0;
    public const int Video = 1;
    public const int Model = 2;
    public const int Audio = 3;
    public const int AssetBundle = 4;
    public const int Text = 5;
    public const int AnimatedAssetBundle = 7;
    #endregion

    public static string cachePathFolder;
    public static string MarkerStrPath(int indexMarker)//возвращает путь к папке с маркером 
    {
        string marker_path = Path.Combine("institution_" + Convert.ToString(institution.data.id), "marker_" + Convert.ToString(institution.data.markers[indexMarker].id)); //путь для папки кеширования маркерам
        return marker_path;
    }
    public static string ArObjStrPath(int indexOfMarker) // возвращает путь к папке кэша объекта
    {
        string ar_obj_path = Path.Combine("institution_" + Convert.ToString(institution.data.id), "marker_" + Convert.ToString(GlobalVariables.institution.data.markers[indexOfMarker].id), "arObject_" + institution.data.markers[indexOfMarker].a_r_object.id);
        return ar_obj_path;
    }
    public static string CacheFilePath(int indexOfMarker, int key, string fileName = "") //возвращает путь к файлу кэша объекта
    {
        
        if (fileName == "") 
        {
            fileName = Convert.ToString(institution.data.markers[indexOfMarker].a_r_object.id);
        }
        string cacheFilePath = "";
        switch (key)
        {
            case Logo: 
                {
                    cacheFilePath = Path.Combine(cachePathFolder, "institution_" + Convert.ToString(institution.data.id), "logo.png");
                    break;
                }
            case Sticker: 
                {
                    cacheFilePath = Path.Combine(cachePathFolder, MarkerStrPath(indexOfMarker), "sticker" + ".png");
                    break;
                }
            case Marker:
                {
                    cacheFilePath = Path.Combine(cachePathFolder, MarkerStrPath(indexOfMarker), fileName + ".png");
                    break;
                }
            case Video:
                {
                    cacheFilePath = Path.Combine(cachePathFolder, ArObjStrPath(indexOfMarker), fileName + ".mp4");
                    break;
                }
            case Image:
                {
                    cacheFilePath = Path.Combine(cachePathFolder, ArObjStrPath(indexOfMarker), fileName + ".png");
                    break;
                }
            case Audio:
                {
                    cacheFilePath = Path.Combine(cachePathFolder, ArObjStrPath(indexOfMarker), fileName + ".mp3");
                    break;
                }
            case AssetBundle:
                {
                    cacheFilePath = Path.Combine(cachePathFolder, ArObjStrPath(indexOfMarker), fileName);
                    break;
                }
            case Text: 
                {
                    cacheFilePath = Path.Combine(cachePathFolder, ArObjStrPath(indexOfMarker), fileName + ".txt");
                    break;
                }
            default: 
                {
                    break;
                }
        }
        
        return cacheFilePath;
    }

}
