using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseManager<ResMgr>
{
    //ͬ������
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        //���������һ��GameObject���͵� �Ұ���ʵ������ �ٷ��س�ȥ �ⲿ ֱ��ʹ�ü���
        if(res is GameObject)
        {
          return  GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
    }

    //�첽������Դ
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        //�����첽���ص�Я��
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //Э�̺����������첽����
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