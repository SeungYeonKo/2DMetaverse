using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public UI_ArticleList ArticleListUI;
    public Toggle NoticeToggleUI;
    public InputField ContentInputFieldUI;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        ArticleListUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnClickWriteButton()
    {
        ArticleType articleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Normal;
        string content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        ArticleManager.Instance.Write(articleType, content);
        ArticleListUI.Show();
        gameObject.SetActive(false);
    }
}
