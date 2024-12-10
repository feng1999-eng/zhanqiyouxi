using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动指令
public class MoveCommand : BaseCommand
{
    private List<AStarPoint> paths;
    private AStarPoint current;
    private int pathIndex;
    
    //移动前的行列坐标 撤销用
    private int preRowIndex;
    private int preColIndex;
    
    public MoveCommand(ModelBase model):base(model)
    {
        
    }
    
    public MoveCommand(ModelBase model, List<AStarPoint> paths) : base(model)
    {
        this.paths = paths;
        pathIndex = 0;
    }

    public override void Do()
    {
        base.Do();
        this.preColIndex = this.model.ColIndex;
        this.preRowIndex = this.model.RowIndex;
        //设置当前所占的格子为null
        GameAPP.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
    }

    public override bool Update(float dt)
    {
        current = this.paths[pathIndex];
        if (this.model.Move(current.RowIndex, current.ColIndex, dt * 5))
        {
            pathIndex++;
            if (pathIndex > paths.Count - 1)
            {
                //到达目的地
                this.model.PlayAni("idle");
                GameAPP.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
                return true;
            }
        }
        this.model.PlayAni("move");
        return false;
    }
    
    public override void Undo()
    {
        base.Undo();
        //回到之前的位置
        Vector3 pos = GameAPP.MapManager.GetBlockPos(this.preRowIndex, this.preColIndex);
        pos.z = this.model.transform.position.z;
        this.model.transform.position = pos;
        GameAPP.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
        this.model.RowIndex = preRowIndex;
        this.model.ColIndex = preColIndex;
        GameAPP.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
    }
}
