using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallMapUI: UserInterface
{
    public SmallMapUI(RoamFacade MF) :base(MF) { }

    public GameObject m_MoveButton = null;//移动大按钮
    public Button m_Button = null;//移动中心按钮
    public Button m_JumpBtn = null;//跳跃按钮
    public Image m_Img = null;
    public bool m_isPress = false;//按钮是否按下
    

    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("RoamUI");
        m_MoveButton = UITool.FindUIGameObject("MoveBtn");
        m_Img = UITool.GetUIComponent<Image>(m_MoveButton, "MoveButtonImg");
        m_Button = UITool.GetUIComponent<Button>(m_Img.gameObject, "MoveOnImg");
        m_JumpBtn = UITool.GetUIComponent<Button>(m_RootUI, "JumpBtn");
    }


}
