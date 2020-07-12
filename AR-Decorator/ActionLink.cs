using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ActionLink : MonoBehaviour
{
    
    public string action_link;

    string response;

    [System.Serializable]
    public class Action
    {
        public string message;
        public string url;
    }

    public Action action;
    void Start() 
    {
        StartCoroutine(LoadTextFromServer(action_link));
    }
    public void OnMouseDown()
    {
        Debug.Log(response);

        if (action.url != null) 
        {
            Application.OpenURL(action.url); //Запуск сайта в браузере
        }
    }
    //Загрузка JSON текст с сервера 
    IEnumerator LoadTextFromServer(string url)
    {
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            string jsonText = (request.downloadHandler).text;
            processJson(jsonText);
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            response = null;
        }

        request.Dispose();
    }

    private void processJson(string json)
    {
        action = JsonUtility.FromJson<Action>(json);
        response = action.message;
        
    }


}
