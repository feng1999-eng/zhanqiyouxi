using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ModelBase
{
    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        Type = int.Parse(data["Type"]);
    }
    
    //选中
    protected override void OnSelectCallBack(System.Object arg) 
    {
        //玩家回合才能选中
        if (GameAPP.FightWorldManager.state == GameState.Player)
        {
            //不能操作
            if (IsStop == true)
            {
                return;
            }

            if (GameAPP.CommandManager.IsRunningCommand == true)
            {
                return;
            }
            
            //添加显示路径指令
            GameAPP.CommandManager.AddCommand(new ShowPathCommand(this));
            base.OnSelectCallBack(arg);
            GameAPP.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }
    //未选中
    protected override void OnUnSelectCallBack(System.Object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameAPP.ViewManager.Close((int)ViewType.HeroDesView);
    }
}
