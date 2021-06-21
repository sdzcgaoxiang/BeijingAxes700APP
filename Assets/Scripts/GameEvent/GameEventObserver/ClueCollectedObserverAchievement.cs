using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueCollectedObserver : IGameEventObserver
{
    private ClueCollectedSubject m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;

    public ClueCollectedObserver(AchievementSystem AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }
    //设置主题
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as ClueCollectedSubject;
    }

    //通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.AddClud();
    }
}
