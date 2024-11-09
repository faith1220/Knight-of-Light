using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseManager<ResMgr>
{
    //同步加载
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        //如果对象是一个GameObject类型的 我把它实例化后 再返回出去 外部 直接使用即可
        if(res is GameObject)
        {
          return  GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
    }

    //异步加载资源
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        //开启异步加载的携程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //协程函数，开启异步加载
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if( r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
        {
            callback(r.asset as T);             
        }
    }
}
