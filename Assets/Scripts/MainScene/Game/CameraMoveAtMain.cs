using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraMoveAtMain:GameSystem
{
    
    public CameraMoveAtMain(MainFacade MF) : base(MF)
    {

    }
    //根节点
    GameObject m_root;
    GameObject m_BuildingRoot;
    //摄像机
    public Camera m_MainCamera;//主摄像机
    public CinemachineVirtualCamera m_MainVCamera;//主虚拟摄像机

    public Dictionary<Enum_Building,CinemachineVirtualCamera> VCMS = null;//建筑摄像机
    public CinemachineVirtualCamera m_StartVCamera;//开场摄像机

    //参数
    private float sensitivityAmt;   //灵活度

    //初始化
    public override void Initialize()
    {
        //根节点
        m_root =  UnityTool.FindGameObject("Root");


        //主摄像机
        m_MainCamera = UnityTool.FindChildGameObject(m_root, "Main Camera").GetComponent<Camera>();
        m_MainVCamera = UnityTool.FindChildGameObject(m_root, "CM MainVcam").GetComponent<CinemachineVirtualCamera>();
        //开场摄像机
        m_StartVCamera = UnityTool.FindChildGameObject(m_root, "CM StartVcam").GetComponent<CinemachineVirtualCamera>();

        VCMS = new Dictionary<Enum_Building, CinemachineVirtualCamera>();
        //绑定各个建筑的虚拟摄像机
        BindVCam(Enum_Building.TianAnMen, "tiananmen");
        BindVCam(Enum_Building.GuLou, "gulou");
        BindVCam(Enum_Building.Tiantan, "tiantan");
        BindVCam(Enum_Building.Zhonglou, "zhonglou");
        BindVCam(Enum_Building.RenMinYingXiongStela, "renminyingxiongStela");
        BindVCam(Enum_Building.MaoZhuXiMemorialHall, "maozhuxijiniantang");
        BindVCam(Enum_Building.ZhengYangMen, "zhengyangmen");
        BindVCam(Enum_Building.YongDingMen, "yongdingmen");
        BindVCam(Enum_Building.JianLou, "jianlou");
        BindVCam(Enum_Building.JingShan, "jingshan");

        //参数
        sensitivityAmt = 2.0f;
    }
    private void BindVCam(Enum_Building building,string buildingobjname)
    {
        m_BuildingRoot = UnityTool.FindChildGameObject(m_root, buildingobjname);
        if (!m_BuildingRoot) {Debug.LogWarning("场景中不存在建筑："+ buildingobjname); return; }
        VCMS.Add(building, UnityTool.FindChildGameObject(m_BuildingRoot, "CM BuildingVcam").GetComponent<CinemachineVirtualCamera>());
    }

    //结束
    public override void Release() { }
    //更新
    public override void Update() { }
    //隐藏开场摄像机
    public void HideStartCamera()
    {
        m_StartVCamera.gameObject.SetActive(false);
        
    }
    //摄像机拖动
    public void DragCamera()
    {
        Vector3 p0 = m_MainVCamera.transform.position;
        Vector3 p01 = m_MainVCamera.transform.right * Input.GetAxisRaw("Mouse X") * sensitivityAmt * Time.timeScale;
        Vector3 p03 = m_MainVCamera.transform.forward * Input.GetAxisRaw("Mouse Y") * sensitivityAmt * Time.timeScale;
        m_MainVCamera.transform.position = p0 - p03;
        if (m_MainVCamera.transform.position.z > 338) m_MainVCamera.transform.position = new Vector3(m_MainVCamera.transform.position.x, m_MainVCamera.transform.position.y, 338f);
        if (m_MainVCamera.transform.position.z < -370) m_MainVCamera.transform.position = new Vector3(m_MainVCamera.transform.position.x, m_MainVCamera.transform.position.y, -370f); ;
    }

    //相机切换
    public void ShowBuilding(Enum_Building building)
    {
        if (!VCMS[building])
        {
            Debug.LogError("不存在该建筑的VCM");
            return;
        }
        VCMS[building].gameObject.SetActive(true);
    }
    public void HideAll()
    {
        foreach (KeyValuePair<Enum_Building,CinemachineVirtualCamera> item in VCMS)
        if (item.Value.gameObject.activeSelf)
                item.Value.gameObject.SetActive(false);
    }
    //关闭渐变
    public void ShutDownGradient()
    {
        m_MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        
    }
    //打开渐变
    public void OpenGradient()
    {
        m_MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
    }
}
