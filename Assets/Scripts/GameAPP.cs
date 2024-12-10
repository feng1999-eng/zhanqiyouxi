using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ͳһ������Ϸ�еĹ�����
/// </summary>
public class GameAPP : Singleton<GameAPP>
{
    public static SoundManager SoundManager; //��Ƶ������
    public static ControllerManager ControllerManager; //������������
    public static ViewManager ViewManager; //��ͼ������
    public static ConfigManager ConfigManager; //���ñ�
    public static CameraManager CameraManager; //�����
    public static MessageCenter MessageCenter; //��Ϣ����
    public static TimerManager TimerManager;//ʱ���������
    public static FightWorldManager FightWorldManager;//ս����������
    public static MapManager MapManager;//��ͼ��������
    public static GameDataManager GameDataManager; //��Ϸ���ݹ�����
    public static UserInputManager UserInputManager;//�û����������
    public static CommandManager CommandManager;//���������
    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MessageCenter = new MessageCenter();
        TimerManager = new TimerManager();
        FightWorldManager = new FightWorldManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();    
        UserInputManager = new UserInputManager();
        CommandManager = new CommandManager();
    }

    public override void Update(float dt)
    {
        UserInputManager.Update();
        TimerManager.OnUpdate(dt);
        FightWorldManager.Update(dt);
        CommandManager.Update(dt);
    }
}
