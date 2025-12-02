using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    public Button Back;
    public Transform EquipSlotRoot;
    public GameObject EquipSlot;
    public EquipSlot Arrow;
    public EquipSlot Weapon;
    public EquipSlot Shoes;
    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
        });
        UpdatePanel();
    }
    public override void UpdatePanel()
    {
        for (int i = 0; i < EquipSlotRoot.childCount; i++)
        {
            Destroy(EquipSlotRoot.GetChild(i).gameObject);
        }
        Arrow.Init(BagSystem.Instance.CurrentArrow);
        Weapon.Init(BagSystem.Instance.CurrentWeapon);
        Shoes.Init(BagSystem.Instance.CurrentShoes);
        foreach (var i in BagSystem.Instance.MyEquip)
        {
            Instantiate(EquipSlot, EquipSlotRoot).GetComponent<EquipSlot>().Init(i);
        }
    }
}
