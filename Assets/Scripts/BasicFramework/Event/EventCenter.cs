using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        action += actions;
    }
}
public class EventInfo :IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        action += actions;
    }
}
public class EventCenter : BaseManager<EventCenter>
{
    //key --事件的名字
    //value 对应监听这个事件
    private Dictionary<string, IEventInfo> eventDic =new Dictionary<string, IEventInfo>();

    //添加事件的监听
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))//有事件的情况
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else//没有的情况
        {
            eventDic.Add(name,new EventInfo<T>(action));
        }
    }
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))//有事件的情况
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else//没有的情况
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    //移除事件监听
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }
    //事件触发
    public void EventTrigger<T>(string name,T info)
    {
        if ((eventDic[name] as EventInfo<T>).actions != null)
        {
            (eventDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }
    public void EventTrigger(string name)
    {
        if ((eventDic[name] as EventInfo).actions != null)
        {
            (eventDic[name] as EventInfo).actions.Invoke();
        }
    }

    //清空事件监听
    public void Clear()
    {
        eventDic.Clear();
    }
}
