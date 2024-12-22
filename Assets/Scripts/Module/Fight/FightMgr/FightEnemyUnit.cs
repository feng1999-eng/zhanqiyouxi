using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameAPP.FightWorldManager.ResetHeros();
        GameAPP.ViewManager.Open(ViewType.TipView, "敌人回合");
        
        GameAPP.CommandManager.AddCommand(new WaitCommand(1.25f));
        
        //敌人移动 使用技能等
        for (int i = 0; i < GameAPP.FightWorldManager.enemies.Count; i++)
        {
            Enemy enemy = GameAPP.FightWorldManager.enemies[i];
            GameAPP.CommandManager.AddCommand(new WaitCommand(0.25f));//等待
            GameAPP.CommandManager.AddCommand(new AiMoveCommand(enemy));//敌人移动
            GameAPP.CommandManager.AddCommand(new WaitCommand(0.25f));//等待
            GameAPP.CommandManager.AddCommand(new SkillCommand(enemy));//敌人使用技能
            GameAPP.CommandManager.AddCommand(new WaitCommand(0.25f));//等待
        }
        //等待一段时间 切换回玩家回合
        GameAPP.CommandManager.AddCommand(new WaitCommand(0.2f, delegate()
        {
            GameAPP.FightWorldManager.ChangeState(GameState.Player);
        }));
    }
}
