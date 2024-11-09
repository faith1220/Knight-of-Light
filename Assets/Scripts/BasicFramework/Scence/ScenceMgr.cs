using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;

public class ScenceMgr : BaseManager<ScenceMgr>
{
    //切换场景 同步
    public void LoadScence(string name,UnityAction fun)
    {
        //场景同步加载
        SceneManager.LoadScene(name);
        //加载完成 才会执行fun
        fun();
    }

    public void LoadScenceAsyn(string name,UnityAction fun)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }
    //协程异步加载场景
    private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //可以得到场景加载的一个进度
        while (!ao.isDone)
        {
            //事件中心向外分发进度情况
            EventCenter.GetInstance().EventTrigger("进度条更新",ao.progress);
            //这里面更新进度条
            yield return ao.progress;
        }
        yield return ao;
        //加载完成后 才会执行fun
        fun();
    }
}
