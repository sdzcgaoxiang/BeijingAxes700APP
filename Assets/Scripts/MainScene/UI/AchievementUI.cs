using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI:UserInterface
{
    private AchievementSystem m_AchievementSystem = null;//成就系统
    public AchievementUI(MainFacade MF) : base(MF) { }
    //UI组件
    //成就UI
    public Button m_MainBtn;        //成就按钮
    public Image m_InterFace;       //成就和收集界面
    public Button m_BackBtn;        //返回按钮
    public Image m_Achievement;     //成就板块
    public Image m_AchievementObjs; //显示成就部分
    public Dictionary<Enum_Achievenment, Image> m_ChildAchievements = null;//子成就
    //收集UI
    public Image m_Collection;      //收集板块
    public Image m_CollectionObjs; //显示收集部分
    public Dictionary<Enum_Collection, Image> m_ChildCollections = null;//子收集
    public override void Initialize()
    {
        m_AchievementSystem = BeijingAxes700.Instance.m_AchievementSystem;//关联成就系统

        m_RootUI = UITool.FindUIGameObject("CollectAndAchieve");
        m_MainBtn = UITool.GetUIComponent<Button>(m_RootUI, "Button");
        m_InterFace = UITool.GetUIComponent<Image>(m_RootUI, "MainInterFace");
        m_BackBtn = UITool.GetUIComponent<Button>(m_RootUI, "BackBtn");
        m_Achievement = UITool.GetUIComponent<Image>(m_InterFace.gameObject, "Achievement");
        m_AchievementObjs = UITool.GetUIComponent<Image>(m_Achievement.gameObject, "AchievementObjs");
        m_ChildAchievements = new Dictionary<Enum_Achievenment, Image>();
        //添加子成就
        m_ChildAchievements.Add(Enum_Achievenment.EnterTheMap, UITool.GetUIComponent<Image>(m_AchievementObjs.gameObject, "EnterTheMap"));
        m_ChildAchievements.Add(Enum_Achievenment.RoamAtTianAnMen, UITool.GetUIComponent<Image>(m_AchievementObjs.gameObject, "EnterTheTAM"));
        m_ChildAchievements.Add(Enum_Achievenment.ClueCollected, UITool.GetUIComponent<Image>(m_AchievementObjs.gameObject, "CollectAllClue"));
        ShowAchievement();

        m_Collection = UITool.GetUIComponent<Image>(m_InterFace.gameObject, "Collection");
        m_CollectionObjs = UITool.GetUIComponent<Image>(m_Collection.gameObject, "CollectionObjs");
        m_ChildCollections = new Dictionary<Enum_Collection, Image>();
        //添加子收集
        m_ChildCollections.Add(Enum_Collection.YuPei, UITool.GetUIComponent<Image>(m_CollectionObjs.gameObject, "YuPei"));
        m_ChildCollections.Add(Enum_Collection.ZheShan, UITool.GetUIComponent<Image>(m_CollectionObjs.gameObject, "ZheShan"));
        m_ChildCollections.Add(Enum_Collection.ZhuanKuai, UITool.GetUIComponent<Image>(m_CollectionObjs.gameObject, "ZhuanKuai"));
        m_ChildCollections.Add(Enum_Collection.Shu, UITool.GetUIComponent<Image>(m_CollectionObjs.gameObject, "Shu"));
        ShowCollection();
    }

    private void ShowAchievement()
    {
        Text temp;
        foreach (KeyValuePair<Enum_Achievenment, Image> item in m_ChildAchievements)
        {
            //item.Value.gameObject.SetActive(true);
            //如果成就已经达成
            if (m_AchievementSystem.IsAchievementReached(item.Key))
            {
                temp = UITool.GetUIComponent<Text>(item.Value.gameObject, "AchieveText");
                temp.text = "（已达成）";
                temp.color = new Color(255, 0, 0);
            }
            item.Value.gameObject.SetActive(true);
        }
    }
    private void ShowCollection()
    {
        Text temp;
        foreach (KeyValuePair<Enum_Collection, Image> item in m_ChildCollections)
        {

            if (m_AchievementSystem.IsCollectionCollected(item.Key))
            {
                UITool.GetUIComponent<Image>(item.Value.gameObject, "CollectionImg").gameObject.SetActive(true);     //图片显示
                temp = UITool.GetUIComponent<Text>(item.Value.gameObject, "CollectionText");                        
                temp.text = CollectionIntroSave.Instance.collection[item.Key].title;                                 //标题显示

            }
        item.Value.gameObject.SetActive(true);
        }
    }


    public void HideMainBtn()
    {
        m_MainBtn.gameObject.SetActive(false);
    }

    public void ShowMainBtn()
    {
        m_MainBtn.gameObject.SetActive(true);
    }

    public void HideMainInterFace()
    {
        m_InterFace.gameObject.SetActive(false);
    }
    public void ShowMainInterFace()
    {
        m_InterFace.gameObject.SetActive(true);
    }
}
