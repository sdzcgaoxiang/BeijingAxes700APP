using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueCollectedSubject:IGameEventSubject
{

    private int m_ClueCount = 0;
    public ClueCollectedSubject() { }

    //获取线索数
    public int GetClueCount()
    {
        return m_ClueCount;
    }

    //通知获取线索
    public override void SetParam(object Param)
    {
        base.SetParam(Param);
        m_ClueCount++;
        Notify();
    }
}
