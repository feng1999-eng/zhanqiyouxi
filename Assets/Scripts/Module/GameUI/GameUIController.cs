using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 处理一些游戏通用的ui控制器(设置面板、提示面板、 开始游戏面板等在这个控制器注册)
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //注册视图

        //开始游戏视图
        GameAPP.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        //设置面板
        GameAPP.ViewManager.Register(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            sortingOrder = 1,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        //提示面板
        GameAPP.ViewManager.Register(ViewType.MessageView, new ViewInfo()
        {
            PrefabName = "MessageView",
            controller = this,
            sortingOrder = 999,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        InitModuleEvent();//初始化模板事件
        InitGlobalEvent();//初始化全局事件
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView); //注册打开开始面板
        RegisterFunc(Defines.OpenSetView, openSetView); //注册打开开始面板
        RegisterFunc(Defines.OpenMessageView, openMessageView); //注册打开开始面板
    }
    //测试模板注册例子
    private void openStartView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.StartView, args);
    }

    private void openSetView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.SetView, args);
    }

    //打开提示面板
    private void openMessageView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.MessageView, args);
    }
}
