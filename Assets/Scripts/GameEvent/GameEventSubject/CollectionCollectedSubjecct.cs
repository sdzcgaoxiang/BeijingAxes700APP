using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCollectedSubjecct : IGameEventSubject
{
    private Enum_Collection m_CollectionName;
    public CollectionCollectedSubjecct() { }

    //通知收集名字
    public Enum_Collection GetCollectionName()
    {
        return m_CollectionName;
    }
    //通知收集收集品
    public override void SetParam(object Param)
    {
        base.SetParam(Param);
        m_CollectionName = (Enum_Collection)Param;
        Notify();
    }
}
