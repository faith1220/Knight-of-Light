using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart;
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    //是否开启或关闭输入检测
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            //事件中心模块 分发按下抬起事件
            EventCenter.GetInstance().EventTrigger("W键按下", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("W键抬起", key);
        }
    }

    private void MyUpdate()
    {
        //没有开启输入检测
        if (!isStart)
            return;
        CheckKeyCode(KeyCode.K);
    }
}
