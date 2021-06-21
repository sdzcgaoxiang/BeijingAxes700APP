using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//成就/收集系统
//成就内容
public enum Enum_Achievenment
{
    Null = 0,
    EnterTheMap = 1,        //进入地图板块
    BuildingsBrowse = 2,    //主界面建筑浏览
    ClueCollected = 3,       //收集品收集
    RoamAtTianAnMen  = 4,   //漫游天安门
}
//收集内容
public enum Enum_Collection
{ 
    Null = 0 ,
    YuPei = 1,
    ZheShan = 2,
    Shu = 3,
    ZhuanKuai = 4,

}
public class AchievementSystem : GlobalSystem
{

    //成就参数
    private Dictionary<Enum_Achievenment, bool> m_AchievementHasReach = new Dictionary<Enum_Achievenment, bool>();
    private Dictionary<Enum_Collection, bool> m_CollectionHasCollected = new Dictionary<Enum_Collection, bool>();
    private int m_BuildingsVisitedCount = 0;
    private int m_CollectionsCount = 0;
    private int m_CludsCount = 0;
    public Dictionary<Enum_Achievenment, string> AchievementText = new Dictionary<Enum_Achievenment, string>{ {Enum_Achievenment.EnterTheMap, "纵览中轴"},{ Enum_Achievenment.Null,""}, { Enum_Achievenment.ClueCollected,"线索收集"},{ Enum_Achievenment.RoamAtTianAnMen,"漫游天安门"} };

    public AchievementSystem(BeijingAxes700 BJA ):base(BJA) { Initialize(); }
    
    public override void Initialize()
    {
        //为游戏事件注册成就系统观察者
        m_BeijingAxes700.RegisterGameEvent(ENUM_GameEvent.ClueCollected,new ClueCollectedObserver(this));
        m_BeijingAxes700.RegisterGameEvent(ENUM_GameEvent.EnterTheMap, new EnterTheMapObserver(this));
        m_BeijingAxes700.RegisterGameEvent(ENUM_GameEvent.EnterTianAnMen, new EnterTheTianAnMenObserverAchievement(this));
        //注册收集系统观察者
        m_BeijingAxes700.RegisterGameEvent(ENUM_GameEvent.TreatureCollected, new CollectionCollectedObserverAchievement(this));
        //初始化成就和收集是否完成字典
        foreach (Enum_Achievenment item in Enum.GetValues(typeof(Enum_Achievenment)))
        {
            m_AchievementHasReach.Add(item, false);
        }
        foreach (Enum_Collection item in Enum.GetValues(typeof(Enum_Collection)))
        {
            m_CollectionHasCollected.Add(item, false);
        }


    }
    //是否成就已经达成
    public bool IsAchievementReached(Enum_Achievenment e)
    {
        return m_AchievementHasReach[e];
    }
    //收集品是否被收集
    public bool IsCollectionCollected(Enum_Collection e)
    {
        return m_CollectionHasCollected[e];
    }
    //UI显示成就
    public void ShowAchievementUI(Enum_Achievenment achievement)
    {
        GameObject aUI = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/AchievenmentUI"));
        aUI.transform.parent = UnityTool.FindGameObject("Canvas").transform;
        aUI.transform.position = new Vector3((float)0.7*Screen.width, (float)0.9*Screen.height, 0);
        aUI.transform.localScale = Vector3.one;
        Text atext = UITool.GetUIComponent<Text>(aUI, "AlertMessage ");
        if (AchievementText.ContainsKey(achievement))
        {
            atext.text = AchievementText[achievement];
        }
        aUI.AddComponent<DestroyUIItSelf>();
        DestroyUIItSelf des = aUI.GetComponent<DestroyUIItSelf>();
        des.StartDestroy(5.0f);
    }
    //收集收集品
    public void CollectCollection(Enum_Collection e)
    {
        m_CollectionHasCollected[e] = true;
    }
    public void EnterTheMap()
    {
        m_AchievementHasReach[Enum_Achievenment.EnterTheMap] = true;
    }
    public void EnterTheTAM()
    {
        m_AchievementHasReach[Enum_Achievenment.RoamAtTianAnMen] = true;
    }
    public void AddBuildingVisitedCount()
    {
        m_BuildingsVisitedCount++;
    }
    public void AddCollectionsCount()
    {
        m_CollectionsCount++;
        if (m_CollectionsCount == 4)
        {
            m_AchievementHasReach[Enum_Achievenment.ClueCollected] = true;
        }
    }
    public void AddClud()
    {
        m_CludsCount++;
    }
}
