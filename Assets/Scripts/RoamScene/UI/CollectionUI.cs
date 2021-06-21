using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectionUI : UserInterface
{
    public CollectionUI(RoamFacade MF) : base(MF) { }
    public Image m_Content = null;
    public Image m_TitleImg = null;
    public Text m_TitleText = null;
    public Text m_ContentText = null;
    public string Temp = null;
    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("Collection");
        m_Content = UITool.GetUIComponent<Image>(m_RootUI, "Content");
        m_TitleImg = UITool.GetUIComponent<Image>(m_Content.gameObject, "TitleImg");
        m_TitleText = UITool.GetUIComponent<Text>(m_Content.gameObject, "TitleText");
        m_ContentText = UITool.GetUIComponent<Text>(m_Content.gameObject, "ContentText");
     
    }
    public void ShowCollectionUI(Enum_Collection item)
    {
        if (!CollectionIntroSave.Instance.collection.ContainsKey(item))
        {
            Debug.LogWarning("不存在该收集的介绍");
            return;
        }
        m_RootUI.SetActive(true);
        Temp = CollectionIntroSave.Instance.collection[item].imagePath;
        m_TitleImg.sprite = Resources.Load<Sprite>(Temp);
        Temp = CollectionIntroSave.Instance.collection[item].title;
        m_TitleText.text = Temp;
        Temp = CollectionIntroSave.Instance.collection[item].content;
        m_ContentText.text = Temp;
    }
}
