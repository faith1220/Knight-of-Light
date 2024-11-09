using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;

public class ScenceMgr : BaseManager<ScenceMgr>
{
    //�л����� ͬ��
    public void LoadScence(string name,UnityAction fun)
    {
        //����ͬ������
        SceneManager.LoadScene(name);
        //������� �Ż�ִ��fun
        fun();
    }

    public void LoadScenceAsyn(string name,UnityAction fun)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }
    //Э���첽���س���
    private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //���Եõ��������ص�һ������
        while (!ao.isDone)
        {
            //�¼���������ַ��������
            EventCenter.GetInstance().EventTrigger("����������",ao.progress);
            //��������½�����
            yield return ao.progress;
        }
        yield return ao;
        //������ɺ� �Ż�ִ��fun
        fun();
    }
}
