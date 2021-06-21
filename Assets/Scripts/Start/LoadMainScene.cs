using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    private Slider loadingSlider;                 //loading滑动条
    private Text loadingText;                     //显示百分比
    private Text endText;                     //显示按任意键继续
    private float loadingSpeed = 2;              //loading速度
    private float targetValue;
    private AsyncOperation operation;            //异步操作

    void Start()
    {
        GameObject temp = GameObject.Find("Slider");
        loadingSlider = temp.GetComponent<Slider>();
        loadingSlider.value = 0.0f;

        temp = GameObject.Find("PrecentText");
        loadingText = temp.GetComponent<Text>();

        temp = GameObject.Find("EndText");
        endText = temp.GetComponent<Text>();
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync("Main");
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        yield return operation;
    }

    //平滑过渡场景转换
    void Update()
    {
        //Debug.Log(operation.progress);
        targetValue = operation.progress;
        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }
        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }
        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
        if ((int)(loadingSlider.value * 100) == 100)
        {
            endText.text = "点击探寻北京中轴线";
            //输入任意键切换场景
            if (Input.anyKeyDown)
            {
                operation.allowSceneActivation = true;
            }
        }
    }

}
