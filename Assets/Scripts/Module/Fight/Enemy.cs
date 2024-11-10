using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : ModelBase
{
    protected override void Start()
    {
        base.Start();

        data = GameAPP.ConfigManager.GetConfigData("enemy").GetDataById(Id);
        Id = int.Parse(data["Id"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        Type = int.Parse(data["Type"]);
    }
}
