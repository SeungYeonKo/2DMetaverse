using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Unity.Properties;

public class MongoExample01 : MonoBehaviour
{
    public void Start()
    {
        // 데이터 베이스 역할
        // 쓰기, 읽기, 수정(갱신), 삭제 = C R U D
        // 읽기 : Find 메서드
        // Finds 메서드 : 컬렉션 담겨 있는 도큐먼트를 조회하는 메서드다
        // 사용 빈도가 압도적으로 높다 -> 그만큼 중요하다!

        // 1. MongoDB 서버에 연결
        string connectionString = "mongodb+srv://SeungYeon:SeungYeon@cluster0.yw3lg0i.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 데이터베이스 목록을 조회하여 연결 확인
      /*  try
        {
            List<BsonDocument> databases = mongoClient.ListDatabases().ToList();
            Debug.Log("연결 성공! 사용 가능한 데이터베이스 목록 : ");
            foreach(var db in databases)
            {
                Debug.Log(db["name"]);
            }
        }
        catch (Exception ex)
        {
            // 연결 실패시 예외 처리
            Debug.Log($"연결 실패: {ex.Message}");
        }*/

        // 3. 'sample_mflix' 데이터베이스 선택, 콜렉션 이름들 조회
        IMongoDatabase db = mongoClient.GetDatabase("sample_mflix");
        var collectionNames = db.ListCollectionNames().ToList();
        foreach (string name in collectionNames)
        {
            Debug.Log(name);
        }

        // 4. 'movies' 콜렉션 연결
        IMongoCollection<BsonDocument> movieCollection = db.GetCollection<BsonDocument>("movies");

        // 5. 'movies' 콜렉션 속 Document 개수 계산
        long count = movieCollection.CountDocuments(new BsonDocument());
        Debug.Log($"movies 컬렉션의 문서 개수 : {count}");
        

        // 6. Document 읽기

        // 6-1. 첫 번째 문서를 검색
        var firstDocument = movieCollection.Find(new BsonDocument()).FirstOrDefault();
        // 결과 출력
        if(firstDocument != null )
        {
            Debug.Log(firstDocument.ToString());
        }
        else
        {
            Debug.Log("문서가 없습니다");
        }

        // 6-2. 10개 문서를 검색
        var documents = movieCollection.Find(new BsonDocument()).Limit(10).ToList();
        for(int i = 0; i < documents.Count; ++i)
        {
            Debug.Log(documents[i]);
        }
        foreach(BsonDocument document in documents)
        {
            Debug.Log(document["title"]);
        }

        // 6-3. 2002년도에 개봉한 영화 5개 찾기
        // => '값으로 쿼리' 를 사용한 방식
        BsonDocument filter = new BsonDocument();
        filter["year"] = 2002;
        var data2002 = movieCollection.Find(filter).Limit(5).ToList();
        foreach (BsonDocument document in data2002)
        {
            Debug.Log($"2002년도에 개봉한 영화 : {document["title"]}");
        }

        // => **'필터 쿼리'를 사용한 방식 ** 필터쿼리는 비교 연산자가 있다
        // 필터 쿼리 방식 선호
        var filter2 = Builders<BsonDocument>.Filter.Eq("year", 2002);
        var data2001 = movieCollection.Find(filter2).Limit(5).ToList();
        foreach (BsonDocument document in data2001)
        {
            Debug.Log($"2001년도에 개봉한 영화 : {document["title"]}");
        }

        // 7. 논리 연산자(And, Or, Not)
        //if((1992 <= year && year <= 2002))
        var filter1992 = Builders<BsonDocument>.Filter.Gte("year", 1992);
        var filter2002 = Builders<BsonDocument>.Filter.Lte("year", 2002);
        var filterFinal = Builders<BsonDocument>.Filter.And(filter1992, filter2002);
        var data1992_2002 = movieCollection.Find(filterFinal).Limit(5).ToList();
        foreach(var d in data1992_2002)
        {
            Debug.Log($"1992-2022년도 사이에 개봉한 영화 : {d["title"]}");
        }

        // 8. Where 연산자
        var whereFilter = Builders<BsonDocument>.Filter.Where(d => 1992 <= d["year"] && d["year"] <= 2002);
        var data1992_20022 = movieCollection.Find(whereFilter).Limit(5).ToList();
        foreach (var d in data1992_20022)
        {
            Debug.Log($"1992-2022년도 사이에 개봉한 영화2 : {d["title"]}");
        }

        // 9. 람다식 (**제일 많이 쓰는 방식)
        var finalData = movieCollection.Find(d => 1992 <= d["year"] && d["year"] <= 2002).Limit(5).ToList();
        foreach (var d in finalData)
        {
            Debug.Log($"1992-2022년도 사이에 개봉한 영화3 : {d["title"]}");
        }
    }
}
