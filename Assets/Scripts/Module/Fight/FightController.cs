using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//战斗控制器(战斗相关的界面 事件等)
public class FightController : BaseController
{
    public FightController() : base()
    {
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
        InitModuleEvent();
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
