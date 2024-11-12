using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    Enter,
    Player
}

/// <summary>
/// 战斗管理器 （用于管理战斗相关的实体，敌人 英雄 地图 格子等）
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase currentFightUnit; //当前所处的战斗单元
    public List<Hero> heros; //战斗中的英雄集合
    public List<Enemy> enemies;//战斗中的敌人集合

    public int RoundCount;//回合数
    public FightUnitBase CurrentFightUnit
    {
        get { return currentFightUnit; }
    }

    public FightWorldManager()
    {
        heros = new List<Hero>();
        enemies = new List<Enemy>();
        ChangeState(GameState.Idle);
    }

    public void Update(float dt)
    {
        if (currentFightUnit != null && currentFightUnit.Update(dt))
        {
            //ToDo
        }
        else
        {
            currentFightUnit = null;
        }
    }

    //切换战斗状态
    public void ChangeState(GameState newState)
    {
        FightUnitBase _current = currentFightUnit;
        this.state = newState;
        switch (this.state)
        {
            case GameState.Idle:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;
            case GameState.Player:
                _current = new FightPlayerUnit();
                break;
        }

        _current.Init();
    }
    //进入战斗 初始化一些信息 敌人信息 回合数等
    public void EnterFight()
    {
        RoundCount = 1;
        heros = new List<Hero>();
        enemies = new List<Enemy>();
        //将场景中的敌人脚本进行存储
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy"); //给怪物添加Enemy标签
        for (int i = 0; i < obj.Length; i++)
        {
            Enemy enemy = obj[i].GetComponent<Enemy>();
            //当前位置被占用了 要把对应的方法类型设置为障碍物
            GameAPP.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemies.Add(enemy);
        }
    }
    //添加英雄
    public void AddHero(Block b, Dictionary<string, string> data)
    {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        //这个位置被占领了，设置方块的类型为障碍物
        b.Type = BlockType.Obstacle;
        heros.Add(hero);
    }
}