﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using WebClient;

public class IdDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject IdBox;
    [SerializeField] private Text _text;

    private string Code;
    private readonly string Url = "https://192.168.0.101:5001/api";

    [SerializeField] private GameObject StartGameObject;
    [SerializeField] private GameObject FinishGameObject;
    public void SendRequest()
    {
        StartCoroutine(WebRequest());
    }

   IEnumerator WebRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Post($"{Url}/complete-game",""))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               var res = JsonUtility.FromJson<Response>(www.downloadHandler.text);
               Code = res.code;
               _text.text = Code;
              Debug.Log(res.code);
            }
            
            HideMainButtons(true);
            IdBox.SetActive(true);
        }
        
    }

    public void OpenWebPage()
    {
        Application.OpenURL($"http://{Url}/game-results/{Code}");
    }

    private void HideMainButtons(bool value)
    {
        StartGameObject.SetActive(!value);
        FinishGameObject.SetActive(!value);
    }
}
