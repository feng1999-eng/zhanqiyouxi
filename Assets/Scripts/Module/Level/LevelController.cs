using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ؿ�������
public class LevelController : BaseController
{
    public LevelController() : base()
    {
        SetModel(new LevelModel()); //��������ģ��
        GameAPP.ViewManager.Register(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf
        });

        InitModuleEvent();
        InitGlobalEvent();
    }
    public override void Init()
    {
        model.Init(); //��ʼ������
    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }
    //ע��ȫ���¼�
    public override void InitGlobalEvent()
    {
        GameAPP.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameAPP.MessageCenter.AddEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);
    }
    private void onOpenSelectLevelView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.SelectLevelView, args);
    }

    private void onShowLevelDesCallBack(System.Object arg)
    {
        LevelModel levelModel = GetModel<LevelModel>();
        levelModel.current = levelModel.GetLevel(int.Parse(arg.ToString()));
        GameAPP.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDes();
    }

    private void onHideLevelDesCallBack(System.Object arg)
    {
        GameAPP.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDes();
    }
}
