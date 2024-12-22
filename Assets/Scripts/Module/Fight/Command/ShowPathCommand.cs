using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPathCommand : BaseCommand
{
    private Collider2D pre;//鼠标之前检测到的2d碰撞盒
    private Collider2D current; //鼠标当前检测的2d碰撞盒
    AStar aStar;//A星对象
    private AStarPoint start;
    AStarPoint end;
    private List<AStarPoint> prePaths; //之前检测到的路径集合 用来清空用
    
    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        aStar = new AStar(GameAPP.MapManager.RowCount, GameAPP.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        //点击鼠标后 确定移动的位置
        if (Input.GetMouseButtonDown(0))
        {
            if (prePaths.Count != 0 && this.model.Step >= prePaths.Count - 1)
            {
                GameAPP.CommandManager.AddCommand(new MoveCommand(this.model, prePaths));//移动
            }
            else
            {
                GameAPP.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
                
                //不移动直接显示操作选项
                //显示选项界面
                GameAPP.ViewManager.Open(ViewType.SelectOptionView, this.model.data["Event"], (Vector2)this.model.transform.position);
            }
            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);//检测当前鼠标位置是否有2d碰撞盒
        if (current != null)
        {
            //之前的检测碰撞盒和当前的盒子不一致 才进行路经检测
            if (current != pre)
            {
                pre = current;
                Block b = current.GetComponent<Block>();

                if (b != null)
                {
                    //检测到block脚本的物体 进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    aStar.FindPath(start, end, updatePath);
                }
                else
                {
                    //没检测到 将之前的路径清除
                    for (int i = 0; i < prePaths.Count; i++)
                    {
                        GameAPP.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePaths.Clear();
                }
            }
        }
        return false;
    }
    private void updatePath(List<AStarPoint> paths)
    {
        //如果之前已经有路径 要先清除
        if (prePaths.Count != 0)
        {
            for (int i = 0; i < prePaths.Count; i++)
            {
                GameAPP.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }

        if (paths.Count >= 2 && model.Step >= paths.Count - 1)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;
                if (i == 0)
                {
                    dir = GameAPP.MapManager.GetDirection1(paths[i], paths[i + 1]);
                }
                else if (i == paths.Count - 1)
                {
                    dir = GameAPP.MapManager.GetDirection2(paths[i], paths[i - 1]);
                }
                else
                {
                    dir = GameAPP.MapManager.GetDirection3(paths[i - 1], paths[i], paths[i +1]);
                }
                GameAPP.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir,Color.yellow);
            }
        }
        prePaths = paths;
    }
}

