using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleMenu : MonoBehaviour
{
    private Article _article;


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
        UI_ArticleModify.Instance.Show(_article);
        gameObject.SetActive(false);
    }
    public void OnClickBackground()
    {
        gameObject.SetActive(false);
    }

    public void OnClickDeleteButton2()
    {
        Debug.Log("삭제하기클릭");
        ArticleManager.Instance.Delete(_article.Id);
        ArticleManager.Instance.FindAll();

        gameObject.SetActive(false);
        UI_ArticleList.Instance.Refresh();
    }
    
}
