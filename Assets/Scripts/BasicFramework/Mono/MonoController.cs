using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(updateEvent != null) 
            updateEvent.Invoke();
    }

    //���ⲿ�ṩ�����֡����ʱ��ĺ���
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    
    //�ṩ���ⲿ�����Ƴ�֡�����¼�����
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
}

