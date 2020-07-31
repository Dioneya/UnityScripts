using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject SliderLoad;
    [SerializeField] private Slider Slider;
    [SerializeField] private TextMeshProUGUI LoadingObjectText;

    CacheMaker cacheMaker;
    CacheChecker cacheChecker;

    public int SceneNmb;
    private static int sceneNmb;
    private float progress = 0;

    public static string link = GlobalVariables.link;
    void Start()
    {
        SceneNmb = sceneNmb;
        cacheMaker = GetComponent<CacheMaker>();
        cacheChecker = GetComponent<CacheChecker>();
        //LoadScene(SceneNmb);
    }
    public static void SetSceneNmb(int snmb) {
        sceneNmb = snmb;
    }
    public void LoadScene(int sceneIndex) {

        StartCoroutine(LoadAsunc(sceneIndex));
    }
    IEnumerator LoadAsunc(int sceneIndex) {

        SliderLoad.SetActive(true);
        LoadingObjectText.gameObject.SetActive(true);

        StartCoroutine(LoadContent()); //запуск метода по закачке данных + кеширование

        //SceneManager.LoadSceneAsync(sceneIndex); //переход на другую сцену

        yield return null;
    }

    private void Update()
    {
        #region Загрузочная шкала 
        Slider.value = progress / 100;
        LoadingObjectText.text = string.Format(($"{progress:0}%"));
        #endregion

        if (Slider.value == 1)
        {
            LoadingScreenAnimation.PlayAnimation = false;
            SceneManager.LoadSceneAsync(sceneNmb);
        }
    }
    IEnumerator LoadContent()
    {
        float progress_cnt = 100f / ((GlobalVariables.institution.data.markers.Count * 3f));

        #region Константы 
        const int Image = GlobalVariables.Image;
        const int Video = GlobalVariables.Video;
        const int Model = GlobalVariables.Model;
        const int Audio = GlobalVariables.Audio;
        const int AssetBundle = GlobalVariables.AssetBundle;
        const int Text = GlobalVariables.Text;
        const int AnimatedAssetBundle = GlobalVariables.AnimatedAssetBundle;
        #endregion

        var markerInfo = GlobalVariables.institution.data.markers;


        for (int i = 0; i < GlobalVariables.institution.data.markers.Count; i++)
        {

            string ar_object_url="";

            #region Проверка на тип ссылки объекта 
            if (markerInfo[i].a_r_object.object_path.url != null) 
            {
                if (markerInfo[i].a_r_object.object_path.url.Contains("http"))
                    ar_object_url = markerInfo[i].a_r_object.object_path.url; //url ссылка на объект
                else
                    ar_object_url = link + markerInfo[i].a_r_object.object_path.url;
            }
            #endregion

            #region Скачка всех изображений из image_set
            for (int j = 0; j < markerInfo[i].image_set.Count; j++) 
            {
                if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i,GlobalVariables.Marker,Convert.ToString(j))))
                {
                    cacheMaker.StartDownloadAndCacheImage(GlobalVariables.MarkerStrPath(i), Convert.ToString(j), link + markerInfo[i].image_set[j].url); //кэширование маркера 
                    yield return new WaitWhile(() => !cacheMaker.isImageDone);
                    cacheMaker.isImageDone = false;
                }
            }
            #endregion

            progress += progress_cnt;

            #region Стикеры
            if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i,GlobalVariables.Sticker)))
            {
                cacheMaker.StartDownloadAndCacheImage(GlobalVariables.MarkerStrPath(i), "sticker", link + markerInfo[i].sticker_image);
                yield return new WaitWhile(() => !cacheMaker.isImageDone);
                cacheMaker.isImageDone = false;
            }
            #endregion

            progress += progress_cnt;

            #region Лого
            if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i,GlobalVariables.Logo)))
            {
                cacheMaker.StartDownloadAndCacheImage(Path.Combine("institution_" + Convert.ToString(GlobalVariables.institution.data.id)), "logo", link + GlobalVariables.institution.data.image);
                yield return new WaitWhile(() => !cacheMaker.isImageDone);
                cacheMaker.isImageDone = false;
            }
            #endregion

            int key = markerInfo[i].a_r_object.object_type.value; //переменная которая указывает на id типа объекта

            #region Объекты
            switch (key)
            {
                #region Изображение
                case Image:
                    {
                        if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i, GlobalVariables.Image)))
                        {
                            cacheMaker.StartDownloadAndCacheImage(GlobalVariables.ArObjStrPath(i), Convert.ToString(markerInfo[i].a_r_object.id), ar_object_url);
                            yield return new WaitWhile(() => !cacheMaker.isImageDone);
                            cacheMaker.isImageDone = false;
                        }
                        
                        break;
                    }
                #endregion
                #region Видео
                case Video:
                    {
                        if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i, GlobalVariables.Video)))
                        {
                            cacheMaker.StartDownloadAndCacheVideo(GlobalVariables.ArObjStrPath(i), Convert.ToString(markerInfo[i].a_r_object.id), ar_object_url);
                            yield return new WaitWhile(() => !cacheMaker.isVideoDone);
                            cacheMaker.isVideoDone = false;
                        }
                        break;
                    }
                #endregion
                #region Модели
                case Model:
                    {
                        break;
                    }
                #endregion
                #region Аудио
                case Audio:
                    {
                        if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i, GlobalVariables.Audio)))
                        {
                            cacheMaker.StartDownloadAndCacheAudio(GlobalVariables.ArObjStrPath(i), Convert.ToString(markerInfo[i].a_r_object.id), ar_object_url, AudioType.MPEG);
                            yield return new WaitWhile(() => !cacheMaker.isAudioDone);
                            cacheMaker.isAudioDone = false;
                        }
                        break;
                    }
                #endregion
                #region Ассет Бандлы
                case AssetBundle:
                    {
                        if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i, GlobalVariables.AssetBundle)))
                        {
                            cacheMaker.StartDownloadAsset(GlobalVariables.ArObjStrPath(i), Convert.ToString(markerInfo[i].a_r_object.id), ar_object_url);
                            yield return new WaitWhile(() => !cacheMaker.isAssetDone);
                            cacheMaker.isAssetDone = false;
                        }
                        break;
                    }
                #endregion
                #region Текст
                case Text:
                    {
                        var cacheFilePath = GlobalVariables.CacheFilePath(i,GlobalVariables.Text);
                        Debug.Log(cacheFilePath);
                        var pathFolder = Path.Combine(GlobalVariables.cachePathFolder,GlobalVariables.ArObjStrPath(i));
                        Debug.Log(pathFolder);

                        if (!Directory.Exists(pathFolder))
                        {
                            Directory.CreateDirectory(pathFolder);
                        }
                        File.WriteAllText(cacheFilePath, markerInfo[i].a_r_object.object_settings.text);
                        break;
                    }
                #endregion
                case AnimatedAssetBundle:
                    {
                        if (!cacheChecker.CheckFile(GlobalVariables.CacheFilePath(i, GlobalVariables.AssetBundle)))
                        {
                            cacheMaker.StartDownloadAsset(GlobalVariables.ArObjStrPath(i), Convert.ToString(markerInfo[i].a_r_object.id), ar_object_url);
                            yield return new WaitWhile(() => !cacheMaker.isAssetDone);
                            cacheMaker.isAssetDone = false;
                        }
                        break;
                    }
            }
            #endregion

            progress += progress_cnt;
            
            Debug.LogWarning(link + markerInfo[i].image_set[0].url);
            
           //yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
