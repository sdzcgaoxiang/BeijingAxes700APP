using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapState :ISceneState
{
    public MapState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MapState";
    }

    public override void StateBegin()
    {
        // 取得返回按键
        Button tmpBtn = UITool.GetUIComponent<Button>("BackBtn");
        tmpBtn.onClick.AddListener(OnBackBtnClick);
        BeijingAxes700.Instance.m_MapFacade.Initinal();

    }
    public override void StateEnd()
    {
        BeijingAxes700.Instance.m_MapFacade.Release();
    }
    public override void StateUpdate()
    {
        BeijingAxes700.Instance.m_MapFacade.Update();
    }
    // 返回
    private void OnBackBtnClick()
    {

        m_Controller.SetState(new MainMenuState(m_Controller), "Main");
    }
}
