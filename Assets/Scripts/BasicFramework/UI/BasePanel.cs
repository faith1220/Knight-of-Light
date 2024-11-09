using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���ؼ�
/// �ҵ������Լ�����µĿؼ�����
/// ��Ӧ���ṩ��ʾ �����ص���Ϊ
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
    /// ��ʾ�Լ�
    /// </summary>
    public virtual void ShowMe()
    {

    }

    /// <summary>
    /// �����Լ�
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
    /// �õ���Ӧ���ֵĿؼ��ű�
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
    /// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
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
