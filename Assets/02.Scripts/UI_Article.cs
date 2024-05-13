using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using UnityEngine.UI;
// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    private Dictionary<string, Texture> _cache = new Dictionary<string, Texture>();

    public RawImage ProfileImageUI;   // 프로필 이미지
    public Text NameTextUI;       // 글쓴이
    public Text ArticleTypeUI;
    public Text ContentTextUI;    // 글 내용
    public Text LikeTextUI;       // 좋아요 개수
    public Text WriteTimeUI;      // 글 쓴 날짜/시간

    public UI_ArticleMenu MenuUI;

    private Article _article;

    public void Init(in Article article)
    {
        _article = article;

        NameTextUI.text = article.Name;
        ArticleTypeUI.text = article.ArticleType.ToString(); // Enum을 String으로 변환
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());

       StartCoroutine(GetTexture(article.Profile));
   
    }

    private IEnumerator GetTexture(string url)
    {
        // 캐쉬된게 있을 때 -> 캐시 히트(적중)
        if (_cache.ContainsKey(url))
        {
            Debug.Log("캐시 히트!");
            var now = DateTime.Now;

            ProfileImageUI.texture = _cache[url];

            var span = DateTime.Now - now;
            Debug.Log($"캐시 히트 ! : {span.TotalMilliseconds}");

            yield break;
        }
       
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);



        yield return www.SendWebRequest(); // 비동기

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ProfileImageUI.texture = myTexture;
        }
    }

    private string GetTimeString(DateTime dateTime)
    {
        TimeSpan timeSpan = DateTime.Now - dateTime;
        if (timeSpan.TotalMinutes < 1)     // 1분 이내 -> 방금 전
        {
            return "방금 전";
        }
        else if (timeSpan.TotalHours < 1) // 1시간 이내 -> n분 전
        {
            return $"{timeSpan.TotalMinutes:N0}분 전";
        }
        else if (timeSpan.TotalDays < 1)  // 하루 이내 -> n시간 전
        {
            return $"{timeSpan.TotalHours:N0}시간 전";
        }
        else if (timeSpan.TotalDays < 7) // 7일 이내 -> n일 전
        {
            return $"{timeSpan.TotalDays:N0}일 전";
        }
        else if (timeSpan.TotalDays < 7 * 4) // 4주 이내 -> n주 전
        {
            return $"{timeSpan.TotalDays / 7:N0}주 전";
        }

        return dateTime.ToString("yyyy년 M월 d일");
    }


    public void OnClickMenuButton()
    {
        MenuUI.Show(_article);
    }

    public void OnClickLikeButton()
    {
        // 1. 데이터 조작은 항상 매니저에게 시킨다
        ArticleManager.Instance.AddLike(_article);

        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Refresh();
    }
}