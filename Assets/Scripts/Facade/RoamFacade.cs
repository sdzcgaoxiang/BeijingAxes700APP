using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class RoamFacade : IFacade
{

    //射线
    Ray m_ray;
    RaycastHit m_hit;

    //Game
    CameraMoveAtRoam m_CameraMoveAtRoam = null;

    //UI
    RoamUI m_RoamUI = null;
    CollectionUI m_CollectionUI = null;
    public RoamFacade()
    { }
    public override void Initinal()
    {
        base.Initinal();
        //game
        m_CameraMoveAtRoam = new CameraMoveAtRoam(this);
        //UI
        m_RoamUI = new RoamUI(this);
        m_CollectionUI = new CollectionUI(this);
        //初始化
        m_CameraMoveAtRoam.Initialize();
        m_RoamUI.Initialize();
        m_CollectionUI.Initialize();
        //绑定按钮事件
        m_RoamUI.m_JumpBtn.onClick.AddListener(PlayerJump);
        m_RoamUI.m_InterActBtn.onClick.AddListener(ClickInterActBtn);
        //参数
        m_CameraMoveAtRoam.m_OnMovePosition = false;
        //通知进入天安门漫游事件
        BeijingAxes700.Instance.m_GameEventSystem.NotifySubject(ENUM_GameEvent.EnterTianAnMen, null);
        //注册收集事件观察者
        BeijingAxes700.Instance.m_GameEventSystem.RegisterObserver(ENUM_GameEvent.TreatureCollected, new CollectionCollectedObserverUI(this));
    }


    private void ResigerGameEvent()
    { }

    public override void Release() { }


    //主界面更新
    public override void Update()
    {
        ExtraEffect(); //其他作用
        InputCrossPlatform();//输入管理
     
    }

    //其他作用
    private void ExtraEffect()
    {
       // m_CameraMoveAtRoam.JumpBySpace();  //空格跳跃(给向上的初速度)

        if (m_CameraMoveAtRoam.m_OnMovePosition == true)
        {
            PlayerMove();
        }
        m_CameraMoveAtRoam.GravityEffect();//重力作用
    }
    //输入管理
    protected override void ClickDown()
    {
        //是否点击到了UI
       
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (EventSystem.current.currentSelectedGameObject == null) return;
            //镜头移动停止
            m_CameraMoveAtRoam.m_OnMoveView = false;
            //是否点击到移动按钮
            if (EventSystem.current.currentSelectedGameObject.name == "MoveOnImg"|| EventSystem.current.currentSelectedGameObject.name == "MoveButtonImg")
            {
                m_CameraMoveAtRoam.m_OnMovePosition = true;
            }
            if (EventSystem.current.currentSelectedGameObject.name == "JumpBtn")
            {
                PlayerJump();
            }

        }
        //视角移动
        m_CameraMoveAtRoam.DragCamera();
    }
    protected override void SingleClick()
    {
        base.SingleClick();
        //释放镜头锁定
        PlayerMoveUIReset();
        m_CameraMoveAtRoam.m_OnMoveView = true;
        m_CameraMoveAtRoam.m_OnMovePosition = false;
    }
    protected override void LongClickUp()
    {
        base.LongClickUp();
        //释放镜头锁定
        PlayerMoveUIReset();
        m_CameraMoveAtRoam.m_OnMoveView = true;
        m_CameraMoveAtRoam.m_OnMovePosition = false;
    }
    protected override void FingerDown()
    {
        //是否点击到了UI
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (EventSystem.current.currentSelectedGameObject == null) return;
            //镜头移动停止
            m_CameraMoveAtRoam.m_OnMoveView = false;
            //是否点击到移动按钮
            if (EventSystem.current.currentSelectedGameObject.name == "MoveButtonImg"|| EventSystem.current.currentSelectedGameObject.name == "MoveOnImg")
            {
                m_CameraMoveAtRoam.m_OnMovePosition = true;
            }
            if (EventSystem.current.currentSelectedGameObject.name == "JumpBtn")
            {
                PlayerJump();
            }

        }
    }
    protected override void FingerSlide()
    {
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
            //视角移动
        m_CameraMoveAtRoam.DragCamera();
    }
    protected override void FingerUp()
    {
        PlayerMoveUIReset();
        m_CameraMoveAtRoam.m_OnMoveView = true;
        m_CameraMoveAtRoam.m_OnMovePosition = false;
    }
    //角色移动
    private void PlayerMove()
    {
        Vector2 touchPos = Vector2.zero;

        //世界坐标转换
        if (m_PresentPlatForm == Enum_PlatForm.Andiroid)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RoamUI.m_MoveButton.transform as RectTransform, Input.touches[0].position, null, out touchPos);
        }
        else if (m_PresentPlatForm == Enum_PlatForm.PC)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RoamUI.m_MoveButton.transform as RectTransform, Input.mousePosition, null, out touchPos);
        }
      
        //偏移调整
        touchPos += new Vector2(-150,-150);

        float distance = Vector2.Distance(Vector2.zero, touchPos);
        //区域限制
        if (distance > 50)
        {
            touchPos = touchPos.normalized * 52;
            m_RoamUI.m_Button.transform.localPosition = touchPos;
        }
        else
        {
            m_RoamUI.m_Button.transform.localPosition = touchPos;
        }

        m_CameraMoveAtRoam.Move(touchPos.x, touchPos.y);

    }
    private void PlayerJump()
    {
        m_CameraMoveAtRoam.Jump();
    }
    private void PlayerMoveUIReset()
    {
        m_RoamUI.m_Button.transform.localPosition = new Vector2(0f,0f);
    }
    //点击交互按钮
    private void ClickInterActBtn()
    {
        //参数ray 为射线碰撞检测的光线(返回一个从相机到屏幕鼠标位置的光线)
        if(m_PresentPlatForm == Enum_PlatForm.PC)
           m_ray = m_CameraMoveAtRoam.m_MainCamera.ScreenPointToRay(Input.mousePosition);
        else if(m_PresentPlatForm == Enum_PlatForm.Andiroid)
            m_ray = m_CameraMoveAtRoam.m_MainCamera.ScreenPointToRay(Input.touches[0].position);
        //射线碰撞检测
        if (Physics.Raycast(m_ray, out m_hit,20f))
        {
            //如果检测到点击到收集物品
            foreach (Enum_Collection item in Enum.GetValues(typeof(Enum_Achievenment)))
            {
                if (m_hit.collider.name == item.ToString())
                {
                    BeijingAxes700.Instance.m_GameEventSystem.NotifySubject(ENUM_GameEvent.TreatureCollected, item);
                    m_hit.collider.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
    //收集物品UI显示(开放给事件系统中观察者的接口)
    public void ShowCollectionUI(Enum_Collection item)
    {
        m_CollectionUI.ShowCollectionUI(item);
    }



}
