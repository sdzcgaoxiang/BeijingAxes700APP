using UnityEngine;
using System.Collections.Generic;

// 事件
public enum ENUM_GameEvent
{
	Null  			    = 0,
	ClueCollected	    = 1,     // 线索收集
	TreatureCollected	= 2,     // 宝物收集
    EnterTianAnMen   = 3,         // 进入天安门漫游
    EnterTheMap = 4,             // 进入地图模块
}


// 事件系统
public class GameEventSystem
{
    
	private Dictionary< ENUM_GameEvent, IGameEventSubject> m_GameEvents = new Dictionary< ENUM_GameEvent, IGameEventSubject>();       

	public GameEventSystem(BeijingAxes700 BJA700)
	{
	    
	}
		
	// 释放
	public void Release()
	{
		m_GameEvents.Clear();
	}
		
	// 替某一主题确定观测者
	public void RegisterObserver(ENUM_GameEvent emGameEvnet, IGameEventObserver Observer)
	{
		// 取得事件
		IGameEventSubject Subject = GetGameEventSubject( emGameEvnet );
		if( Subject!=null)
		{
			Subject.Attach( Observer );
			Observer.SetSubject( Subject );
		}
	}

	// 注册一个事件
	private IGameEventSubject GetGameEventSubject( ENUM_GameEvent emGameEvnet )
	{
		// 若存在
		if( m_GameEvents.ContainsKey( emGameEvnet ))
			return m_GameEvents[emGameEvnet];

		// 产生对应的的GameEvent
		IGameEventSubject pSubject= null;
		switch( emGameEvnet )
		{

            case ENUM_GameEvent.ClueCollected:
                pSubject = new ClueCollectedSubject();
                break;
            case ENUM_GameEvent.EnterTheMap:
                pSubject = new EnterTheMapSubjecct();
                break;
            case ENUM_GameEvent.EnterTianAnMen:
                pSubject = new EnterTianAnMenSubject();
                break;
            case ENUM_GameEvent.TreatureCollected:
                pSubject = new CollectionCollectedSubjecct();
                break;
            default:
			Debug.LogWarning("无["+emGameEvnet+"]指定要产生的的Subject类别");
			return null;
		}

		// 加入后并回传
		m_GameEvents.Add (emGameEvnet, pSubject);
		return pSubject;
	}

	// 通知一个GameEvent更新
	public void NotifySubject( ENUM_GameEvent emGameEvnet, System.Object Param)
	{
        // 是否存在
        if (m_GameEvents.ContainsKey(emGameEvnet) == false)
        {
            Debug.Log("Don not exist"+ emGameEvnet);
            return;
        }

		
		m_GameEvents[emGameEvnet].SetParam( Param );
	}
	
}
