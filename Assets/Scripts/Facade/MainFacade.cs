using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


//建筑标记常量
public enum Enum_Building
{
    TianAnMen = 0,
    GuLou = 1,
    Tiantan = 2,
    Zhonglou = 3,
    RenMinYingXiongStela = 4,
    MaoZhuXiMemorialHall = 5,
    ZhengYangMen = 6,
    JianLou = 7,
    YongDingMen = 8,
    JingShan =9,
    XianNongTan = 10,
    TianAnMenSquare = 11,
    GuGong = 12,
    DiAnMen = 13,


}

public class MainFacade:IFacade
{
    //输入管理
    //private float main_time;    //检测长按
    //public float click_time;    //检测单击事件
    //private float two_click_time; //检测双击事件
    //private int click_count;
    //协程
    private Mono m_Mono;//协程
    private GameObject m_MonoObject;//协程挂载的物体
    //Game系统
    private CameraMoveAtMain m_CameraMoveAtMain = null; //主界面摄像机移动系统
    
    //UI系统
    private MainUI m_MainUI = null;//主界面
    private BuildingsUI m_BuildingsUI = null;//各景点界面
    private AchievementUI m_AchievementUI = null;//成就界面
    //射线
    Ray m_ray;
    RaycastHit m_hit;
    //初始化判断
    private bool isStart = false;


    public MainFacade()
    { }

    public override void Initinal()
    {
        base.Initinal();
        //添加协程
        m_MonoObject = new GameObject("mono");
        m_MonoObject.AddComponent<Mono>();
        m_Mono = m_MonoObject.GetComponent<Mono>();

        //Game系统
        m_CameraMoveAtMain = new CameraMoveAtMain(this);//摄像机控制及其初始化
        m_CameraMoveAtMain.Initialize();                //主界面

        //UI系统
        m_MainUI = new MainUI(this);
        m_MainUI.Initialize();
        m_BuildingsUI = new BuildingsUI(this);
        m_BuildingsUI.Initialize();
        m_AchievementUI = new AchievementUI(this);
        m_AchievementUI.Initialize();

        //UI绑定
        m_BuildingsUI.m_BackBtn.onClick.AddListener(OnClickBackBtn);//返回按钮绑定监听
        m_MainUI.m_MoreBtn.onClick.AddListener(OnClickMoreBtn);//“更多”按钮绑定监听
        m_MainUI.m_BackGroundMaskImg.onClick.AddListener(OnClickBackGroundMask);//点击背景隐藏更多按钮，返回主界面
        m_AchievementUI.m_MainBtn.onClick.AddListener(OnClickAchievementBtn);//成就按钮绑定监听
        m_AchievementUI.m_BackBtn .onClick.AddListener(OnClickAchievementBackBtn);//成就返回按钮绑定监听
        m_MainUI.m_ShareBtn.onClick.AddListener(OnClickShareBtn);//截图保存

        //小地图绑定
        LinkMapPoint();
        BindSmallMapPoint();

        //第一次进入该场景判断
        if (!isStart)
        {
            StartCameraGradient();
            isStart = true;
        }
        else
        {

            m_CameraMoveAtMain.ShutDownGradient();
            StartCameraGradient();
           
        }
    }


    public override void Release() { }


