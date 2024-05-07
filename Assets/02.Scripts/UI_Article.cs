using UnityEngine;
using UnityEngine.UI;


// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public Image ProfileImageUI;
    public Text ArticleTypeTextUI;
    public Text NameTextUI;
    public Text ContentTextUI;
    public Text LikeTextUI;
    public Text WriteTimeTextUI;

    public void Init(Article article)
    {
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"{article.Like}";
        WriteTimeTextUI.text = $"{article.WriteTime}";
    }
}
