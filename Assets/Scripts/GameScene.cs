using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//跳转场景后当前脚本物体不删除
public class GameScene : MonoBehaviour
{
    float dt;
    private static bool isLoaded = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if ( isLoaded)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameAPP.Instance.Init();
        }
    }
    void Start()
    {
        //注册配置表
        RegisterConfigs();

        GameAPP.ConfigManager.LoadAllConfigs(); //加载配置表

        //测试配置表
        ConfigData itemData = GameAPP.ConfigManager.GetConfigData("enemy");
        string name = itemData.GetDataById(10001)["Name"];
        Debug.Log(name);
        //播放背景音乐
        GameAPP.SoundManager.PlayBGM("login");
        RegisterModule();//注册游戏中的控制器
        InitModule();
    }

    //注册控制器
    void RegisterModule()
    {
        GameAPP.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameAPP.ControllerManager.Register(ControllerType.Game, new GameController());
        GameAPP.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameAPP.ControllerManager.Register(ControllerType.Level, new LevelController());
    }
    //执行所有控制器初始化
    void InitModule()
    {
        GameAPP.ControllerManager.InitAllModules();
    }

    //注册
    void RegisterConfigs()
    {
        GameAPP.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameAPP.ConfigManager.Register("level", new ConfigData("level"));
        GameAPP.ConfigManager.Register("option", new ConfigData("option"));
        GameAPP.ConfigManager.Register("player", new ConfigData("player"));
        GameAPP.ConfigManager.Register("role", new ConfigData("role"));
        GameAPP.ConfigManager.Register("skill", new ConfigData("skill"));
    }
    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime; 
        GameAPP.Instance.Update(dt);
    }
}
