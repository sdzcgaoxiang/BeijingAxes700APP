using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{

    protected IFacade m_MF = null;
    public GameSystem(IFacade MF)
    {
        m_MF = MF;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
