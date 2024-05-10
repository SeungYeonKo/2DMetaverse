using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    // 콜렉션
    private IMongoCollection<Article> _articleCollection;

    public static ArticleManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Init();
        FindAll();
    }

    // 몽고DB -> 데이터베이스 -> 콜렉션 연결
    private void Init()
    {
        // 몽고 DB로부터 article 조회
        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://SeungYeon:SeungYeon@cluster0.yw3lg0i.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        // 2. 특정 데이터베이스 연결
        IMongoDatabase db = mongoClient.GetDatabase("metaverse");
        // 3. 특정 콜렉션 연결
        _articleCollection = db.GetCollection<Article>("articles");
    }

    public void FindAll()
    {
        // 4. 모든 문서 읽어오기
        // 4.1 WriteTime을 기준으로 '정렬'
        // Sort 메서드를 이용해서 도큐먼트를 정렬할 수 있다.
        // 매개변수로는 어떤 Key로 정렬할 것인지 알려주는 BsonDocument를 전달해주면 된다.
        var sort = new BsonDocument();
        sort["WriteTime"] = -1;
        // +1 -> 오름차순 정렬 -> 낮은 값에서 높은 값으로 정렬한다.
        // -1 -> 내림차순 정렬 -> 높은 값에서 낮은 값으로 정렬한다.
        _articles = _articleCollection.Find(new BsonDocument()).Sort(sort).ToList();
        // 5. 읽어온 문서 만큼 New Article()해서 데이터 채우고 
        /*_articles.Clear();
        foreach (var data in dataList)
        {
            Article article = new Article();
            article.Name        = data["Name"].ToString();
            article.Content     = data["Content"].ToString();
            article.Like        = (int)data["Like"];
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.WriteTime   = DateTime.Parse(data["WriteTime"].ToString());
            //    _articles에 넣기
            _articles.Add(article);
        }*/
    }

    public void FindNotice()
    {
        // 4. 공지 문서 읽어오기
        _articles = _articleCollection.Find(data => (int)data.ArticleType == (int)ArticleType.Notice).ToList();
    }

    public void Write(ArticleType articleType, string content)
    {
        Article article = new Article()
        {
            ArticleType = articleType,
            Name = "고승연",
            Content = content,
            Like = 0,
            WriteTime = DateTime.Now,
        };
        _articleCollection.InsertOne(article);
    }

    public void Delete(ObjectId id)
    {
        _articleCollection.DeleteOne(d=>d.Id==id);
    }

    public void Modify(ObjectId id, string newContent)
    {
        var update = Builders<Article>.Update.Set(a => a.Content, newContent);
        _articleCollection.UpdateOne(a => a.Id == id, update);
    }

}