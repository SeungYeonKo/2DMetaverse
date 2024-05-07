using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ArticleList : MonoBehaviour
{
    public List<UI_Article> UIArticles;
    public GameObject EmptyObject;

    private void Start()
    {
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {
        // 1. Article 매니저로부터 Article을 가져온다
        List<Article> articles = ArticleManager.Instance.Articles;

        // 게시글 개수가 0개 일때 
        EmptyObject.gameObject.SetActive(articles.Count == 0);

        // 2. 모든 UI_Article을 끈다.
        foreach (UI_Article uiArticle in UIArticles)
        {
            uiArticle.gameObject.SetActive(false);
        }
        // 3. 가져온 Article 개수만큼 UI_Article을 킨다.
        for (int i = 0; i < articles.Count; i++)
        {
            if (i < UIArticles.Count)
            {
                // UI_Article의 해당 요소를 활성화
                UIArticles[i].gameObject.SetActive(true);

                // 4. 각 UI_Article의 내용을 Article로 초기화(Init)한다.
                UIArticles[i].Init(articles[i]);
            }
        }
    }
}
