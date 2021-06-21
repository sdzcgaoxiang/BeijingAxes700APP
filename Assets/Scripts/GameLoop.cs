using UnityEngine;
using System;
using System.Collections;

// 主循环
public class GameLoop : MonoBehaviour 
{
    // Singleton模板 防止切换场景产生多个MainLoop
    public string StartScene;
    private static GameLoop _instance;
    public static GameLoop Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameLoop();
            return _instance;
        }
    }


    // 场景状态
    SceneStateController m_SceneStateController = new SceneStateController();


	// 
	void Awake()
	{
		// z在切换场景的时候不会被消除
		GameObject.DontDestroyOnLoad( this.gameObject );		 
	}

	// Use this for initialization
	void Start () 
	{
        if (StartScene == "") StartScene = "Main";
        // 设定起始场景
        switch (StartScene)
        {
            case "Main":
                m_SceneStateController.SetStartState(new MainMenuState(m_SceneStateController), StartScene);
                break;
            case "Roam":
                m_SceneStateController.SetStartState(new RoamState(m_SceneStateController), StartScene);
                break;
            case "Map":
                m_SceneStateController.SetStartState(new MapState(m_SceneStateController), StartScene);
                break;
        }
	

    }

	// Update is called once per frame
	void Update () 
	{
		m_SceneStateController.StateUpdate();	
	}
}
