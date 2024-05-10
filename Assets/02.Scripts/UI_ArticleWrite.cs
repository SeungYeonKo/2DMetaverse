using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public UI_ArticleList ArticleListUI;
    public Toggle NoticeToggleUI;
    public InputField ContentInputFieldUI;

    public void OnClickExitButton()
    {
        ArticleListUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnClickWriteButton()
    {
        ArticleType articleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Notice;
        string content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(content))
        {
            return;
        }
        ArticleManager.Instance.Write(articleType,content);
        FindObjectOfType<UI_ArticleList>().Show();
        gameObject.SetActive(false);
    }
}
