using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipBag
{
    public int EquipID;
    public int SortIndex;
}
public class BagSystem : SingletonBase<BagSystem>
{
    public List<EquipBag> MyEquip { get; private set; }
    public EquipBag CurrentArrow { get; private set; }
    public EquipBag CurrentWeapon { get; private set; }
    public EquipBag CurrentShoes { get; private set; }
    public override void Init()
    {
        base.Init();
        MyEquip = new List<EquipBag>();
        CurrentArrow = null;
        CurrentShoes = null;
        CurrentShoes = null;
    }
    public void Load()
    {
        this.MyEquip = DataSystem.Instance.CurrentLoginData.MyEquip;
        if (DataSystem.Instance.CurrentLoginData.CurrentArrow == null || DataSystem.Instance.CurrentLoginData.CurrentArrow.EquipID == 0)
        {
            this.CurrentArrow = null;
        }
        else
        {
            this.CurrentArrow = DataSystem.Instance.CurrentLoginData.CurrentArrow;
        }
        if (DataSystem.Instance.CurrentLoginData.CurrentWeapon == null || DataSystem.Instance.CurrentLoginData.CurrentWeapon.EquipID == 0)
        {
            this.CurrentWeapon = null;
        }
        else
        {
            this.CurrentWeapon = DataSystem.Instance.CurrentLoginData.CurrentWeapon;
        }
        if (DataSystem.Instance.CurrentLoginData.CurrentShoes == null || DataSystem.Instance.CurrentLoginData.CurrentShoes.EquipID == 0)
        {
            this.CurrentShoes = null;
        }
        else
        {
            this.CurrentShoes = DataSystem.Instance.CurrentLoginData.CurrentShoes;
        }
    }
    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="ID">物品ID</param>
    public void AddEquip(int ID)
    {
        EquipBag equip = new EquipBag();
        equip.EquipID = ID;
        MyEquip.Add(equip);
        Sort();
        //更新UI
        UIService.Instance.UpdatePanel<BagPanel>();
        //更新存储
        DataSystem.Instance.Save();
    }
    /// <summary>
    /// 移除物品
    /// </summary>
    /// <param name="SortIndex">物品序号</param>
    public void RemoveEquip(int SortIndex)
    {
        MyEquip.RemoveAt(SortIndex);
        Sort();
        //更新UI
        UIService.Instance.UpdatePanel<BagPanel>();
        //更新存储
        DataSystem.Instance.Save();
    }
    /// <summary>
    /// 从背包装备物品
    /// </summary>
    /// <param name="SortIndex">背包序号</param>
    public void Equip(int SortIndex)
    {
        EquipBag equip = MyEquip[SortIndex];
        RemoveEquip(SortIndex);
        EquipData data = ResService.Instance.GetEquipDataByID(equip.EquipID);
        switch (data.EquipType)
        {
            case EquipType.Arrow:
                if (CurrentArrow != null)
                {
                    AddEquip(CurrentArrow.EquipID);
                }
                CurrentArrow = equip;
                CurrentArrow.SortIndex = -1;
                break;
            case EquipType.Weapon:
                if (CurrentWeapon != null)
                {
                    AddEquip(CurrentWeapon.EquipID);
                }
                CurrentWeapon = equip;
                CurrentWeapon.SortIndex = -2;
                break;
            case EquipType.Shoes:
                if (CurrentShoes != null)
                {
                    AddEquip(CurrentShoes.EquipID);
                }
                CurrentShoes = equip;
                CurrentShoes.SortIndex = -3;
                break;
        }
        //重新计算玩家数据
        PlayerSystem.Instance.ReCalculateData();
        //更新UI
        UIService.Instance.UpdatePanel<BagPanel>();
        //更新存储
        DataSystem.Instance.Save();
    }
    public void UnEquip(int SortIndex)
    {
        switch (SortIndex)
        {
            case -1:
                if (CurrentArrow != null)
                {
                    AddEquip(CurrentArrow.EquipID);
                    CurrentArrow = null;
                }
                break;
            case -2:
                if (CurrentWeapon != null)
                {
                    AddEquip(CurrentWeapon.EquipID);
                    CurrentWeapon = null;
                }
                break;
            case -3:
                if (CurrentShoes != null)
                {
                    AddEquip(CurrentShoes.EquipID);
                    CurrentShoes = null;
                }
                break;
        }
        //重新计算玩家数据
        PlayerSystem.Instance.ReCalculateData();
        //更新UI
        UIService.Instance.UpdatePanel<BagPanel>();
        //更新存储
        DataSystem.Instance.Save();
    }
    private void Sort()
    {
        for (int i = 0; i < MyEquip.Count; i++)
        {
            MyEquip[i].SortIndex = i;
        }
    }
}
