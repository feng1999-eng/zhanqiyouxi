using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ѡ��ؿ���Ϣ����
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);   
    }

    //�ر�
    private void onCloseBtn()
    {
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callBack = delegate
        {
            //���ؿ�ʼ����
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    //��ʾ�ؿ�����
    public void ShowLevelDes()
    {
        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }
    //�л���ս������
    public void onFightBtn()
    {
        //�رյ�ǰ����
        GameAPP.ViewManager.Close(ViewId);
        //���������λ��λ��
        GameAPP.CameraManager.ResetPos();
        
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName;
        loadingModel.callBack = delegate()
        {
            //���سɹ�����ʾս�������
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
}
