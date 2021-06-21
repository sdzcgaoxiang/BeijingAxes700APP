using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全局系统 负责存储 成就 收集等
public class GlobalSystem 
{
    protected BeijingAxes700 m_BeijingAxes700 = null;
    public GlobalSystem(BeijingAxes700 BJA)
    {
        m_BeijingAxes700 = BJA;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
