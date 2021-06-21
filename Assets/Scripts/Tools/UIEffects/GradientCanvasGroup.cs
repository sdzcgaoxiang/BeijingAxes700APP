using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GradientTool
{
    public class GradientCanvasGroup : MonoBehaviour
    {
            
        public float m_GradientTime = 1.0f;//渐变总时间
        private float m_Gtime;               //当前时间
        public CanvasGroup m_CGroup = null;       //渐变组件
        private float MaxAlpha = 1f;     //最大透明度
        private float MinAlpha = 0f;       //最小透明度
        private float m_Alpha;             //当前透明度
                                           // Start is called before the first frame update

        void Awake()
        {
            m_CGroup = GetComponent<CanvasGroup>();
            if (m_CGroup == null)
            {
                Debug.LogError("该GameObject不存在CanvasGroup");
            }
        }
        void OnEnable()
        {

            //激活时渐变
            m_Gtime = m_GradientTime;
            //透明度设置
            m_Alpha = MinAlpha;
            m_CGroup.alpha = m_Alpha;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Gtime > 0 && m_Alpha <= MaxAlpha)
            {
                m_Alpha += (MaxAlpha - MinAlpha) * Time.deltaTime / m_GradientTime;
                m_CGroup.alpha = m_Alpha;
                m_Gtime -= Time.deltaTime;
            }
            else { m_Gtime = 0; m_Alpha = MaxAlpha; }
        }
    }
}