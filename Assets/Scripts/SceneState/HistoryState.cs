using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryState :ISceneState
{
    public HistoryState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "HistoryState";
    }
    //返回按钮

    public override void StateBegin()
    {
        // 取得返回按键
        Button tmpBtn = UITool.GetUIComponent<Button>("BackBtn");
        tmpBtn.onClick.AddListener(OnBackBtnClick);
        BeijingAxes700.Instance.m_HistoryFacade.Initinal();
    }
    public override void StateEnd()
    {
        BeijingAxes700.Instance.m_HistoryFacade.Release();
    }
    public override void StateUpdate()
    {
        BeijingAxes700.Instance.m_HistoryFacade.Update();
    }
    public void OnBackBtnClick()
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "Main");
    }
}
