using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//当前场景的mono类，用于协程等操作
public class Mono : MonoBehaviour
{
    
    private string m_Name { set; get; } //场景名

    void Start()
    {
        m_Name = SceneManager.GetActiveScene().name;
    }


}

