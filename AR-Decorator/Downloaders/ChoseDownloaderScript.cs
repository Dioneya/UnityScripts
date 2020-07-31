using UnityEngine;
using Vuforia;
public class ChoseDownloaderScript : MonoBehaviour
{
    public GameObject test;
    private JsonLoader jsonLoader;
    private ImageDownload marker;
    private string link = GlobalVariables.link;

    public void SelectDownload(int indexOfMarker, ref DataSetTrackableBehaviour trackableBehaviour)
    {
        jsonLoader = GetComponent<JsonLoader>();
        marker = GetComponent<ImageDownload>();
        var obj_path = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path;

        #region Константы 
        const int Image = GlobalVariables.Image;
        const int Video = GlobalVariables.Video;
        const int Model = GlobalVariables.Model;
        const int Audio = GlobalVariables.Audio;
        const int AssetBundle = GlobalVariables.AssetBundle;
        const int Text = GlobalVariables.Text;
        const int AnimatedAssetBundle = GlobalVariables.AnimatedAssetBundle;
        #endregion

        int key = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_type.value;

        trackableBehaviour.gameObject.AddComponent<ActionLink>().action_link = GlobalVariables.institution.data.markers[indexOfMarker].action_link;

        switch (key)
        {
            case Image:
                {
                    var image = trackableBehaviour.gameObject.AddComponent<ImgDownload>();
                    image.url = link + obj_path.url;
                    image.StartDownload(indexOfMarker);
                    break;
                }

            case Video:
                {
                    trackableBehaviour.gameObject.AddComponent<VideoTracker>();
                    var video = trackableBehaviour.gameObject.AddComponent<VideoDownload>();
                    //video.videoURL = link + obj_path.url;
                    video.StartDownload(indexOfMarker);
                    break;
                }

            case Model:
                {
                    break;
                }

            case Audio:
                {
                    var audio = trackableBehaviour.gameObject.AddComponent<AudioDownload>();
                    trackableBehaviour.gameObject.AddComponent<AudioTracker>();

                    audio.url = "file://" + GlobalVariables.CacheFilePath(indexOfMarker, 3);
                    audio.StartDownload();
                    break;
                }

            case AssetBundle:
                {
                    var model = trackableBehaviour.gameObject.AddComponent<AssetDownload>();

                    #region Заполнение информации о 3D модели
                    model.nameObj = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path.name;
                    model.url = "file:///" + GlobalVariables.CacheFilePath(indexOfMarker, 4);
                    model.transform.SetParent(trackableBehaviour.gameObject.transform);
                    model.action_link = GlobalVariables.institution.data.markers[indexOfMarker].action_link;
                    model.transform_obj = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.transform;
                    #endregion

                    model.StartLoad();

                    //test = trackableBehaviour.gameObject;
                    break;
                }
            case Text:
                {
                    var text = trackableBehaviour.gameObject.AddComponent<TextDownload>();
                    text.text = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_settings.text;
                    text.StartDownload();
                    break;
                }
            case AnimatedAssetBundle:
                {
                    var model = trackableBehaviour.gameObject.AddComponent<AnimatedAssetBundle>();
                    trackableBehaviour.gameObject.AddComponent<ResetPosition>();
                    #region Заполнение информации о 3D модели
                    model.nameObj = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path.name;
                    model.url = "file:///" + GlobalVariables.CacheFilePath(indexOfMarker, 4);
                    model.transform.SetParent(trackableBehaviour.gameObject.transform);
                    model.action_link = GlobalVariables.institution.data.markers[indexOfMarker].action_link;
                    model.transform_obj = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.transform;
                    #endregion

                    model.StartLoad();
                    break;
                }
                
            default: 
                {
                    break;
                }
        }

    }
}
