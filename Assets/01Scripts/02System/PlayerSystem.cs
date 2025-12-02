using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSystem : SingletonBase<PlayerSystem>
{

    #region 玩家基本信息，血量速度攻击力金币
    //当前血量
    private float _CurrentHeathl;
    public float CurrentHeathl
    {
        get => _CurrentHeathl;
        set
        {
            _CurrentHeathl = value;
            if (_CurrentHeathl > MaxHeathl)
            {
                _CurrentHeathl = MaxHeathl;
            }
            if (_CurrentHeathl <= 0)
            {
                _CurrentHeathl = 0;
            }
            UIService.Instance.UpdatePanel<BattlePanel>();
        }
    }
    //最大血量
    private float _MaxHeathl;
    public float MaxHeathl
    {
        get => _MaxHeathl;
        set
        {
            _MaxHeathl = value;
            UIService.Instance.UpdatePanel<BattlePanel>();
        }
    }
    //金币数量
    private int _CoinCount;
    public int CoinCount
    {
        get => _CoinCount;
        set
        {
            _CoinCount = value;
            UIService.Instance.UpdatePanel<BattlePanel>();
            UIService.Instance.UpdatePanel<StartPanel>();
        }
    }
    //当前经验值
    private int _CurrentEXP;
    public int CurrentEXP
    {

        get => _CurrentEXP;
        set
        {
            _CurrentEXP = value;
            if (_CurrentEXP >= 100)
            {
                Level++;
                _CurrentEXP = 0;
            }
            UIService.Instance.UpdatePanel<BattlePanel>();
        }
    }
    //等级
    private int _Level;
    public int Level
    {
        get => _Level;
        set
        {
            if (value > 10)
            {
                return;
            }
            _Level = value;
            BaseAttack = 10 + (_Level - 1) * 3;
            BaseMaxHeathl = 100 + (_Level - 1) * 10;
            UIService.Instance.UpdatePanel<BattlePanel>();
        }
    }
    //攻击
    public int Attack { get; set; }
    //移动速度
    public int MoveSpeed { get; set; }
    #endregion

    #region 基础数据
    private int BaseMoveSpeed;
    private float BaseMaxHeathl;
    private int BaseAttack;
    public float BaseRecoverCD { get; private set; }
    #endregion

    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public Player player { get; private set; }
    public bool IsImmunity { get; set; }//是否无敌
    public bool IsPause { get; private set; }//玩家是否处于暂停状态，并非人物暂停
    public float RecoverCD { get; private set; }
    public override void Init()
    {
        base.Init();
        BaseMaxHeathl = 100;
        BaseMoveSpeed = 7;
        BaseAttack = 10;
        BaseRecoverCD = 50;
        RecoverDefaultData();
        RecoverCD = 0;
    }
    public void Load()
    {
        CoinCount = DataSystem.Instance.CurrentLoginData.CoinCount;
        CurrentEXP = DataSystem.Instance.CurrentLoginData.CurrentEXP;
        Level = DataSystem.Instance.CurrentLoginData.Level;
        ReCalculateData();
    }
    private void Update()
    {
        if (RecoverCD > 0)
        {
            RecoverCD -= Time.deltaTime;
            //更新面板
            UIService.Instance.GetPanel<BattlePanel>()?.UpdateRecover();
        }
    }
    public void UseRecover()
    {
        if (RecoverCD > 0)
            return;
        CurrentHeathl += 40;
        RecoverCD = BaseRecoverCD;
    }
    private void RecoverDefaultData()
    {
        MoveSpeed = BaseMoveSpeed;
        MaxHeathl = BaseMaxHeathl;
        Attack = BaseAttack;
    }
    /// <summary>
    /// 生成人物
    /// </summary>
    /// <param name="pos">人物位置</param>
    public void CreatPlayer(Vector3 pos)
    {
        //恢复人物状态
        CurrentHeathl = MaxHeathl;
        //生成人物
        player = Instantiate(PlayerPrefab, pos, Quaternion.identity).GetComponent<Player>();
        player.Init();
        //生成相机
        GameObject go = Instantiate(CameraPrefab);
        PolygonCollider2D Bound = GameObject.Find("Bound").GetComponent<PolygonCollider2D>();
        go.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = Bound;
        go.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
    /// <summary>
    /// 重新计算玩家数据
    /// </summary>
    public void ReCalculateData()
    {
        //恢复基础数据
        RecoverDefaultData();
        //重新计算装备加成
        EquipData arrow = ResService.Instance.GetEquipDataByID(BagSystem.Instance.CurrentArrow != null ? BagSystem.Instance.CurrentArrow.EquipID : -1);
        EquipData weapon = ResService.Instance.GetEquipDataByID(BagSystem.Instance.CurrentWeapon != null ? BagSystem.Instance.CurrentWeapon.EquipID : -1);
        EquipData shoes = ResService.Instance.GetEquipDataByID(BagSystem.Instance.CurrentShoes != null ? BagSystem.Instance.CurrentShoes.EquipID : -1);
        if (arrow != null)
        {
            MaxHeathl += arrow.AddHealth;
            Attack += arrow.AddAttack;
            MoveSpeed += arrow.AddSpeed;
        }
        if (weapon != null)
        {
            MaxHeathl += weapon.AddHealth;
            Attack += weapon.AddAttack;
            MoveSpeed += weapon.AddSpeed;
        }
        if (shoes != null)
        {
            MaxHeathl += shoes.AddHealth;
            Attack += shoes.AddAttack;
            MoveSpeed += shoes.AddSpeed;
        }
    }

    /// <summary>
    /// 玩家暂停
    /// </summary>
    public void Pause()
    {
        if (player == null)
            return;
        player.PausePlayer();
        IsPause = true;
    }
    /// <summary>
    /// 玩家继续
    /// </summary>
    public void Continue()
    {
        if (player == null)
            return;
        player.ContinuePlayer();
        IsPause = false;
    }
}
