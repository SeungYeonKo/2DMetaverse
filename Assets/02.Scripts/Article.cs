using System;
using System.Collections.Generic;
using UnityEngine;

// MVC 아키텍처 패턴
// - Model          (데이터와 그 데이터를 다루는 로직)         -> Article
// - View            (UI, 사용자 인터페이스)                            -> UI_Article, UI_ArticleList
// - Controller    (관리자, Model<->View 사이의 중재자)  -> ArticleManager
// 위 요소들(데이터, 시각적, 관리)의 간섭없이 독립적으로 개발할 수 있다.

public enum ArticleType
{
    Normal,
    Notice,
}

// 게시글을 나타내는 데이터 클래스
[Serializable]
public class Article    // Quest, Item, Achievement, Attendance, 
{
    public ArticleType ArticleType;
    public string Name;
    public string Content;
    public int Like;
    public DateTime WriteTime;  
}

[Serializable]
public class ArticleData
{
    public List<Article> Data;

    public ArticleData(List<Article> data)
    {
        Data = data;
    }
}
