using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/AllEquip")]
public class AllEquipData_SO : ScriptableObject
{
    public List<EquipData> AllEquip = new List<EquipData>();
}
