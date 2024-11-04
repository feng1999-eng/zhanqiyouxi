using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����һЩ��Ϸͨ�õ�ui������(������塢��ʾ��塢 ��ʼ��Ϸ���������������ע��)
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //ע����ͼ

        //��ʼ��Ϸ��ͼ
        GameAPP.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        //�������
        GameAPP.ViewManager.Register(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            sortingOrder = 1,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        //��ʾ���
        GameAPP.ViewManager.Register(ViewType.MessageView, new ViewInfo()
        {
            PrefabName = "MessageView",
            controller = this,
            sortingOrder = 999,
            parentTf = GameAPP.ViewManager.canvasTf
        });
        InitModuleEvent();//��ʼ��ģ���¼�
        InitGlobalEvent();//��ʼ��ȫ���¼�
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView); //ע��򿪿�ʼ���
        RegisterFunc(Defines.OpenSetView, openSetView); //ע��򿪿�ʼ���
        RegisterFunc(Defines.OpenMessageView, openMessageView); //ע��򿪿�ʼ���
    }
    //����ģ��ע������
    private void openStartView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.StartView, args);
    }

    private void openSetView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.SetView, args);
    }

    //����ʾ���
    private void openMessageView(System.Object[] args)
    {
        GameAPP.ViewManager.Open(ViewType.MessageView, args);
    }
}
