using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 场景状态控制器
public class SceneStateController
{ 
	private ISceneState m_State;	                //状态接口
	private bool 	m_bRunBegin = false;            //是否状态开始
    private AsyncOperation operation;              //异步操作


    public SceneStateController()
	{}

	// 设定状态
	public void SetState(ISceneState State, string LoadSceneName)
	{
		//Debug.Log ("SetState:"+State.ToString());
		m_bRunBegin = false;

        
		// 载入场景
		LoadScene(LoadSceneName);

		// 通知前一個State結束
		if( m_State != null )
			m_State.StateEnd();

		// 设定
		m_State=State;	

	}

    // 载入场景
    private void LoadScene(string LoadSceneName)
    {
        if (LoadSceneName == null || LoadSceneName.Length == 0)
            return;

         SceneManager.LoadScene(LoadSceneName);
        //StartCoroutine(AsyncLoading(LoadSceneName));
        
    }

    // 设定状态
    public void SetStartState(ISceneState State, string LoadSceneName)
    {
        //Debug.Log ("SetState:"+State.ToString());
        m_bRunBegin = false;

        // 通知前一個State結束
        if (m_State != null)
            m_State.StateEnd();

        // 设定
        m_State = State;

    }


    //IEnumerator AsyncLoading(string LoadSceneName)
    //{

    //    Debug.Log("???");
    //    operation = SceneManager.LoadSceneAsync(LoadSceneName);
    //    //阻止当加载完成自动切换
    //    // operation.allowSceneActivation = false;

    //    yield return operation;
    //}


    // 更新
    public void StateUpdate()
	{

        if (Application.isLoadingLevel)
            return;
        // 通知新的State开始
        if ( m_State != null && m_bRunBegin==false)
		{
			m_State.StateBegin();
			m_bRunBegin = true;
		}

		if( m_State != null)
			m_State.StateUpdate();
	}
}