    //主界面更新
    public override void Update()
    {
        InputCrossPlatform();//跨平台输入管理
        SetSmallMap();//小地图设置
    }
    //跨平台输入管理
    protected override void ClickDown() {
        base.ClickDown();
        //如果点击到UI返回
        if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
        m_CameraMoveAtMain.DragCamera();
        m_CameraMoveAtMain.OpenGradient(); 
    }
    protected override void SingleClick()
    {

        //如果点击到UI返回
        if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
        base.SingleClick();
        //参数ray 为射线碰撞检测的光线(返回一个从相机到屏幕鼠标位置的光线)
        m_ray = m_CameraMoveAtMain.m_MainCamera.ScreenPointToRay(Input.mousePosition);
        //射线碰撞检测
        if (Physics.Raycast(m_ray, out m_hit))
        {
           // Debug.Log("HIT TO THE " + m_hit.collider.gameObject.name);//打印鼠标点击到的物体名称

            //函数 点击判断
            if (m_hit.collider.gameObject.name == "tiananmen")
                OnClickBuilding(Enum_Building.TianAnMen);
            else if (m_hit.collider.gameObject.name == "gulou")
                OnClickBuilding(Enum_Building.GuLou);
            else if (m_hit.collider.gameObject.name == "tiantan")
                OnClickBuilding(Enum_Building.Tiantan);
            else if (m_hit.collider.gameObject.name == "zhonglou")
                OnClickBuilding(Enum_Building.Zhonglou);
            else if (m_hit.collider.gameObject.name == "renminyingxiongStela")
                OnClickBuilding(Enum_Building.RenMinYingXiongStela);
            else if (m_hit.collider.gameObject.name == "maozhuxijiniantang")
                OnClickBuilding(Enum_Building.MaoZhuXiMemorialHall);
            else if (m_hit.collider.gameObject.name == "zhengyangmen")
                OnClickBuilding(Enum_Building.ZhengYangMen);
            else if (m_hit.collider.gameObject.name == "jianlou")
                OnClickBuilding(Enum_Building.JianLou);
            else if (m_hit.collider.gameObject.name == "yongdingmen")
                OnClickBuilding(Enum_Building.YongDingMen);
            else if (m_hit.collider.gameObject.name == "jingshan")
                OnClickBuilding(Enum_Building.JingShan);
        }
    }
    protected override void FingerSlide ()
    {
        base.FingerDown();
        //如果点击到UI返回
        if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
        m_CameraMoveAtMain.DragCamera();
        m_CameraMoveAtMain.OpenGradient();
    }
    protected override void FingerClick()
    {

        //如果点击到UI返回
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.currentSelectedGameObject != null) return;
        //参数ray 为射线碰撞检测的光线(返回一个从相机到屏幕鼠标位置的光线)
        //开启过渡
        StartCameraGradient();
        m_ray = m_CameraMoveAtMain.m_MainCamera.ScreenPointToRay(Input.touches[0].position);
        //射线碰撞检测
        if (Physics.Raycast(m_ray, out m_hit))
        {
       
            //函数 点击判断
            if (m_hit.collider.gameObject.name == "tiananmen")
                OnClickBuilding(Enum_Building.TianAnMen);
            else if (m_hit.collider.gameObject.name == "gulou")
                OnClickBuilding(Enum_Building.GuLou);
            else if (m_hit.collider.gameObject.name == "tiantan")
                OnClickBuilding(Enum_Building.Tiantan);
            else if (m_hit.collider.gameObject.name == "zhonglou")
                OnClickBuilding(Enum_Building.Zhonglou);
            else if (m_hit.collider.gameObject.name == "renminyingxiongStela")
                OnClickBuilding(Enum_Building.RenMinYingXiongStela);
            else if (m_hit.collider.gameObject.name == "maozhuxijiniantang")
                OnClickBuilding(Enum_Building.MaoZhuXiMemorialHall);
            else if (m_hit.collider.gameObject.name == "zhengyangmen")
                OnClickBuilding(Enum_Building.ZhengYangMen);
            else if (m_hit.collider.gameObject.name == "jianlou")
                OnClickBuilding(Enum_Building.JianLou);
            else if (m_hit.collider.gameObject.name == "yongdingmen")
                OnClickBuilding(Enum_Building.YongDingMen);
            else if (m_hit.collider.gameObject.name == "jingshan")
                OnClickBuilding(Enum_Building.JingShan);
        }
    }
    protected override void FingerUp()
    {

    }
    //开局过渡
    private void StartCameraGradient()
    {
        m_CameraMoveAtMain.HideStartCamera();
    }
    //点击建筑
    private void OnClickBuilding(Enum_Building building)
    {
        m_CameraMoveAtMain.ShowBuilding(building);
        m_MainUI.hide();
        m_AchievementUI.hide();
        m_BuildingsUI.ShowBuildingUI(building);
        m_BuildingsUI.ShowBackBtn();
    }
   
    private void OnClickJianLou()
    {
        m_CameraMoveAtMain.ShowBuilding(Enum_Building.JianLou);
        m_MainUI.hide();
        m_AchievementUI.hide();
        m_BuildingsUI.ShowBuildingUI(Enum_Building.JianLou);
        m_BuildingsUI.ShowBackBtn();
    }
    //点击返回
    private void OnClickBackBtn()
    {
        m_CameraMoveAtMain.HideAll();
        m_MainUI.show();
        m_AchievementUI.show();
        m_BuildingsUI.HideAllBuildingsUI();
        m_BuildingsUI.HideBackBtn();
    }
    //点击“更多”按钮
    private void OnClickMoreBtn()
    {
        m_MainUI.HideBesidesMoreBtnBtns();
        m_MainUI.ShowMoreBtnBtns();
    }
    //点击“更多”按钮后点击背景
    private void OnClickBackGroundMask()
    {
        m_MainUI.ShowBesidesMoreBtnBtns();
        m_MainUI.HideMoreBtnBtns();
    }
    //点击“成就”按钮
    private void OnClickAchievementBtn()
    {
        m_AchievementUI.ShowMainInterFace();
        m_AchievementUI.HideMainBtn();
    }
    //点击" 返回" 按钮
    private void OnClickAchievementBackBtn()
    {
        m_AchievementUI.HideMainInterFace();
        m_AchievementUI.ShowMainBtn();
    }
    
    //点击“分享”按钮
   // private Texture2D m_SharePhotoTexture;
    private void OnClickShareBtn()
    {
        Rect rect = new Rect(new Vector2(0f, 0f), new Vector2(Screen.width, Screen.height));

        m_Mono.StartCoroutine(CaptureScreenshot2(rect));

      //  Texture2D texture = m_SharePhotoTexture;

    }   
    //纹理读取
    IEnumerator  CaptureScreenshot2(Rect rect)
    {
        
        yield return new WaitForEndOfFrame();
        // 创建一个的空纹理  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        try
        {
            System.IO.File.WriteAllBytes(filename, bytes);
        }
        catch (Exception ex)  {
            Debug.Log("抛出异常+"+ex);
        }

        ShowCapture(screenShot);

    }
    //截屏显示
    private void ShowCapture(Texture2D texture)
    {
        if (texture != null)
        {
            m_MainUI.m_ShareImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400.0f);
            m_MainUI.m_ShareImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 800.0f);
       
            m_MainUI.m_ShareImg.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            m_MainUI.m_ShareImg.gameObject.SetActive(true);
        }
    }

    //确定小地图状态
    private void SetSmallMap()
    {
        float position = GetMainCameraPosition();             //记录在第几个建筑
        float z;//取得z值
        z = (float)(MainUI.MPDistance * position);
        //Debug.Log("Position:" + position + "\n z:" + z);
        m_MainUI.m_MP.transform.localPosition = new Vector3(m_MainUI.m_MP.transform.localPosition.x, z, m_MainUI.m_MP.transform.localPosition.z); //设置指针位置
        m_MainUI.SetPointUnderLine((int)(position+0.5f),true);                //小地图点焦点强调
        m_MainUI.SetPointUnderLine((int)(position + 0.5f)-1, false);
        m_MainUI.SetPointUnderLine((int)(position + 0.5f)+1, false);
    }

    //关联小地图点到建筑摄像机
    private void LinkMapPoint()
    {
        Dictionary<Enum_Building, float> BuildingSortByZ = new Dictionary<Enum_Building, float>();
        //字典赋值
        Dictionary<Enum_Building, CinemachineVirtualCamera> Buildings = m_CameraMoveAtMain.VCMS;
        foreach (KeyValuePair<Enum_Building, CinemachineVirtualCamera> item in Buildings)
        {
            BuildingSortByZ.Add(item.Key, item.Value.transform.position.z);
        }
        //字典按z排序
        m_MainUI.m_BuildingPositionZ = m_MainUI.DictionarySort(BuildingSortByZ);
    }
    //绑定小地图按钮
    private void BindSmallMapPoint()
    {
        int i = 0;
        foreach (var item in m_MainUI.m_MapPoints)
        {
            int temp = i;
            item.onClick.AddListener(
                delegate {
                    OnClickMapPoints(temp);
                });
            i++;
        }
    }
    //点击小地图按钮
    private void OnClickMapPoints(int count)
    {
        Vector3 newPosition = new Vector3(m_CameraMoveAtMain.m_MainVCamera.transform.position.x, m_CameraMoveAtMain.m_MainVCamera.transform.position.y, m_MainUI.m_BuildingPositionZ[count].Value);
        Vector3 oldPosition = m_CameraMoveAtMain.m_MainVCamera.transform.position;
        m_Mono.StartCoroutine(VCameraTransition(m_CameraMoveAtMain.m_MainVCamera,oldPosition, newPosition));
    }

    //摄像机过度
    IEnumerator VCameraTransition(CinemachineVirtualCamera camera, Vector3 oldPosition,Vector3 newPosition)
    {
        Vector3 transitionScale = (newPosition - oldPosition).normalized * 200;  //渐变尺度,每秒100
        while(true)
        {
            camera.transform.position += transitionScale * Time.deltaTime;
            if (Mathf.Abs(newPosition.z - camera.transform.position.z ) <= 5)
                break;
            //yield return new WaitForSeconds(transitionTime / transitionTimes * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
       
    }
    //判断摄像机当前所在位置，返回值为float：反应在第几个建筑的位置
    public float GetMainCameraPosition()
    {
        float NowZ = m_CameraMoveAtMain.m_MainCamera.transform.position.z;  //当前摄像机确切位置
        float NowPosition = 0f;     //记录当前位置
        float Last = 0f;            //上一个item的z值
        bool isAboveMax = true;    //是否比最远的建筑远
        int i = 0;
       
        foreach (KeyValuePair<Enum_Building,float> item in m_MainUI.m_BuildingPositionZ)
        {
            if (i == 0)
            {
                if (NowZ <= item.Value)
                {
                    NowPosition = 0;
                    isAboveMax = false;
                    break;
                }
            }
            if (NowZ <= item.Value)
            {
                NowPosition = i - (item.Value - NowZ) / (item.Value - Last) ;
                isAboveMax = false;
                break;
            }
            else if (NowZ > item.Value)
                Last = item.Value;
            i++;
        }
        if (isAboveMax == true)
            NowPosition = i - 1;
        return NowPosition;
    }
    // 存档
    protected override void SaveData()
    {
    }

    // 取回存档
    protected override  void LoadData()
    {
    }

}
