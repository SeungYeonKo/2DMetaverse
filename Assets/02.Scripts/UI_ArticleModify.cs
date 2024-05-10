using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleModify : MonoBehaviour
{
    public static UI_ArticleModify Instance { get; private set; }
    public Toggle NoticeToggleUI;
    public InputField ContentInputFieldUI;

    private Article _article;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(in Article article)
    {
        _article = article;

        NoticeToggleUI.isOn = _article.ArticleType == ArticleType.Notice;
        ContentInputFieldUI.text = _article.Content;

        gameObject.SetActive(true);
    }


    public void OnClickExitButton()
    {
        UI_ArticleList.Instance.Show();
        gameObject.SetActive(false);
    }

    public void OnClickWriteButton()
    {
        _article.ArticleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Normal;
        _article.Content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(_article.Content))
        {
            return;
        }

        ArticleManager.Instance.Replace(_article);
        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Show();
        gameObject.SetActive(false);
    }
}