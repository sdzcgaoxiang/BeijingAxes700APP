using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapFacade: IFacade
{



    //UI
    MapUI m_MapUI = null;

    public MapFacade()
    { }
    public override void Initinal()
    {
        //实例化UI
        m_MapUI = new MapUI(this);
        //初始化
        m_MapUI.Initialize();
        //通知进入过地图版面事件
        BeijingAxes700.Instance.m_GameEventSystem.NotifySubject(ENUM_GameEvent.EnterTheMap, null);
    }


    private void ResigerGameEvent()
    { }

    public override void Release() { }


    //主界面更新
    public override void Update()
    {
        InputCrossPlatform();//输入管理
    }
    //输入管理
    protected override void LongClick()
    {   
        m_MapUI.DragMap();
    }
    protected override void FingerSlide()
    {
        m_MapUI.DragMap();
    }
  
    // 存档
    protected override void SaveData()
    {

    }

    // 取回存档
    protected override void LoadData()
    {

    }
}
