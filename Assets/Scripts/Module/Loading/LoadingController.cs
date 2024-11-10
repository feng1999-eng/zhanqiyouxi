using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : BaseController
{
    AsyncOperation asyncOperation;
    public LoadingController() : base()
    {
        GameAPP.ViewManager.Register(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, loadSceneCallBack);
    }

    //加载场景回调
    private void loadSceneCallBack(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);
        //打开加载界面
        GameAPP.ViewManager.Open(ViewType.LoadingView);

        //加载场景
        asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOperation.completed += onLoadedEndCallBack;
    }

    //加载后回调
    private void onLoadedEndCallBack(AsyncOperation op)
    {
        asyncOperation.completed -= onLoadedEndCallBack;
        GameAPP.TimerManager.Register(0.25f, delegate()
        {
            GetModel<LoadingModel>().callBack?.Invoke(); //执行回调
            GameAPP.ViewManager.Close((int)ViewType.LoadingView);//关闭加载界面
        });
    }
}
