using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryFacade : IFacade
{



    //Game系统


    //UI系统


    public HistoryFacade()
    { }
    public override void Initinal()
    {
    }


    private void ResigerGameEvent()
    { }

    public override void Release() { }


    //更新
    public override void Update()
    {
        InputProcessPC();//输入管理
    }

    // 存档
    protected override void SaveData()
    {

    }

    // 取回存档
    protected override void LoadData()
    {

    }


}
