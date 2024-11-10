using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//选择英雄面板
public class FightSelectHeroView : BaseView
{
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
