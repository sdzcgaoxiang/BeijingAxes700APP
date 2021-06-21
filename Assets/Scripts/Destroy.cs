using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Awake()
    {
 

    }
    void OnEnable()
    {
        GameObject[] gameloop = GameObject.FindGameObjectsWithTag("MainLoop");
        if (gameloop.Length > 1)
        {
            for (int i = 0; i < gameloop.Length; i++)
            {
                if (i == 0)//canvasTitlsArray.Length[0]是最早的
                {
                    continue;
                }
                else
                {
                    Destroy(gameloop[i]);
                }
            }
        }
    }

}
