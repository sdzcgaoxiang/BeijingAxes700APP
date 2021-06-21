using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//主接口类，负责各个场景大的接口和全局内容存储

public class BeijingAxes700 
{
    //Sinleton模板
    private static BeijingAxes700 _instance;
    public static BeijingAxes700 Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BeijingAxes700();
            return _instance;
        }
    }
    //协程用Monobehavior
    public MonoBehaviour mono;
    //接口类
    public MainFacade m_MainFacade = null;
    public MapFacade m_MapFacade = null;
    public RoamFacade m_RoamFacade = null;
    public HistoryFacade m_HistoryFacade = null;


    //全局类
    public GameEventSystem m_GameEventSystem = null;
    public AchievementSystem m_AchievementSystem = null;


    //构造
    public BeijingAxes700()
    {
        //仅构造实例，初始化交由状态控制
        m_MainFacade = new MainFacade();
        m_MapFacade = new MapFacade();
        m_RoamFacade = new RoamFacade();
        m_HistoryFacade = new HistoryFacade();

        //实例化并初始化全局类
        m_GameEventSystem = new GameEventSystem(this);
        m_AchievementSystem = new AchievementSystem(this);

    }

    // 注册游戏事件
    public void RegisterGameEvent(ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
    {
        m_GameEventSystem.RegisterObserver(emGameEvent, Observer);
    }

    // 通知游戏事件
    public void NotifyGameEvent(ENUM_GameEvent emGameEvent, System.Object Param)
    {
        m_GameEventSystem.NotifySubject(emGameEvent, Param);
    }

}
