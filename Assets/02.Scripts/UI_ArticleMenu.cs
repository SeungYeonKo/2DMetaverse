using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleMenu : MonoBehaviour
{
    private Article _article;
    public InputField contentInputField;

    public void Show(Article article)
    {
        _article = article;
        gameObject.SetActive(true);
    }

    public void OnClickCancelNoticeButton()
    {
        Debug.Log("공지내리기클릭");
    }
    public void OnClickEditButton()
    {
        Debug.Log("수정하기클릭");
        if (!string.IsNullOrWhiteSpace(contentInputField.text))
        {
            ArticleManager.Instance.Modify(_article.Id, contentInputField.text);
            // 새로고침을 해주어야 변경사항이 반영됨
            ArticleManager.Instance.FindAll();
            UI_ArticleList.Instance.Refresh();
            gameObject.SetActive(false);
        }
    }
    public void OnClickDeleteButton2()
    {
        Debug.Log("삭제하기클릭");
        ArticleManager.Instance.Delete(_article.Id);
        ArticleManager.Instance.FindAll();

        gameObject.SetActive(false);
        UI_ArticleList.Instance.Refresh();
    }
    public void OnClickFavoriteButton()
    {
        Debug.Log("좋아요 +1!");
    }
}
