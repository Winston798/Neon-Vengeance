using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店系统
/// </summary>
public class ShopSystem : SingletonBase<ShopSystem>
{
    public List<EquipShop> ShopEquips { get; private set; }
    public override void Init()
    {
        base.Init();
        ShopEquips = new List<EquipShop>();
        //将所有装备添加进商店
        foreach (var i in ResService.Instance.GetAllEquipID())
        {
            EquipShop equip = new EquipShop();
            equip.EquipID = i;
            ShopEquips.Add(equip);
        }
    }

    public void Buy(int EquipID)
    {
        //能走到这一步说明钱够
        //减钱
        PlayerSystem.Instance.CoinCount -= ResService.Instance.GetEquipDataByID(EquipID).Price;
        BagSystem.Instance.AddEquip(EquipID);
    }
}
public class EquipShop
{
    public int EquipID;
}
