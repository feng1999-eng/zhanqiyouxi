using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//地图管理器 存储地图网格的信息
public class MapManager
{
    private Tilemap tileMap;
    public Block[,] mapArr;

    public int RowCount;
    public int ColCount;
    
    //初始化地图信息
    public void Init()
    {
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
        //地图大小 可以将这个信息写到配置表进行设置
        RowCount = 12;
        ColCount = 20;
        
        mapArr = new Block[RowCount, ColCount];
        
        List<Vector3Int> tempPosArr = new List<Vector3Int>(); //临时记录瓦片地图每个格子的位置
        //遍历瓦片地图
        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }
        
        //将一维数组的位置转换成二维数组的Block 进行存储
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i < tempPosArr.Count; i++)
        {
            int row = i / ColCount;
            int col = i % ColCount;
            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            b.RowIndex = row;
            b.ColIndex = col;
            b.transform.position = tileMap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);
            mapArr[row, col] = b;
        }
    }

    public BlockType GetBlockType(int row, int col)
    {
        return mapArr[row, col].Type;
    }

    public void ChangeBlockType(int row, int col, BlockType type)
    {
        mapArr[row, col].Type = type;
    }
    //显示移动的区域
    public void ShowStepGrid(ModelBase model, int step)
    {
        BFS bfs = new BFS(RowCount, ColCount);
        
        List<BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.green);
        }
    }
    //隐藏移动的区域
    public void HideStepGrid(ModelBase model, int step)
    {
        BFS bfs = new BFS(RowCount, ColCount);
        
        List<BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
        }
    }
}
