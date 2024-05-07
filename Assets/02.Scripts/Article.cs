using System;
using UnityEngine;


public enum ArticleType
{
    Normal,
    Notice,
}

// 게시글을 나타내는 데이터 클래스
public class Article    // Quest, Item, Achievement, Attendance, 
{
    public ArticleType ArticleType;
    public string Name;
    public string Content;
    public int Like;
    public DateTime WriteTime;  
}
