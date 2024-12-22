using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : ModelBase, ISkill
{
    public SkillProperty skillPro { get; set; }


    private Slider hpSlider;
    protected override void Start()
    {
        base.Start();
        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
        data = GameAPP.ConfigManager.GetConfigData("enemy").GetDataById(Id);
        Id = int.Parse(data["Id"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        Type = int.Parse(data["Type"]);
        skillPro = new SkillProperty(int.Parse(data["Skill"]));
    }
    //选中
    protected override void OnSelectCallBack(System.Object arg)
    {
        if(GameAPP.CommandManager.IsRunningCommand == true)
        {
            return;
        }
        base.OnSelectCallBack(arg);
        GameAPP.ViewManager.Open(ViewType.EnemyDesView, this);
    }
    //未选中
    protected override void OnUnSelectCallBack(System.Object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameAPP.ViewManager.Close((int)ViewType.EnemyDesView);
    }
    
    public void ShowSkillArea()
    {
        
    }

    public void HideSkillArea()
    {
        
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
            
            //从敌人集合中移除
            GameAPP.FightWorldManager.RemoveEnemy(this);
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
