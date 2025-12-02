using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 数据系统
/// </summary>
public class DataSystem : SingletonBase<DataSystem>
{
    public List<Data> AllUserData { get; private set; }
    public Data CurrentLoginData { get; private set; }

    public override void Init()
    {
        base.Init();
        //如果是第一次打开游戏
        if (!File.Exists(Application.persistentDataPath + "UserData.json"))
        {
            AllUserData = new List<Data>();
        }
        else
        {
            string JsonStr = File.ReadAllText(Application.persistentDataPath + "UserData.json");
            SaveData saveData = JsonUtility.FromJson<SaveData>(JsonStr);
            AllUserData = saveData.AllUserData;
        }
    }

    /// <summary>
    /// 登录账号
    /// </summary>
    public bool Login(string Account, string Password)
    {
        foreach (var i in AllUserData)
        {
            if (i.Account == Account && i.Password == Password)
            {
                CurrentLoginData = i;
                Load();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 注册账号
    /// </summary>
    /// <param name="Account">名字</param>
    /// <param name="Password">密码</param>
    /// <returns></returns>
    public bool Register(string Account, string Password)
    {
        foreach (var i in AllUserData)
        {
            if (i.Account == Account)
            {
                return false;
            }
        }
        Data data = new Data();
        data.Account = Account;
        data.Password = Password;
        data.PlayerName = "";
        data.CoinCount = 1000;
        data.Level = 1;
        data.CurrentLevel = 1;
        data.CurrentEXP = 0;
        data.MyEquip = new List<EquipBag>();
        data.MyTask = new List<MyTaskData>();
        data.CurrentArrow = null;
        data.CurrentShoes = null;
        data.CurrentWeapon = null;
        AllUserData.Add(data);
        return true;
    }

    /// <summary>
    /// 起名字
    /// </summary>
    /// <param name="Name"></param>
    public void Named(string Name)
    {
        CurrentLoginData.PlayerName = Name;
        Save();
    }

    public void Save()
    {
        CurrentLoginData.CoinCount = PlayerSystem.Instance.CoinCount;
        CurrentLoginData.Level = PlayerSystem.Instance.Level;
        CurrentLoginData.CurrentEXP = PlayerSystem.Instance.CurrentEXP;
        CurrentLoginData.CurrentLevel = LevelSystem.Instance.CurrentLevel;
        CurrentLoginData.MyEquip = BagSystem.Instance.MyEquip;
        CurrentLoginData.MyTask = TaskSystem.Instance.MyTask;
        CurrentLoginData.CurrentArrow = BagSystem.Instance.CurrentArrow;
        CurrentLoginData.CurrentShoes = BagSystem.Instance.CurrentShoes;
        CurrentLoginData.CurrentWeapon = BagSystem.Instance.CurrentWeapon;
        SaveData saveData = new SaveData { AllUserData = AllUserData, };
        string str = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "UserData.json", str);
    }

    public void Load()
    {
        BagSystem.Instance.Load();
        PlayerSystem.Instance.Load();
        LevelSystem.Instance.Load();
        TaskSystem.Instance.Load();
    }
}

[System.Serializable]
public class SaveData
{
    public List<Data> AllUserData;
}

/// <summary>
/// 数据类
/// </summary>

[System.Serializable]
public class Data
{
    public string Account;
    public string Password;
    public string PlayerName;
    public int CoinCount;
    public int Level;
    public int CurrentEXP;
    public int CurrentLevel;
    public List<MyTaskData> MyTask;
    public List<EquipBag> MyEquip;
    public EquipBag CurrentArrow;
    public EquipBag CurrentWeapon;
    public EquipBag CurrentShoes;
}
