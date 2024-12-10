using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 统一定义游戏中的管理器
/// </summary>
public class GameAPP : Singleton<GameAPP>
{
    public static SoundManager SoundManager; //音频管理器
    public static ControllerManager ControllerManager; //控制器管理器
    public static ViewManager ViewManager; //视图管理器
    public static ConfigManager ConfigManager; //配置表
    public static CameraManager CameraManager; //摄像机
    public static MessageCenter MessageCenter; //消息中心
    public static TimerManager TimerManager;//时间管理中心
    public static FightWorldManager FightWorldManager;//战斗管理中心
    public static MapManager MapManager;//地图管理中心
    public static GameDataManager GameDataManager; //游戏数据管理器
    public static UserInputManager UserInputManager;//用户输入管理器
    public static CommandManager CommandManager;//命令管理器
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
