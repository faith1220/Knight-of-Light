using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    //抽屉中 对象挂载的父节点
    public GameObject fatherObj;
    //对象的容器
    public List<GameObject> poolList;

    public PoolData(GameObject obj,GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
    }
}
public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string,List<GameObject>> poolDic = new Dictionary<string,List<GameObject>>();
    private GameObject poolObj;
    /// <summary>
    /// 往外拿东西
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name)
    {
        GameObject obj = null;

        if (poolDic.ContainsKey(name) && poolDic[name].Count >0)//有抽屉并且抽屉里有东西
        {
            obj = poolDic[name][0];
            poolDic[name].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;//让物体名和缓存池的名字一样
                
        }
        obj.SetActive(true);//激活，让其显示
        obj.transform.parent = null;//断开父子关系
        return obj;
    }

    /// <summary>
    /// 换暂时不用的东西
    /// </summary>
    public void PushObj(string name,GameObject obj)
    {
        if (poolDic == null)
            poolObj = new GameObject("Pool");
        //设置父对象为根节点
        obj.transform.parent = poolObj.transform;

        obj.SetActive(false);//失活，让其关闭
        obj.SetActive(false);
        //里面有抽屉
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Add(obj);
        }
        //里面没有抽屉
        else
        {
            poolDic.Add(name, new List<GameObject>() { obj });
        }
    }

    /// <summary>
    /// 清空缓存池的方法，主要用在场景切换时
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
