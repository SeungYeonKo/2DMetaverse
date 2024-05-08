using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Unity.VisualScripting;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    public static ArticleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // 몽고 DB로부터 article 조회
        // 1. 몽고 DB 연결
        string connectionString = "mongodb+srv://SeungYeon:SeungYeon@cluster0.yw3lg0i.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 특정 데이터 베이스 연결
        IMongoDatabase db = mongoClient.GetDatabase("metaverse");
     
        // 3. 특정 콜렉션 연결
        // 4. 모든 문서 읽어오기
        IMongoCollection<BsonDocument> articleCollection = db.GetCollection<BsonDocument>("articles");
        int count = (int)articleCollection.CountDocuments(new BsonDocument());
        var firstDocument = articleCollection.Find(new BsonDocument()).Limit(count).ToList();
        for (int i = 0; i < firstDocument.Count; ++i)
        {
            Debug.Log(firstDocument[i]);
        }

        // 5. 읽어온 문서 만큼 New Article()해서 데이터 채우고 _articles에 넣기
        foreach(var articleData in firstDocument)
        {
            Article newArticle = new Article();
            newArticle.Name = articleData["Name"].ToString();
            newArticle.Content = articleData["Content"].ToString();
            newArticle.Like = (int)articleData["Like"];
            newArticle.ArticleType = (ArticleType)articleData["ArticleType"].ToInt64();

            string articleDate = articleData["WriteTime"].ToString();
            DateTime articleDataTime = DateTime.Parse(articleDate);
            newArticle.WriteTime = articleDataTime;

            _articles.Add(newArticle);
        }
    }

}