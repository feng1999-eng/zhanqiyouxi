using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id; //物体Id

    public Dictionary<string, string> data;//数据表

    public int Step; //行动力

    public int Attack; //攻击力

    public int Type; //类型

    public int MaxHp; //最大血量

    public int CurHp; //当前血量
    
    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp; //身体图片渲染组件

    public GameObject stopObj; //停止行动的标记物体

    public Animator ani; //动画组件

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void OnDestroy()
    {
        RemoveEvents();
    }
    //注册事件
    protected virtual void AddEvents()
    {
        GameAPP.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameAPP.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }
    //移除事件
    protected virtual void RemoveEvents()
    {
        GameAPP.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameAPP.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }
    //选中回调
    protected virtual void OnSelectCallBack(System.Object arg)
    {
        //执行未选中
        GameAPP.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        
        GameAPP.MapManager.ShowStepGrid(this, Step);
    }
    //未选中回调
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        GameAPP.MapManager.HideStepGrid(this, Step);
    }
    
}
