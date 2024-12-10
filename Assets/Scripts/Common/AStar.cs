using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//路径点类
public class AStarPoint
{
    public int RowIndex;
    public int ColIndex;
    public int G;//当前节点到开始点的距离
    public int H;//当前节点到终点的距离
    public int F;//G+H
    public AStarPoint Parent;//找到当前点的父节点

    public AStarPoint(int row, int col)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = null;
    }
    public AStarPoint(int row, int col, AStarPoint parent)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = parent;
    }
    
    //计算G值
    public int GetG()
    {
        int g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null)
        {
            g = g + 1;
            parent = parent.Parent;
        }

        return g;
    }
    
    //计算H值
    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
}

//A星寻路类
public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> opened;//open表
    private Dictionary<string, AStarPoint> closed;//已经查找过的表
    private AStarPoint start;//开始点
    private AStarPoint end;//终点

    public AStar(int rowCount, int colCount)
    {
        this.rowCount = rowCount;
        this.colCount = colCount;
        opened = new List<AStarPoint>();
        closed = new Dictionary<string, AStarPoint>();
    }
    
    //找到open表的路径点
    public AStarPoint IsInOpen(int rowIndex, int colIndex)
    {
        for (int i = 0; i < opened.Count; i++)
        {
            if (opened[i].RowIndex == rowIndex && opened[i].ColIndex == colIndex)
            {
                return opened[i];
            }
        }
        return null;
    }
    
    //某个点是否已经存在closed表
    public bool IsInClosed(int rowIndex, int colIndex)
    {
        if(closed.ContainsKey($"{rowIndex}_{colIndex}"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
     * A星寻路思路
     * 1.将起点添加到open表
     * 2.查找open表中 f值最小的路径点
     * 3.将找到最小f值的点从open表中移除， 并添加到close表
     * 4.将当前的路径点周围的点添加到open表(上下左右的点)
     * 5.判断终点是否在open表，如果不在 从步骤2继续执行逻辑
     */

    public bool FindPath(AStarPoint start, AStarPoint end, System.Action<List<AStarPoint>> findCallback)
    {
        this.start = start;
        this.end = end;
        opened = new List<AStarPoint>();
        closed = new Dictionary<string, AStarPoint>();
        
        //1.将起点添加到open表
        opened.Add(start);
        while (true)
        {
            //2.从open表中获取F值最小的路径点
            AStarPoint current = GetMinFFromInOpen();
            if (current == null)
            {
                //没有路了
                return false;
            }
            else
            {
                //3.1 从open列表中移除
                opened.Remove(current);
                //3.2 添加到close表
                closed.Add($"{current.RowIndex}_{current.ColIndex}", current);
                //4.将当前路径点周围的点加入到open表
                AddAroundInOpen(current);
                //5.判断终点是否在open表
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if (endPoint != null)
                {
                    //找到路径了]
                    findCallback(GetPath(endPoint));
                    return true;
                }
                //将open表排序 从小到大
                opened.Sort(OpenSort);
            }
        }
    }

    public int OpenSort(AStarPoint a, AStarPoint b)
    {
        return a.F - b.F;
    }
    public List<AStarPoint> GetPath(AStarPoint point)
    {
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null)
        {
            paths.Add(parent);
            parent = parent.Parent;
        }
        paths.Reverse();
        return paths;
    }
    public void AddAroundInOpen(AStarPoint current)
    {
        //上
        if (current.RowIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex - 1, current.ColIndex);
        }
        //下
        if (current.RowIndex + 1 < rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColIndex);
        }
        //左
        if (current.ColIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex, current.ColIndex- 1);
        }
        //右
        if (current.ColIndex + 1 < colCount)
        {
            AddOpen(current, current.RowIndex, current.ColIndex + 1);
        }
    }

    public void AddOpen(AStarPoint current, int row, int col)
    {
        //不在open表 不在close表 对应格子类型不能为障碍物 才能加入open
        if (IsInClosed(row, col) == false && IsInOpen(row, col) == null &&
            GameAPP.MapManager.GetBlockType(row, col) == BlockType.Null)
        {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            opened.Add(newPoint);
        }
    }
    public AStarPoint GetMinFFromInOpen()
    {
        if (opened.Count == 0)
        {
            return null;
        }
        return opened[0];//open表会排序 最小f值在第一位 所以返回第一位的路径点
    }
}
