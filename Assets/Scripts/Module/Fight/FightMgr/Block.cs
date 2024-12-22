using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//地图的单元格子
public enum BlockType
{
    Null,//空
    Obstacle,//障碍物
}
public class Block : MonoBehaviour
{
    public int RowIndex; //行坐标
    public int ColIndex; //纵坐标
    public BlockType Type; //格子类型
    private SpriteRenderer selectSp;//选中的格子图片
    private SpriteRenderer gridSp;//网格图片
    private SpriteRenderer dirSp;//移动方向图片
    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();
        
        GameAPP.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameAPP.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    public void OnDestroy()
    {
        GameAPP.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameAPP.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }
    //显示格子
    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }
    //隐藏格子
    public void HideGrid()
    {
        gridSp.enabled = false;
    }
    private void OnSelectCallBack(System.Object arg)
    {
        GameAPP.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        if (GameAPP.CommandManager.IsRunningCommand == false)
        {
            GameAPP.ViewManager.Open(ViewType.FightOptionDesView);
        }
    }
    //未选中
    void OnUnSelectCallBack(System.Object arg)
    {
      dirSp.sprite = null;   
      GameAPP.ViewManager.Close((int)ViewType.FightOptionDesView);
    }
    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    private void Start()
    {
        
    }
    //设置箭头方向的图片资源 和 颜色
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }
}
