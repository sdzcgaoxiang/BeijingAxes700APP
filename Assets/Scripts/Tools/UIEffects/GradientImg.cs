using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//图片渐变工具 挂载即可用
namespace GradientTool
{
    public class GradientImg : MonoBehaviour
    {
        public float m_GradientTime = 1.0f;//渐变总时间
        public float m_Gtime;               //当前时间
        public Image m_Image = null;       //渐变图片
        private float MaxAlpha = 1f;     //最大透明度
        private float MinAlpha = 0f;       //最小透明度
        private float m_Alpha;             //当前透明度
        void Awake()
        {

        }
        // Start is called before the first frame update
        void OnEnable()
        {
            m_Image = GetComponent<Image>();
            if (m_Image == null)
            {
                Debug.LogError("该GameObject不存在Image");
            }

            //激活时渐变
            m_Gtime = m_GradientTime;
            //透明度设置
            m_Alpha = MinAlpha;
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, m_Alpha);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Gtime > 0 && m_Alpha <= MaxAlpha)
            {
                m_Alpha += (MaxAlpha - MinAlpha) * Time.deltaTime / m_GradientTime;
                m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, m_Alpha);
                m_Gtime -= Time.deltaTime;
            }
            else { m_Gtime = 0; m_Alpha = MaxAlpha; }

        }
    }
}