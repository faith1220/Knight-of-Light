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

    //给外部提供的添加帧更新时间的函数
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    
    //提供给外部用于移除帧更新事件函数
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
}

