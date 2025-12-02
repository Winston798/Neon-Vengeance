using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPanel : BasePanel
{
    public Button Back;
    public Button Attack;
    public Button HeavyAttack;
    public Button Recover;
    public Text Des;
    [TextArea]
    public string AttackDes;
    [TextArea]
    public string HeavyAttackDes;
    [TextArea]
    public string RecoverDes;
    private int CurrentIndex;
    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            HideMe();
            AudioService.Instance.PlayEffect("Button");
        });
        Attack.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            CurrentIndex = 1;
            UpdateDes();
        });
        HeavyAttack.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            CurrentIndex = 2;
            UpdateDes();
        });
        Recover.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            CurrentIndex = 3;
            UpdateDes();
        });
        CurrentIndex = 0;
        UpdateDes();
    }
    private void UpdateDes()
    {
        switch (CurrentIndex)
        {
            case 0:
                Des.text = "";
                break;
            case 1:
                Des.text = AttackDes;
                break;
            case 2:
                Des.text = HeavyAttackDes;
                break;
            case 3:
                Des.text = RecoverDes;
                break;
        }
    }
}
