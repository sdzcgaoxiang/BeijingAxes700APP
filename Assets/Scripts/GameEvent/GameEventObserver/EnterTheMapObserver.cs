using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheMapObserver :IGameEventObserver
{
    private EnterTheMapSubjecct m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;

    public EnterTheMapObserver(AchievementSystem AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }
    //设置主题
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as EnterTheMapSubjecct;
    }

    //通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.EnterTheMap();
        m_AchievementSystem.ShowAchievementUI(Enum_Achievenment.EnterTheMap);
    }
}
