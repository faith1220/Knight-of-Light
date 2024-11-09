using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instace;
    public static T GetInstance()
    {
        if (instace == null)
        {
            GameObject obj = new GameObject();
            //设置对象的名字为脚本
            obj.name = typeof(T).ToString();
            instace = obj.AddComponent<T>();
        }
        return instace;
    }

}
