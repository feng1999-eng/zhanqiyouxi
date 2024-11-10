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

    //���س����ص�
    private void loadSceneCallBack(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);
        //�򿪼��ؽ���
        GameAPP.ViewManager.Open(ViewType.LoadingView);

        //���س���
        asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOperation.completed += onLoadedEndCallBack;
    }

    //���غ�ص�
    private void onLoadedEndCallBack(AsyncOperation op)
    {
        asyncOperation.completed -= onLoadedEndCallBack;
        GameAPP.TimerManager.Register(0.25f, delegate()
        {
            GetModel<LoadingModel>().callBack?.Invoke(); //ִ�лص�
            GameAPP.ViewManager.Close((int)ViewType.LoadingView);//�رռ��ؽ���
        });
    }
}
