using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message; //事件字典

    protected BaseModel model; //模块数据

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }
    //注册后调用的初始化函数(要所有控制器初始化后执行)
    public virtual void Init()
    {

    }
    //加载视图
    public virtual void OnLoadView(IBaseView view)
    {

    }
    //打开视图
    public virtual void OpenView(IBaseView view)
    {

    }
    //关闭视图
    public virtual void CloseView(IBaseView view)
    {

    }
    //注册模板事件
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
    //移除模板事件
    public void UnRegisterFunc(string eventName)
    {
        if (message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }
    //触发本模块事件
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

    //触发其他模块事件
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

    //删除控制器
    public virtual void Destroy()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent();
    }

    //初始化模板事件
    public virtual void InitModuleEvent()
    {

    }
    //移除模板事件
    public virtual void RemoveModuleEvent()
    {

    }
    //初始化全局事件
    public virtual void InitGlobalEvent()
    {

    }
    //移除全局事件
    public virtual void RemoveGlobalEvent()
    {

    }
}
