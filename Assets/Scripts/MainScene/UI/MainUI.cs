using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class MainUI :UserInterface
{
    public MainUI(MainFacade MF) : base(MF) { }
    //UI组件
    public Button m_MoreBtn;                 //“更多”按钮
    public Button m_BackGroundMaskImg;       //掩膜，掩盖后面场景
    public GameObject m_MoreBtnBtns;         //“更多”按钮包含的三个按钮
    public GameObject m_ScenesChangeBtns;    //有关场景切换的两个按钮
    public Button m_ShareBtn;                //分享按钮
    public Image m_ShareImg;                 //分享图片

    //小地图
    public GameObject m_SmallMap;                       //小地图
    public List<Button> m_MapPoints;                    //小地图点
    public Image m_MapPointer;                          //小地图指针
    public Image m_MP;//小地图点容器
    public static float MPDistance = 40f;//小地图点的距离
//public List<Enum_Building> m_MapPointsToBuildings;  //记录小地图点与按钮对应关系
public List<KeyValuePair<Enum_Building, float>> m_BuildingPositionZ;//建筑z值（纵方向的值）顺序排序
    public override void Initialize()
    {
        
        m_RootUI = UITool.FindUIGameObject("Main");
        m_MoreBtn = UITool.GetUIComponent<Button>(m_RootUI, "MoreBtn");
        m_ScenesChangeBtns = UITool.FindUIGameObject("ScenesChangeBtns");
        m_MoreBtnBtns = UITool.FindUIGameObject("MoreBtnBtns");
        m_BackGroundMaskImg = UITool.GetUIComponent<Button>(m_MoreBtnBtns, "BackGroundMaskImg");
        m_ShareBtn = UITool.GetUIComponent<Button>(m_MoreBtnBtns, "ShareBtn");
        m_ShareImg = UITool.GetUIComponent<Image>(m_MoreBtnBtns, "ShareImg");
        m_SmallMap = UITool.FindUIGameObject("SmallMap");
        
        //m_MapPointsToBuildings = new List<Enum_Building>();
        m_MapPoints = new List<Button>();
        InitMapPoints();
    }
    public GameObject GetRootUI()
    {
        return m_RootUI;
    }
    //初始化小地图
    private void InitMapPoints()
    {
        Image MapPoints = UITool.GetUIComponent<Image>(m_SmallMap, "MapPoints");
        m_MapPointer = UITool.GetUIComponent<Image>(MapPoints.gameObject, "Pointer");//初始化小地图指针
        m_MP = UITool.GetUIComponent<Image>(MapPoints.gameObject, "MP");  //初始化小地图点容器
        Button point ; 
        for (int i = 1; i < 11; i++)
        {
             point = UITool.GetUIComponent<Button>(m_MP.gameObject, ("MP" + i.ToString()));
            if (!point)
                return;
            m_MapPoints.Add(point);

        }
    }

    //字典排序
    public List<KeyValuePair<Enum_Building, float>> DictionarySort(Dictionary<Enum_Building, float> dic)
    {
        if (dic.Count > 0)
        {
            List<KeyValuePair<Enum_Building, float>> lst = new List<KeyValuePair<Enum_Building, float>>(dic);
            lst.Sort(delegate (KeyValuePair<Enum_Building, float> s1, KeyValuePair<Enum_Building, float> s2)
            {
                return s1.Value.CompareTo(s2.Value);
            });
            dic.Clear();
            return lst;
        }
        return null;
    }
    //设置小地图地图点被选中时UI变化
    public void SetPointUnderLine(int count, bool isUnderLine)
    {
        if (count < 0 || count > 9) return;
         Text pointText =  UITool.GetUIComponent<Text>(m_MapPoints[count].gameObject,"Text");
        if (isUnderLine == true)//高亮和取消高亮
        {
            pointText.fontSize = 20;
            pointText.color = new Color(pointText.color.r, pointText.color.g, pointText.color.b, 0.95f);
        }
        else {
            pointText.fontSize = 15;
            pointText.color = new Color(pointText.color.r, pointText.color.g, pointText.color.b, 0.8f);
        }
    }

    //隐藏除了MoreBtn下属按钮外其他按钮
    public void HideBesidesMoreBtnBtns()
    {
        m_ScenesChangeBtns.gameObject.SetActive(false);
        m_MoreBtn.gameObject.SetActive(false);
    }
    //显示除了MoreBtn下属按钮外其他按钮
    public void ShowBesidesMoreBtnBtns()
    {
        m_ScenesChangeBtns.gameObject.SetActive(true);
        m_MoreBtn.gameObject.SetActive(true);
    }
    //显示“更多”按钮下的所有按钮
    public void ShowMoreBtnBtns()
    {
        m_MoreBtnBtns.gameObject.SetActive(true);
    }
    //隐藏“更多”按钮下的所有按钮
    public void HideMoreBtnBtns()
    {
        m_MoreBtnBtns.gameObject.SetActive(false);
        m_ShareImg.gameObject.SetActive(false);
    }
    
}
