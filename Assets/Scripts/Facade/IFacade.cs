using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IFacade 
{
    //输入管理
    protected float main_time;    //检测长按
    protected float click_time;    //检测单击事件
    protected float two_click_time; //检测双击事件
    protected int click_count;

    //Debug
    protected Image m_Debug;
    protected Text m_DebugText;
    protected void AddDebugWindow()
    {
        //添加debug
        m_Debug = GameObject.Instantiate(Resources.Load<Image>("Prefabs/Debug"));
        m_Debug.transform.parent = UnityTool.FindGameObject("Canvas").transform;
        m_Debug.transform.localPosition = Vector3.zero;
        m_Debug.transform.localScale = Vector3.one;
        m_DebugText = UITool.GetUIComponent<Text>(m_Debug.gameObject,"Text");
    }
    //平台检测
    protected Enum_PlatForm m_PresentPlatForm = Enum_PlatForm.PC;//当前平台
    protected enum Enum_PlatForm
    {
        PC = 0,
        Andiroid = 1,
    }
    protected IFacade() { }
    public virtual void Initinal() {
        //平台检测
        if (Application.platform == RuntimePlatform.Android)
            m_PresentPlatForm = Enum_PlatForm.Andiroid;
        else m_PresentPlatForm = Enum_PlatForm.PC;
    }
    public virtual void  Release() { }
    public virtual void Update() { }
    protected virtual void SaveData() { }
    protected virtual void LoadData() { }
    //跨平台输入管理
    protected void InputCrossPlatform()
    {
        switch (m_PresentPlatForm)
        {
            case Enum_PlatForm.PC:
                InputProcessPC();
                break;
            case Enum_PlatForm.Andiroid:
                InputProcessAndroid();
                break;
        }
    }
    //输入管理(Android)
    protected void InputProcessAndroid()
    {
        if (Input.touchCount == 1)
        {
            if (main_time == 0.0f)
            {
                main_time = Time.time;
            }
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                // 手指按下时，要触发的代码
                FingerDown();
            }
         }
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                // 手指滑动时，要触发的代码 
                FingerSlide();
            }
        }
        if ((Input.touches[0].phase == TouchPhase.Ended)&&(Input.touches[0].phase != TouchPhase.Canceled))
        {
            //手指松开时触发代码
            if (Time.time - main_time < 0.2f)
            {
                if (two_click_time != 0 && Time.time - two_click_time < 0.2f)
                {
                    //双击前判定
                    click_count = 2;
                    Debug.Log("click2times");
                }
                else
                {
                    //如果点击到UI,则返回
                    //单击
                    click_count++;
                    if (click_count == 1)
                    {
                        click_time = Time.time;
                    }
                    //单击函数
                    FingerClick();
                }
                if (click_count == 2
                    && ((click_time != 0 && Time.time - click_time < 0.2f) || (two_click_time != 0 && Time.time - two_click_time < 0.2f)))
                {
                    //双击
                    click_count = 0;
                    //双击时动作
                }
                if (click_count == 2 && (Time.time - click_time > 0.2f || Time.time - two_click_time > 0.2f))//双击状态清零
                {

                    two_click_time = Time.time;
                    click_count = 0;
                }
                main_time = 0.0f;
            }
            //长安抬起
            else
            {
                //抬起函数
                FingerUp();
                main_time = 0.0f;
            }
        }
    }
    //手指按下函数
    protected virtual void FingerDown() { }
    //拖动函数
    protected virtual void FingerSlide() { }
    //手指松开函数
    protected virtual void FingerUp() { }
    //手指单击函数
    protected virtual void FingerClick() { }
    //输入管理(PC)
    protected void InputProcessPC()
    {
        //PC端输入
        //长按
        if (Input.GetMouseButton(0))
        {
            if (main_time == 0.0f)
            {
                main_time = Time.time;
            }
            if (Time.time - main_time > 0.2f)
            {
                //长按时执行的动作
                LongClick();
            }
            //按下时动作
            ClickDown();
        }

        if (Input.GetMouseButtonUp(0))
        {

            //当鼠标抬起时，检测按下到抬起的时间，如果小于0.2f就判断为点击。
            if (Time.time - main_time < 0.2f)
            {

                if (two_click_time != 0 && Time.time - two_click_time < 0.2f)
                {
                    //双击前判定
                    click_count = 2;
                   // Debug.Log("click2times");
                }
                else
                {
                    //如果点击到UI,则返回
                    if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null)    return;
                   //单击
                    click_count++;
                    if (click_count == 1)
                    {
                        click_time = Time.time;
                    }
                    //单击函数
                    SingleClick();
                }
                if (click_count == 2
                    && ((click_time != 0 && Time.time - click_time < 0.2f) || (two_click_time != 0 && Time.time - two_click_time < 0.2f)))
                {
                    //双击
                    click_count = 0;
                    //双击时动作
                }
                if (click_count == 2 && (Time.time - click_time > 0.2f || Time.time - two_click_time > 0.2f))//双击状态清零
                {
                   
                    two_click_time = Time.time;
                    click_count = 0;
                }
                main_time = 0.0f;
            }
            //长安抬起
            else
            {
                LongClickUp();
                main_time = 0.0f;
            }
        }
    }
    //鼠标按下函数
    protected virtual void ClickDown() { }
    //长按函数
    protected virtual void LongClick() { }
    //长按松开函数
    protected virtual void LongClickUp() { }
    //单击函数
    protected virtual void SingleClick() { }


}
