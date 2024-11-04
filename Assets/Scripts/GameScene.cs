using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��ת������ǰ�ű����岻ɾ��
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
        //ע�����ñ�
        RegisterConfigs();

        GameAPP.ConfigManager.LoadAllConfigs(); //�������ñ�

        //�������ñ�
        ConfigData itemData = GameAPP.ConfigManager.GetConfigData("enemy");
        string name = itemData.GetDataById(10001)["Name"];
        Debug.Log(name);
        //���ű�������
        GameAPP.SoundManager.PlayBGM("login");
        RegisterModule();//ע����Ϸ�еĿ�����
        InitModule();
    }

    //ע�������
    void RegisterModule()
    {
        GameAPP.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameAPP.ControllerManager.Register(ControllerType.Game, new GameController());
        GameAPP.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameAPP.ControllerManager.Register(ControllerType.Level, new LevelController());
    }
    //ִ�����п�������ʼ��
    void InitModule()
    {
        GameAPP.ControllerManager.InitAllModules();
    }

    //ע��
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
