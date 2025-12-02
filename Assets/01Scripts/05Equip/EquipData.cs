using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipData
{
    public string EquipName;
    public int EquipID;
    [TextArea]
    public string EquipDes;
    public Sprite EquipSprite;
    public EquipType EquipType;
    public int AddHealth;
    public int AddAttack;
    public int AddSpeed;
    public int Price;
}
public enum EquipType
{
    Arrow,
    Weapon,
    Shoes,
}
