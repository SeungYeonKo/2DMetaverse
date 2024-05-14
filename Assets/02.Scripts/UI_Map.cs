using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_MapManager : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("맵 정보 입력")]
    public string strBaseURL = "";
    public int level = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";
    public string secretKey = "";
    public InputField inputField;
    public Button searchButton;
    private string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode";
 
    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        searchButton.onClick.AddListener(OnSearchButtonClicked);
    }

    void OnSearchButtonClicked()
    {
        string address = inputField.text;
        // 입력한 주소로 좌표를 가져오는 함수 호출
        GetCoordinatesFromAddress(address);
    }

    // 주소로부터 좌표를 가져오는 함수
    public void GetCoordinatesFromAddress(string address)
    {
        StartCoroutine(SendGeocodeRequest(address));
    }

    // 좌표 요청을 보내는 코루틴
    IEnumerator SendGeocodeRequest(string address)
    {
        // 주소를 URL 인코딩하여 요청 URL 생성
        string url = $"{baseUrl}?query={UnityWebRequest.EscapeURL(address)}";

        // GET 요청 생성
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 요청 헤더에 API 키 설정
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        // 요청 보내기
        yield return request.SendWebRequest();

        // 응답 처리
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ProcessGeocodeResponse(request.downloadHandler.text);
        }
    }

    // 좌표 응답을 처리하는 함수
    void ProcessGeocodeResponse(string jsonResponse)
    {
        // JSON 응답을 파싱하여 좌표 추출
        var geocodeResponse = JsonUtility.FromJson<GeocodeResponse>(jsonResponse);
        if (geocodeResponse.addresses != null && geocodeResponse.addresses.Length > 0)
        {
            // 첫 번째 주소의 위도와 경도를 추출
            string latitude = geocodeResponse.addresses[0].y;
            string longitude = geocodeResponse.addresses[0].x;
            Debug.Log($"Latitude: {latitude}, Longitude: {longitude}");

            // 좌표를 기반으로 지도를 로드하는 코루틴 호출
            StartCoroutine(MapLoader(latitude, longitude));
        }
        else
        {
            Debug.LogError("No addresses found in geocode response");
        }
    }

    // 지도를 로드하는 코루틴
    IEnumerator MapLoader(string latitude, string longitude)
    {
        // 요청 URL 생성
        string str = $"{strBaseURL}?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";

        // 텍스처 요청 생성
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        // 요청 헤더에 API 키 설정
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        // 요청 보내기
        yield return request.SendWebRequest();

        // 응답 처리
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    [System.Serializable]
    public class GeocodeResponse
    {
        public Address[] addresses; //주소 배열
    }

    [System.Serializable]
    public class Address
    {
        public string roadAddress;      // 도로명 주소
        public string jibunAddress;     // 지번 주소
        public string englishAddress;  // 영어 주소
        public AddressElement[] addressElements;    // 주소 요소 배열
        public string x;    // 경도
        public string y;    // 위도
        public float distance;  // 거리
    }

    [System.Serializable]
    public class AddressElement
    {
        public string[] types;      // 타입 배열
        public string longName; // 전체 이름
        public string shortName; // 축약 이름
    }
}