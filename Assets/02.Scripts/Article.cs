using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// 
public enum ArticleType
{
    Normal,
    Notice,

}

// 게시글을 나타내는 데이터 클래스
public class Article 
{
    public ArticleType ArticleType;
    public string Name;
    public string Title;
    public string Content;
    public int Like;
    public DateTime WriteTime;  

}
