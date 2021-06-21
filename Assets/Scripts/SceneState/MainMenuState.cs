using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// 主界面状态
public class MainMenuState : ISceneState
{
	public MainMenuState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "MainMenuState";
	}

	// 开始
	public override void StateBegin()
	{

        //初始化接口
        BeijingAxes700.Instance.m_MainFacade.Initinal();
        // 取得地图按键
        Button tmpBtn0 = UITool.GetUIComponent<Button>("MapBtn");
        tmpBtn0.onClick.AddListener(OnMapBtnClick);
        //取得天安门漫游按键
        Button tmpBtn1 = UITool.GetUIComponent<Button>("TianAnMenRoamBtn");
        tmpBtn1.onClick.AddListener(OnClickTianAnMenRoomBtn);
        //取得历史按钮
        Button tmpBtn2 = UITool.GetUIComponent<Button>("HistoryMapBtn");
        tmpBtn2.onClick.AddListener(OnClickHistoryBtn);
    }


    // 结束
    public override void StateEnd()
    {
        //PBaseDefenseGame.Instance.Release();
    }

    // 更新
    public override void StateUpdate()
    {
        // 是否结束
        BeijingAxes700.Instance.m_MainFacade.Update();
        // Render由Unity负责

        // 是否结束
        //if( )
        //	m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene" );


    }
    // 进入地图
    private void OnMapBtnClick()
    {
        
        m_Controller.SetState(new MapState(m_Controller), "Map");
    }
    //天安门漫游
    private void OnClickTianAnMenRoomBtn()
    {
        m_Controller.SetState(new RoamState(m_Controller), "Roam");
    }
    //历史AR界面
    private void OnClickHistoryBtn()
    {
        Debug.Log("enter the history");
        m_Controller.SetState(new HistoryState(m_Controller), "History");
    }
}



