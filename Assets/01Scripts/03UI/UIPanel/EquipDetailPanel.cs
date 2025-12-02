using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailPanel : BasePanel
{
    public Image EquipSprite;
    public Text EquipName;
    public Text EquipDes;
    public Button Btn_Equip;
    public Button Btn_UnEquip;
    public Button Back;
    private EquipBag Data;
    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
        });
        Btn_UnEquip.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            BagSystem.Instance.UnEquip(Data.SortIndex);
            HideMe();
        });
        Btn_Equip.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            BagSystem.Instance.Equip(Data.SortIndex);
            HideMe();
        });
    }
    public void Init(EquipBag Data)
    {
        this.Data = Data;
        EquipSprite.sprite = ResService.Instance.GetEquipDataByID(Data.EquipID).EquipSprite;
        EquipName.text = ResService.Instance.GetEquipDataByID(Data.EquipID).EquipName;
        EquipDes.text = ResService.Instance.GetEquipDataByID(Data.EquipID).EquipDes;
        if (Data.SortIndex < 0)
        {
            Btn_Equip.gameObject.SetActive(false);
            Btn_UnEquip.gameObject.SetActive(true);
        }
        else
        {
            Btn_Equip.gameObject.SetActive(true);
            Btn_UnEquip.gameObject.SetActive(false);
        }
    }
}
