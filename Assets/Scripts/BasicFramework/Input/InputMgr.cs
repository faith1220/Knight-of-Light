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

    //�Ƿ�����ر�������
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            //�¼�����ģ�� �ַ�����̧���¼�
            EventCenter.GetInstance().EventTrigger("W������", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("W��̧��", key);
        }
    }

    private void MyUpdate()
    {
        //û�п���������
        if (!isStart)
            return;
        CheckKeyCode(KeyCode.K);
    }
}
