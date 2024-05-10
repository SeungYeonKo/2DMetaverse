using System;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public Image ProfileImageUI;   // 프로필 이미지
    public Text NameTextUI;       // 글쓴이
    public Text ContentTextUI;    // 글 내용
    public Text LikeTextUI;       // 좋아요 개수
    public Text WriteTimeUI;      // 글 쓴 날짜/시간

    public UI_ArticleMenu MenuUI;
    public Article _article;

    public void Init(in Article article)
    {
        _article = article;

        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"{article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());      // ToLocalTime을 써서 한국 시간으로 변환
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
}