using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message; //�¼��ֵ�

    protected BaseModel model; //ģ������

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }
    //ע�����õĳ�ʼ������(Ҫ���п�������ʼ����ִ��)
    public virtual void Init()
    {

    }
    //������ͼ
    public virtual void OnLoadView(IBaseView view)
    {

    }
    //����ͼ
    public virtual void OpenView(IBaseView view)
    {

    }
    //�ر���ͼ
    public virtual void CloseView(IBaseView view)
    {

    }
    //ע��ģ���¼�
    public void RegisterFunc(string evnetName, System.Action<object[]> callback)
    {
        if (message.ContainsKey(evnetName))
        {
            message[evnetName] += callback;
        }
        else
        {
            message.Add(evnetName, callback);
        }
    }
    //�Ƴ�ģ���¼�
    public void UnRegisterFunc(string eventName)
    {
        if (message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }
    //������ģ���¼�
    public void ApplyFunc(string evnetName, params object[] args)
    {
        if (message.ContainsKey(evnetName))
        {
            message[evnetName].Invoke(args);
        } 
        else
        {
            Debug.Log("error:" + evnetName);
        }  
    }

    //��������ģ���¼�
    public void ApplyControllerFunc(int controllerKey, string evnetName, params object[] args)
    {
        GameAPP.ControllerManager.ApplyFunc(controllerKey, evnetName, args);
    }
    public void ApplyControllerFunc(ControllerType type,  string evnetName, params object[] args)
    {
        ApplyControllerFunc((int)type, evnetName, args);
    }
    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    public BaseModel GetModel()
    {
        return model;
    }

    public T GetModel<T>() where T : BaseModel
    {
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        return GameAPP.ControllerManager.GetControllerModel(controllerKey);
    }

    //ɾ��������
    public virtual void Destroy()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent();
    }

    //��ʼ��ģ���¼�
    public virtual void InitModuleEvent()
    {

    }
    //�Ƴ�ģ���¼�
    public virtual void RemoveModuleEvent()
    {

    }
    //��ʼ��ȫ���¼�
    public virtual void InitGlobalEvent()
    {

    }
    //�Ƴ�ȫ���¼�
    public virtual void RemoveGlobalEvent()
    {

    }
}
