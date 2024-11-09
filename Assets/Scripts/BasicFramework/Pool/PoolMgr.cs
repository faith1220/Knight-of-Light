using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    //������ ������صĸ��ڵ�
    public GameObject fatherObj;
    //���������
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
    /// �����ö���
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name)
    {
        GameObject obj = null;

        if (poolDic.ContainsKey(name) && poolDic[name].Count >0)//�г��벢�ҳ������ж���
        {
            obj = poolDic[name][0];
            poolDic[name].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;//���������ͻ���ص�����һ��
                
        }
        obj.SetActive(true);//���������ʾ
        obj.transform.parent = null;//�Ͽ����ӹ�ϵ
        return obj;
    }

    /// <summary>
    /// ����ʱ���õĶ���
    /// </summary>
    public void PushObj(string name,GameObject obj)
    {
        if (poolDic == null)
            poolObj = new GameObject("Pool");
        //���ø�����Ϊ���ڵ�
        obj.transform.parent = poolObj.transform;

        obj.SetActive(false);//ʧ�����ر�
        obj.SetActive(false);
        //�����г���
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Add(obj);
        }
        //����û�г���
        else
        {
            poolDic.Add(name, new List<GameObject>() { obj });
        }
    }

    /// <summary>
    /// ��ջ���صķ�������Ҫ���ڳ����л�ʱ
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
