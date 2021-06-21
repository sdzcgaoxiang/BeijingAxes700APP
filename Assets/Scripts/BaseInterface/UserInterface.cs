using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface
{
    protected IFacade m_MFacade = null;
    protected GameObject m_RootUI = null;
    private bool m_Active = true;

    public UserInterface(IFacade PBDGame)
    {
        m_MFacade = PBDGame;
    }


    public bool isVisibale()
    {
        return m_Active;
    }

    public virtual void show()
    {
        m_RootUI.SetActive(true);
        m_Active = true;
    }

    public virtual void hide()
    {
        m_RootUI.SetActive(false);
        m_Active = false;
    }
    public virtual void Initialize()
    {

    }
    public virtual void Update()
    {

    }
}
