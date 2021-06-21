using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTianAnMenSubject : IGameEventSubject
{
    private bool hasEnterTheTAM = false;
    public EnterTianAnMenSubject() { }

    //查询是否已经进入过天安门
    public bool IsHasEnterTheMap()
    {
        return hasEnterTheTAM;
    }
    //通知进入地图板块
    public override void SetParam(object Param)
    {
        base.SetParam(Param);
        hasEnterTheTAM = true;
        Notify();
    }
}
