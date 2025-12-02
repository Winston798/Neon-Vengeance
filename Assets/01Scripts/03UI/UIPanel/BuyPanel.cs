using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : BasePanel
{
    public Image EquipSprite;
    public Text EquipName;
    public Text EquipDes;
    public Text Price;
    public Button Buy;
    public Button Back;
    private EquipShop Data;

    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
        });
        Buy.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            //购买
            if (PlayerSystem.Instance.CoinCount >= ResService.Instance.GetEquipDataByID(Data.EquipID).Price)
            {
                //钱够
                ShopSystem.Instance.Buy(Data.EquipID);
                UIService.Instance.ShowPanel<FloatPanel>(3).Init("Purchase successful", () =>
                {
                    HideMe();
                });
            }
            else
            {
                //不够
                UIService.Instance.ShowPanel<FloatPanel>(3).Init("Insufficient gold coins", null);
            }
        });
    }

    public void Init(EquipShop Data)
    {
        this.Data = Data;
        EquipData data = ResService.Instance.GetEquipDataByID(Data.EquipID);
        EquipSprite.sprite = data.EquipSprite;
        EquipName.text = data.EquipName;
        EquipDes.text = data.EquipDes;
        Price.text = data.Price.ToString();
    }
}