using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI_Article 관리
public class UI_ArticleList : MonoBehaviour
{
    public static UI_ArticleList Instance { get; private set; }

    public List<UI_Article> UIArticles;
    public GameObject EmptyObject;
    public UI_ArticleWrite ArticleWriteUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Refresh();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {

        // 1. Article매니저로부터 Article들을 가져온다.
        List<Article> articles = ArticleManager.Instance.Articles;

        // 게시글 개수가 0개일 때만 '첫 글을 작성해보세요' 보여주기
        EmptyObject.gameObject.SetActive(articles.Count == 0);

        // 2. 모든 UI_Article을 끈다.
        foreach (UI_Article uiArticle in UIArticles)
        {
            uiArticle.gameObject.SetActive(false);
        }

        for (int i = 0; i < articles.Count; i++)
        {
            // 3. 가져온 Article 개수만큼 UI_Article을 켠다.
            UIArticles[i].gameObject.SetActive(true);
            // 4. 각 UI_Article의 내용을 Article로 초기화(Init)한다.
            UIArticles[i].Init(articles[i]);
        }
    }

    // 전체보기 버튼을 클릭했을 때 호출되는 함수
    public void OnClickAllButton()
    {
        ArticleManager.Instance.FindAll();
        Refresh();
    }

    // 공지 버튼을 클릭했을 때 호출되는 함수
    public void OnClickNoticeButton()
    {
        ArticleManager.Instance.FindNotice();
        Refresh();
    }

    public void OnClickWriteButton()
    {
        gameObject.SetActive(false);
        ArticleWriteUI.gameObject.SetActive(true);
    }

 
}