using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCollectedObserverUI : IGameEventObserver
{
    private CollectionCollectedSubjecct m_Subject = null;
    private RoamFacade m_RF = null;
    public CollectionCollectedObserverUI(RoamFacade RF)
    {
        m_RF = RF;
    }
    //设置主题
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as CollectionCollectedSubjecct;
    }

    //更新UI
    public override void Update()
    {
        m_RF.ShowCollectionUI(m_Subject.GetCollectionName());
    }
}
