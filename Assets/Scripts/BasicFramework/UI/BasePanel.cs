using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板控件
/// 找到所有自己面板下的控件对象
/// 他应该提供显示 或隐藏的行为
/// </summary>
public class BasePanel : MonoBehaviour
{
    private Dictionary<string,List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
    }
    
    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe()
    {

    }

    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe()
    {

    }

    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void OnValueChanged(string toggleName,bool value)
    {

    }

    /// <summary>
    /// 得到对应名字的控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T:UIBehaviour
    {
        if(controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; ++i)
            {
                if (controlDic[controlName] [i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }

        return null;
    }


    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        for (int i = 0; i < controls.Length; i++)
        {
            string objname = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objname))
                controlDic[objname].Add(controls[i]);
            else
                controlDic.Add(objname,new List<UIBehaviour>() { controls[i] });

            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() => 
                {
                    OnClick(objname);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objname, value);
                });
            }
                

        }
    }
}
