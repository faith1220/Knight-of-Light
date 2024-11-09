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
            //���ö��������Ϊ�ű�
            obj.name = typeof(T).ToString();
            instace = obj.AddComponent<T>();
        }
        return instace;
    }

}
