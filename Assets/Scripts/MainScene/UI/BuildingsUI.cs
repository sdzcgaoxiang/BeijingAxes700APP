using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsUI : UserInterface
{
    public Button m_BackBtn = null;

    public BuildingsUI(MainFacade MF) : base(MF) { }

    public List<GameObject> m_buildings = null;

    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("Buildings");

        //初始化返回按钮
        m_BackBtn = UITool.GetUIComponent<Button>(m_RootUI, "BackBtn");


        m_buildings = new List<GameObject>();
        m_buildings.Insert((int)Enum_Building.TianAnMen, UITool.FindUIGameObject( "tiananmen"));
        m_buildings.Insert((int)Enum_Building.GuLou, UITool.FindUIGameObject("gulou"));
        m_buildings.Insert((int)Enum_Building.Tiantan, UITool.FindUIGameObject("tiantan"));
        m_buildings.Insert((int)Enum_Building.Zhonglou, UITool.FindUIGameObject("zhonglou"));
        m_buildings.Insert((int)Enum_Building.RenMinYingXiongStela, UITool.FindUIGameObject("renminyingxiongStela"));
        m_buildings.Insert((int)Enum_Building.MaoZhuXiMemorialHall, UITool.FindUIGameObject("maozhuxijiniantang"));
        m_buildings.Insert((int)Enum_Building.ZhengYangMen, UITool.FindUIGameObject("zhengyangmen"));
        m_buildings.Insert((int)Enum_Building.JianLou, UITool.FindUIGameObject("jianlou"));
        m_buildings.Insert((int)Enum_Building.YongDingMen, UITool.FindUIGameObject("yongdingmen"));
        m_buildings.Insert((int)Enum_Building.JingShan, UITool.FindUIGameObject("jingshan"));
    }
    //建筑显隐
    public void ShowBuildingUI(Enum_Building building) { if (!m_buildings[(int)building]) { Debug.LogError("不存在该建筑的UI"); return; } m_buildings[(int)building].SetActive(true);}
    public void HideBuildingUI(Enum_Building building) { if (!m_buildings[(int)building]) { Debug.LogError("不存在该建筑的UI"); return; } m_buildings[(int)building].SetActive(false); }
    public void HideAllBuildingsUI()
    {
        foreach (GameObject obj in m_buildings)
        {
            obj.SetActive(false);
        }
    }
    //返回按钮显隐
    public void ShowBackBtn() { m_BackBtn.gameObject.SetActive(true); }
    public void HideBackBtn() { m_BackBtn.gameObject.SetActive(false); }
}
