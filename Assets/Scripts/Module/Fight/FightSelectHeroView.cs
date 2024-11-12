using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//选择英雄面板
public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }
    //选完英雄进入到玩家回合
    private void onFightBtn()
    {
        //如果一个英雄都没选 提示玩家 选择
        //ToDo
        if (GameAPP.FightWorldManager.heros.Count == 0)
        {
            Debug.Log("没有选择英雄");
        }
        else
        {
            GameAPP.ViewManager.Close(ViewId);//关闭当前选择英雄界面
            //切换到玩家回合
            GameAPP.FightWorldManager.ChangeState(GameState.Player);
        }
    }
    public override void Open(params object[] args)
    {
        base.Open(args);
        GameObject prefabObj = Find("bottom/grid/item");
        Transform gridTf = Find("bottom/grid").transform;
        for (int i = 0; i < GameAPP.GameDataManager.heros.Count; i++)
        {
            Dictionary<string, string> data = GameAPP.ConfigManager.GetConfigData("player")
                .GetDataById(GameAPP.GameDataManager.heros[i]);
            GameObject obj = Object.Instantiate(prefabObj, gridTf);
            obj.SetActive(true);
            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
