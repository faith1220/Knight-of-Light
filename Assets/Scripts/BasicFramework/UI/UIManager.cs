using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI�㼶ö��
/// </summary>
public enum E_UI_Layer
{
    Bot,Mid,Top,System
}
/// <summary>
/// UI������
/// 1.����������ʾ�����
/// 2.�ṩ���ⲿ ��ʾ�����صȵȽӿ�
/// </summary>
public class UIManager : MonoBehaviour
{
    public Dictionary<string,BasePanel> panelDic =new Dictionary<string,BasePanel>();

    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    //��������UI��Canvas������ �����Ժ�����ȥʹ����
    public Transform canvas;
    public UIManager() 
    {
        //����Canvas �����������ʱ�򲻱��Ƴ�
        GameObject obj =  ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform;
        GameObject.DontDestroyOnLoad(obj);

        //�ҵ�����
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        //����EventSystem �����������ʱ�򲻱��Ƴ�
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    /// <summary>
    /// ͨ���㼶ö�ٵõ���Ӧ�ĸ�����
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch(layer)
        {
            case E_UI_Layer.Bot:
                return this.bot;
            case E_UI_Layer.Mid:
                return this.mid;
            case E_UI_Layer.Top:
                return this.top;
            case E_UI_Layer.System:
                return this.system;

        }
        return null;

    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">���ű���</typeparam>
    /// <param name="panelName">�����</param>
    /// <param name="layer">��ʾ����һ��</param>
    /// <param name="callBack">�ص�</param>
    public void ShowPanel<T>(string panelName,E_UI_Layer layer,UnityAction<T> callBack=null) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            //������崴������߼�
            if (callBack != null)
                callBack(panelDic[panelName] as T);

            //��������ظ����� ���������� ��ֱ����ʾ ���ûص������� ֱ��return ���ٴ��������첽����
            return;
        }

        ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //������Ϊ Canvas ���Ӷ���
            //����Ҫ�����������λ��
            Transform father = bot;
            switch(layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            //���ø����� �������λ�úʹ�С
            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //�õ�Ԥ�������ϵ����ű�
            T panel =obj.GetComponent<T>();
            //������崴������߼�
            if(callBack !=null)
                callBack(panel);   

            //����������
            panelDic.Add(panelName, panel);
        });
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// �õ�ĳһ���Ѿ���ʾ����� �����ⲿʹ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T:BasePanel
    {
        if(panelDic.ContainsKey(name))
            return panelDic[name] as T;
        return null;
    }

    /// <summary>
    /// ���ؼ�����Զ����¼�����
    /// </summary>
    /// <param name="control">�ؼ�����</param>
    /// <param name="type">�¼�����</param>
    /// <param name="callBack">�¼�����Ӧ����</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger =control.GetComponent<EventTrigger>();
        if(trigger==null)
        {
            trigger =control.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry =new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }
}
