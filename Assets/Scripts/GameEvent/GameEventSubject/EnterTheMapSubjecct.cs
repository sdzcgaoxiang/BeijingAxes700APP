using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheMapSubjecct : IGameEventSubject
{
    private bool hasEnterTheMap = false;
    public EnterTheMapSubjecct() { }

    //查询是否已经进入过地图板块
    public bool IsHasEnterTheMap()
    {
        return hasEnterTheMap;
    }
    //通知进入地图板块
    public override void SetParam(object Param)
    {
        base.SetParam(Param);
        hasEnterTheMap = true;
        Notify();
    }
}
