using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hero : ModelBase, ISkill
{
    public SkillProperty skillPro { get; set; }
    
    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
    }
    //显示技能区域
    public void ShowSkillArea()
    {
        GameAPP.MapManager.ShowAttckStep(this, skillPro.AttackRange, Color.red);
    }
    
    //隐藏技能攻击区域
    public void HideSkillArea()
    {
        GameAPP.MapManager.HideAttackStep(this, skillPro.AttackRange);
    }

    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Type = int.Parse(data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
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
            //执行未选中
            GameAPP.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
            if (IsStop == false)
            {
                //显示路径
                GameAPP.MapManager.ShowStepGrid(this, Step);
                //添加显示路径指令
                GameAPP.CommandManager.AddCommand(new ShowPathCommand(this));
                //添加选项事件
                addOptionEvents();
            }
            
            GameAPP.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    private void addOptionEvents()
    {
        GameAPP.MessageCenter.AddTempEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameAPP.MessageCenter.AddTempEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameAPP.MessageCenter.AddTempEvent(Defines.OnCancelEvent, onCancelCallBack);
    }

    private void onAttackCallBack(System.Object arg)
    {
        GameAPP.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
    }

    private void onIdleCallBack(System.Object arg)
    {
        Debug.Log("Idle");
        IsStop = true;
    }

    private void onCancelCallBack(System.Object arg)
    {
        GameAPP.CommandManager.Undo();
    }
    //未选中
    protected override void OnUnSelectCallBack(System.Object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameAPP.ViewManager.Close((int)ViewType.HeroDesView);
    }
    
    //受伤
    public override void GetHit(ISkill skill)
    {
        //播放受伤音效
        GameAPP.SoundManager.PlayEffect("hit", transform.position);
        //扣血
        CurHp -= skill.skillPro.Attack;
        //显示伤害数字
        GameAPP.ViewManager.ShowHitNum($"-{skillPro.Attack}", Color.red, transform.position);
        //击中特效
        PlayEffect(skill.skillPro.AttackEffect);
        //判断是否死亡
        if (CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");
            Destroy(gameObject, 1.2f);
            
            //从英雄集合中移除
            GameAPP.FightWorldManager.RemoveHero(this);
        }
        StopAllCoroutines();
        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateHpSlider());
    }
    
    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }
}
