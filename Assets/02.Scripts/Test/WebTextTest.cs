using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebTextTest : MonoBehaviour
{
    public Text TextUI;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        // UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://health.chosun.com/site/data/img_dir/2023/07/17/2023071701753_0.jpg");
        UnityWebRequest www = UnityWebRequest.Get("http://time.jsontest.com/");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            //byte[] results = www.downloadHandler.data;
            string text = www.downloadHandler.text;
            TextUI.text = text;
            WebTime webtime = JsonUtility.FromJson<WebTime>(text);
            Debug.Log(webtime.date);
            Debug.Log(webtime.milliseconds_since_epoch);
            Debug.Log(webtime.time);
        }
    }
}

[Serializable]
public class WebTime
{
    public string date;
    public long milliseconds_since_epoch;
    public string time;
}