using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public Image EquipSprite;
    public Button MyButton;
    private EquipBag Data;
    public void Init(EquipBag Data)
    {
        MyButton.onClick.RemoveAllListeners();
        MyButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<EquipDetailPanel>(1).Init(Data);
        });
        if (Data == null)
        {
            EquipSprite.gameObject.SetActive(false);
            MyButton.interactable = false;
        }
        else
        {
            EquipSprite.gameObject.SetActive(true);
            MyButton.interactable = true;
            this.Data = Data;
            EquipSprite.sprite = ResService.Instance.GetEquipDataByID(Data.EquipID).EquipSprite;
        }
    }

}
