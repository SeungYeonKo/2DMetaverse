using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    void Start()
    {
        // 1. 유추해서 데이터를 담을 수 있는 클래스를 Person 만들어라
        // 2. 리소스 폴더로부터 person.json 내용을 읽어와라
        var textAsset = Resources.Load("person") as TextAsset;
        // 방어 코드
        string jsonText = textAsset.text;
        Debug.Log(jsonText);

        // 3. 클래스 Person의 객체를 만들고 읽어온 내용을 역직렬화해라
        Person person = JsonUtility.FromJson<Person>(jsonText);

        // 4. 클래스 Person의 이름, 나이, 취미들을 Debug.Log로 출력
        Debug.Log(person.Name);
        Debug.Log(person.Age);
        Debug.Log(person.Hobby[0]);
        Debug.Log(person.Hobby[1]);
    }
}
