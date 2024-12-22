using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//战斗控制器(战斗相关的界面 事件等)
public class FightController : BaseController
{
    public FightController() : base()
    {
        SetModel(new FightModel(this));//设置战斗数据模型
        GameAPP.ViewManager.Register(ViewType.FightView, new ViewInfo()
        {
            PrefabName = "FightView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        GameAPP.ViewManager.Register(ViewType.FightSelectHeroView, new ViewInfo()
        {
            PrefabName = "FightSelectHeroView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 1
        });
        GameAPP.ViewManager.Register(ViewType.DragHeroView, new ViewInfo()
        {
            PrefabName = "DragHeroView",
            controller = this,
            parentTf = GameAPP.ViewManager.worldCanvasTf,//设置到世界画布
            sortingOrder = 2
        });
        GameAPP.ViewManager.Register(ViewType.TipView, new ViewInfo()
        {
            PrefabName = "TipView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 2
        });
        GameAPP.ViewManager.Register(ViewType.HeroDesView, new ViewInfo()
        {
            PrefabName = "HeroDesView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 2
        });
        GameAPP.ViewManager.Register(ViewType.EnemyDesView, new ViewInfo()
        {
            PrefabName = "EnemyDesView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 2
        });
        GameAPP.ViewManager.Register(ViewType.SelectOptionView, new ViewInfo()
        {
            PrefabName = "SelectOptionView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
        });
        GameAPP.ViewManager.Register(ViewType.FightOptionDesView, new ViewInfo()
        {
            PrefabName = "FightOptionDesView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 3
        });
        GameAPP.ViewManager.Register(ViewType.WinView, new ViewInfo()
        {
            PrefabName = "WinView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 3
        });
        GameAPP.ViewManager.Register(ViewType.LossView, new ViewInfo()
        {
            PrefabName = "LossView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf,
            sortingOrder = 3
        });
        InitModuleEvent();
    }

    public override void Init()
    {
        model.Init();
    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.BeginFight, onBeginFightCallBack);
    }

    private void onBeginFightCallBack(System.Object[] arg)
    {
        //进入战斗
        GameAPP.FightWorldManager.ChangeState(GameState.Enter);
        GameAPP.ViewManager.Open(ViewType.FightView);
        GameAPP.ViewManager.Open(ViewType.FightSelectHeroView);
    }
}
