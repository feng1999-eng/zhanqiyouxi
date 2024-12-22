using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LossView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("okBtn").onClick.AddListener(delegate()
        {
            //卸载战斗中的资源
            GameAPP.FightWorldManager.ReLoadRes();
            GameAPP.ViewManager.CloseAll();
            
            //切换场景
            LoadingModel load = new LoadingModel();
            load.SceneName = "map";
            load.callBack = delegate()
            {
                GameAPP.SoundManager.PlayBGM("mapbgm");
                GameAPP.ViewManager.Open(ViewType.SelectLevelView);
            };
            Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, load);
        });
    }
}
