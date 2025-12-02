using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景系统
/// 管理各个场景的切换及其初始化
/// </summary>
public class LevelSystem : SingletonBase<LevelSystem>
{
    public int CurrentLevel { get; set; }
    private Door door;//当前场景重的传送门
    private int _EnemyCount;
    public int EnemyCount
    {
        get => _EnemyCount;
        set
        {
            _EnemyCount = value;
            UIService.Instance.UpdatePanel<BattlePanel>();
        }
    }
    public override void Init()
    {
        base.Init();
        CurrentLevel = 1;
    }
    public void Load()
    {
        this.CurrentLevel = DataSystem.Instance.CurrentLoginData.CurrentLevel;
    }
    /// <summary>
    /// 加载关卡
    /// 目前进入关卡只有生成角色
    /// </summary>
    public void LoadTown()
    {
        ResService.Instance.LoadScene("Town", () =>
        {
            InitLevel();
            AudioService.Instance.PlayBK("BattleBK");
        });
    }
    public void LoadCurrentLevel()
    {
        ResService.Instance.LoadScene("Level" + CurrentLevel, () =>
        {
            InitLevel();
            AudioService.Instance.PlayBK("BattleBK");
        });
    }
    private void InitLevel()
    {
        Transform PlayerPos = GameObject.FindGameObjectWithTag("PlayerPos")?.transform;
        if (PlayerPos != null)
        {
            PlayerSystem.Instance.CreatPlayer(PlayerPos.position);
        }
        else
        {
            Debug.LogError("场景加载错误，玩家位置未找到");
        }
        //隐藏传送门
        door = GameObject.FindObjectOfType<Door>();
        door?.Hide();
        //排序敌人
        SortEnemyLay();
        //显示UI
        UIService.Instance.ShowPanel<BattlePanel>();
        CheckIsHaveEnemy();
    }
    //打开当前场景的传送门
    public void OpenDoor(DoorType doorType)
    {
        door?.Show();
        door?.Init(doorType);
    }
    /// <summary>
    /// 场景中是否还有敌人
    /// </summary>
    /// <returns></returns>
    public void CheckIsHaveEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        if (enemies.Length == 0)
        {
            //没有敌人，并且是在关卡中
            if (ResService.Instance.CurrentSceneName.Contains("Level"))
            {
                WinLevel();
            }
        }
        EnemyCount = enemies.Length;
    }
    /// <summary>
    /// 关卡胜利
    /// </summary>
    public void WinLevel()
    {
        //提交任务
        TaskSystem.Instance.SubmitTask((TaskEnum)CurrentLevel);
        //开启下一关卡
        LevelSystem.Instance.CurrentLevel++;
        //打开门
        OpenDoor(DoorType.Win);
        //增加金币
        PlayerSystem.Instance.CoinCount += 150;
        //保存数据
        DataSystem.Instance.Save();
    }
    /// <summary>
    /// 重新排序当前场景中所有敌人的图层
    /// </summary>
    private void SortEnemyLay()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        int j = 0;
        foreach (var i in enemies)
        {
            i.EnemySpriteRenderer().sortingOrder = j;
            j++;
        }
    }
}
