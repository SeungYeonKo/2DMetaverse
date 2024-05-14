using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class NaverShoppingAPI : MonoBehaviour
{
    public string clientId;
    public string clientSecret;

    async void Start()
    {
        string query = "기계식키보드";
        await GetNaverShoppingData(query);
    }

    private async Task GetNaverShoppingData(string query)
    {
        string url = $"https://openapi.naver.com/v1/search/shop.json?query={Uri.EscapeDataString(query)}";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-Naver-Client-Id", clientId);
            client.DefaultRequestHeaders.Add("X-Naver-Client-Secret", clientSecret);

            try
            {
                HttpResponseMessage response = await 
                    client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ParseJsonResponse(responseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Request error: {e.Message}");
            }
        }
    }

    private void ParseJsonResponse(string jsonResponse)
    {
        ResultObject response = JsonUtility.FromJson<ResultObject>(jsonResponse);

        foreach (Item item in response.items)
        {
            string title = item.title;
            string link = item.link;
            Texture image = item.image;
            int lprice = item.lprice;
            //Debug.Log($"Title: {title}, Link: {link}, Image: {image}, Lowest Price: {lprice}");
            Debug.Log(jsonResponse);
            ResultObject shoppingitem = JsonUtility.FromJson<ResultObject>(jsonResponse);
            Debug.Log(shoppingitem.total);
            Debug.Log(shoppingitem.items.Count);
        }
    }

    [Serializable]
    public class ResultObject
    {
        public DateTime lastBuildDate;
        public long total;
        public int start;
        public int display;


        public List<Item> items;
    }

    [Serializable]
    public class Item
    {
        public string title;
        public string link;
        public Texture image;
        public int lprice;
        public int hprice;
        public string mallName;
        public long productId;
        public int productType;
        public string brand;
        public string maker;
        public string category1;
        public string category2;
        public string category3;
        public string category4;
    }
}