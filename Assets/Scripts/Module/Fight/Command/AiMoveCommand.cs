using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敌人移动指令
/// </summary>
public class AiMoveCommand : BaseCommand
{
    private Enemy enemy;
    BFS bfs;
    List<BFS.Point> paths;
    private BFS.Point current;
    private int pathIndex;
    private ModelBase target;//移动到的目标

    public AiMoveCommand(Enemy enemy):base(enemy)
    {
        this.enemy = enemy;
        bfs = new BFS(GameAPP.MapManager.RowCount, GameAPP.MapManager.ColCount);
        paths = new List<BFS.Point>();
    }

    public override void Do()
    {
        base.Do();
        target = GameAPP.FightWorldManager.GetMinDisHero(enemy);//获得最近的英雄
        if(target == null)
        {
            isFinish = true;
        }
        else
        {
            paths = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);
            if (paths == null)
            {
                //没路 可以随机一个点移动
                isFinish = true;
            }
            else
            {
                //将当前敌人的位置设置成null 可移动
                GameAPP.MapManager.ChangeBlockType(this.enemy.RowIndex, this.enemy.ColIndex, BlockType.Null);
            }
        }
    }

    public override bool Update(float dt)
    {
        if (paths.Count == 0)
        {
            return base.Update(dt);
        }
        else
        {
            current = paths[pathIndex];
            if (model.Move(current.RowIndex, current.ColIndex, dt * 5) == true)
            {
                pathIndex++;
                if (pathIndex > paths.Count - 1)
                {
                    enemy.PlayAni("idle");
                    GameAPP.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
                    return true;
                }
            }
        }
        
        model.PlayAni("move");
        return false;
    }
}
