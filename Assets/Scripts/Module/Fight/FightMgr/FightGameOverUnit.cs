using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        
        GameAPP.CommandManager.Clear();

        if (GameAPP.FightWorldManager.heros.Count == 0)
        {
            //延迟一点出现界面
            GameAPP.CommandManager.AddCommand(new WaitCommand(1.25f, delegate()
            {
                GameAPP.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if(GameAPP.FightWorldManager.enemies.Count == 0)
        {
            //延迟一点出现界面
            GameAPP.CommandManager.AddCommand(new WaitCommand(1.25f, delegate()
            {
                GameAPP.ViewManager.Open(ViewType.WinView);
            }));
        }
        else
        {
            
        }
    }
        
    public override bool Update(float dt)
    {
        return true;
    }
}
