using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//进入战斗需要处理的逻辑
public class FightEnter : FightUnitBase
{
    public override void Init()
    {
        //地图初始化
        GameAPP.MapManager.Init();
        GameAPP.FightWorldManager.EnterFight();
    }
}
