using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheTianAnMenObserverAchievement :IGameEventObserver
{
    private EnterTianAnMenSubject m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;

    public EnterTheTianAnMenObserverAchievement(AchievementSystem AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }
    //设置主题
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as EnterTianAnMenSubject;
    }

    //通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.EnterTheTAM();
        m_AchievementSystem.ShowAchievementUI(Enum_Achievenment.RoamAtTianAnMen);
    }
}
