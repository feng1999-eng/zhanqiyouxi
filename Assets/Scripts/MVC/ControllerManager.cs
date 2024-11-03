using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// 控制器管理器
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules; //存储控制器的字典

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();
    }
    public void Register(ControllerType type, BaseController ctl)
    {
        Register((int)type, ctl);
    }
    //注册控制器
    public void Register(int controllerKey, BaseController ctl)
    {
        if (_modules.ContainsKey(controllerKey) == false)
        {
            _modules.Add(controllerKey, ctl);
        }
    }

    //执行所有控制器的Init函数
    public void InitAllModules()
    {
        foreach (var item in _modules)
        {
            item.Value.Init();
        }
    }
    //移除控制器
    public void Unregister(int controllerKey)
    {
        if ( _modules.ContainsKey(controllerKey) )
        {
            _modules.Remove(controllerKey);
        }
    }

    //清除
    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }

    }

    public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else
        {
            return null;
        }
    }
}
