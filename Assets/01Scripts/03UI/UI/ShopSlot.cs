using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopSlot : MonoBehaviour
{
    private EquipShop Data;
    private Button MyButton;
    public Image EquipSprite;
    public void Init(EquipShop Data)
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(() =>
        {
            //判断钱够不够
            //打开购买界面
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<BuyPanel>(2).Init(Data);
        });
        this.Data = Data;
        EquipSprite.sprite = ResService.Instance.GetEquipDataByID(Data.EquipID).EquipSprite;
    }
}
