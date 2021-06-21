using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 漫游状态
public class RoamState : ISceneState
{
	public RoamState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "RoamState";
	}

	// 开始
	public override void StateBegin()
	{
   

        //初始化接口
        BeijingAxes700.Instance.m_RoamFacade.Initinal();
        //绑定返回按钮
        UITool.FindUIGameObject("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
    }
    //绑定返回按钮进行返回操作
    private void OnClickBackBtn()
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "Main");
    }
	// 结束
	public override void StateEnd()
	{
		
	}
			
	// 更新
	public override void StateUpdate()
	{
        BeijingAxes700.Instance.m_RoamFacade.Update();
;
	}
}
