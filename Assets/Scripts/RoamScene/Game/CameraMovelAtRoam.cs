using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraMoveAtRoam : GameSystem
{
    public CameraMoveAtRoam(RoamFacade MF) : base(MF)
    {

    }
    //根节点
    GameObject m_root;
    GameObject m_BuildingRoot;
    //主角
    public GameObject m_Player;
    //摄像机
    public Camera m_MainCamera;//主摄像机
    public CinemachineVirtualCamera m_MainVCamera;//主虚拟摄像机

    //视角状态
    public bool m_OnMoveView;        //旋转状态
    public bool m_OnMovePosition;    //移动状态

    //参数
    private float sensitivityAmt;   //旋转灵活度
    private float moveSpeed;        //移动速度
    private float jumpHeight;       //跳跃高度
    private const float gravity = 8.0f;  //重力
    private float downVelocity;     //下落速度
    private float maxVelocity;       //最大下落速度
    private float moveVelocity;      //移动速度
    //初始化
    public override void Initialize()
    {
        //根节点
        m_root = UnityTool.FindGameObject("Root");

        //玩家
        m_Player = GameObject.FindGameObjectWithTag("Player");
        //主摄像机
        m_MainCamera = UnityTool.FindChildGameObject(m_root, "Main Camera").GetComponent<Camera>();
        m_MainVCamera = UnityTool.FindChildGameObject(m_root, "CM MainVcam").GetComponent<CinemachineVirtualCamera>();

        //参数
        m_OnMoveView = true;
        m_OnMovePosition = true;
        sensitivityAmt = 2.0f;
        moveSpeed = 0.1f;
        jumpHeight = 5f;
        downVelocity = 0f;
        maxVelocity = 10.0f;
        moveVelocity = 0.6f;
    }
    //结束
    public override void Release() { }
    //更新
    public override void Update() { }

    //第一人称拖动转向
    public void DragCamera()
    {

        if (m_OnMoveView)
        {
            Vector3 p0 = m_Player.transform.position;
            //鼠标位移
            float p01 = Input.GetAxisRaw("Mouse X") * sensitivityAmt * Time.timeScale;
            float p02 = -Input.GetAxisRaw("Mouse Y") * sensitivityAmt * Time.timeScale;
            //角色偏移
            Vector3 newEuler = m_Player.transform.eulerAngles;
            newEuler.x += p02;
            newEuler.y += p01;
            //防止万向节死锁
            if (newEuler.x >= 80.0f && newEuler.x <= 270.0f)
            {
                if ((newEuler.x - 80.0f) < (270.0f - newEuler.x))
                {
                    newEuler.x = 80.0f;
                }
                else
                {
                    newEuler.x = 270.0f;
                }
            }
            m_Player.transform.eulerAngles = newEuler;
        }


    }
    //第一人称移动，输入为方向向量
    public void Move(float vx,float vy)
    {
        //方向向量的模
        float m = Mathf.Sqrt(vx * vx + vy * vy);
        Vector3 p0 = Vector3.zero;
        float deltX = vx / m;
        float deltY = vy / m;
        
        //速度控制
        //x取自身x，z取自身x与世界y叉乘
        if (Mathf.Abs(vx) > 30 || Mathf.Abs(vy) > 30)
        {

            p0 += 2*moveVelocity * m_Player.transform.right * deltX + Vector3.Cross(m_Player.transform.right, Vector3.up) * deltY;
        }
        else if (Mathf.Abs(vx) > 5 || Mathf.Abs(vy) > 5)
        {

            p0 += moveVelocity * m_Player.transform.right * deltX + Vector3.Cross(m_Player.transform.right, Vector3.up) * deltY;
        }
      
        

        // m_Player.transform.position = p0;
        m_Player.GetComponent<CharacterController>().Move(p0 *Time.timeScale* moveSpeed);
    }
    //跳跃
    public void Jump()
    {
        //是否在地面上
        if (m_Player.GetComponent<CharacterController>().isGrounded)
        {
            //动力学给予初速度
            downVelocity -= jumpHeight;
        }

      //  m_Player.GetComponent<CharacterController>().Move(Vector3.zero);
    }

    //空格调试跳跃
    public void JumpBySpace()
    {
        Vector3 p0 = Vector3.zero;
        //是否在地面上
        if (m_Player.GetComponent<CharacterController>().isGrounded)
        {
            downVelocity = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                //动力学给予初速度
                downVelocity -= jumpHeight;
            }
        }
    }

    //重力作用
    public void GravityEffect()
    {
        
        Vector3 p0 = Vector3.zero;
        //重力下落
        downVelocity += gravity * Time.deltaTime;
        //最大速度限制
        if (downVelocity >= maxVelocity) downVelocity = maxVelocity;
        p0.y -= downVelocity * Time.deltaTime;
        m_Player.GetComponent<CharacterController>().Move(p0 * Time.timeScale);
        //是否在地面上

        if (m_Player.GetComponent<CharacterController>().isGrounded && downVelocity > 0)
        {
                 downVelocity = 0f;
        }
    }

}
