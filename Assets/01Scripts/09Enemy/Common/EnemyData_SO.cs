using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    [Header("怪物名字")]
    public string EnemyName;
    [Header("最大血量")]
    public float MaxHeathl;
    [Header("移动速度")]
    public int MoveSpeed;
    public int Attack;
    [Header("追赶速度")]
    public int ChaseSpeed;
    [Header("视野范围")]
    public float CheckRadius;
    [Header("反应时间")]
    public float ReactTime;
    [Header("巡逻距离")]
    public float PatrolDistance;
    [Header("站立时间")]
    public float IdleTime;
    [Header("攻击范围")]
    public float AttackRadius;
    [Header("弓箭射击检测范围")]
    public Vector3 BowRadius;

    [Header("打击特效")]
    public GameObject HitEffect;
    [Header("掉落的金币数量")]
    public int FallCoin;//增加的金币
    [Header("最大掉落的经验")]
    public int MaxEXP;
}
public enum EnemyType
{
    NormalEnemy,
    BowEnemy,
}
