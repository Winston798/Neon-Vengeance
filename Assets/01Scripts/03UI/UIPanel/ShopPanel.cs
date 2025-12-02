using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店面板
/// </summary>
public class ShopPanel : BasePanel
{
    public Transform ShopSlotRoot;
    public GameObject ShopSlot;
    public Button Back;
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
        for (int i = 0; i < ShopSlotRoot.childCount; i++)
        {
            Destroy(ShopSlotRoot.GetChild(i).gameObject);
        }
        foreach (var i in ShopSystem.Instance.ShopEquips)
        {
            Instantiate(ShopSlot, ShopSlotRoot).GetComponent<ShopSlot>().Init(i);
        }

    }
}
