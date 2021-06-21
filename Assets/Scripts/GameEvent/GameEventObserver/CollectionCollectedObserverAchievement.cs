using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCollectedObserverAchievement : IGameEventObserver
{
    private CollectionCollectedSubjecct m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;

    public CollectionCollectedObserverAchievement(AchievementSystem AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }
    //设置主题
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as CollectionCollectedSubjecct;
    }

    //通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.CollectCollection(m_Subject.GetCollectionName());
        m_AchievementSystem.AddCollectionsCount();
    }
}
