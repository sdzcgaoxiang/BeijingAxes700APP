using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : UserInterface
{
    public MapUI(MapFacade MF) : base(MF) { }


    public GameObject m_Map = null;
    public GameObject m_Intro = null;
    public Image m_TopBorder = null;//最大上边界
    public Image m_BottomBorder = null;//最小下边界
    public Dictionary<Enum_Building, Image> m_BuildingIntro = null;
    //参数
    private float sensitivityAmt = 20.0f;
    
    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("Map");
        m_Map = UITool.FindUIGameObject("Map");
        m_Intro = UITool.FindUIGameObject("Intro");
        m_TopBorder = UITool.GetUIComponent<Image>(m_Map, "TopBorder");
        m_BottomBorder = UITool.GetUIComponent<Image>(m_Map, "BottomBorder");
        InitIntro();       

    }
    //初始化介绍栏目
    public void InitIntro()
    {
        m_BuildingIntro = new Dictionary<Enum_Building, Image>();
        InitBuildingIntro(Enum_Building.DiAnMen, "DiAnMenIntro");
        InitBuildingIntro(Enum_Building.TianAnMen, "TianAnMenIntro");
        InitBuildingIntro(Enum_Building.ZhengYangMen, "ZhengYangMenIntro");
        InitBuildingIntro(Enum_Building.Tiantan, "TianTanIntro");
        InitBuildingIntro(Enum_Building.YongDingMen, "YongDingMenIntro");
        InitBuildingIntro(Enum_Building.XianNongTan, "XianNongTanIntro ");
        InitBuildingIntro(Enum_Building.GuLou, "GulouIntro");
        InitBuildingIntro(Enum_Building.Zhonglou, "ZhongLouIntro");
        InitBuildingIntro(Enum_Building.GuGong, "GugongIntro");
        InitBuildingIntro(Enum_Building.TianAnMenSquare, "SquareIntro");
        InitBuildingIntro(Enum_Building.JianLou, "JianLouIntro");
        InitIntroText();
    }

    //初始化建筑介绍
    private void InitBuildingIntro(Enum_Building building,string name)
    {
        Image Building = UITool.GetUIComponent<Image>(m_Intro, name);
        m_BuildingIntro.Add(building, Building);
    }
    //地图拖动
    public void DragMap()
    {
        Vector3 p0 = m_Map.transform.position;
        Vector3 p01 = -m_Map.transform.right * Input.GetAxisRaw("Mouse X") * sensitivityAmt * Time.timeScale;
        Vector3 p03 = -m_Map.transform.up * Input.GetAxisRaw("Mouse Y") * sensitivityAmt * Time.timeScale;
        //Debug.Log("the position is +" + (p0.y - p03.y));

        //Debug.Log("p0.y - p03.y=" +(p0.y - p03.y) );
 
        if (m_BottomBorder.transform.position.y - p03.y  >= 0|| m_TopBorder.transform.position.y - p03.y <= Screen.height)
        {
            return;
        }
        m_Map.transform.position = p0 - p03;
       
    }
    //获取文本
    public void InitIntroText()
    {
        foreach (KeyValuePair<Enum_Building, Image> intro in m_BuildingIntro)
        {
            if (!BuildingIntroSave.Instance.building.ContainsKey(intro.Key))
            {
                Debug.LogWarning("不存在该建筑的介绍");
                continue;
            }
            UITool.GetUIComponent<Text>(intro.Value.gameObject, "TitleText").text = BuildingIntroSave.Instance.building[intro.Key].title;
            UITool.GetUIComponent<Text>(intro.Value.gameObject, "MainText").text = BuildingIntroSave.Instance.building[intro.Key].main;
            UITool.GetUIComponent<Text>(intro.Value.gameObject, "DetialText").text = BuildingIntroSave.Instance.building[intro.Key].detail;
        }
    }
}
