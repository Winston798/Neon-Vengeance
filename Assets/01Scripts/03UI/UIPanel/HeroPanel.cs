using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroPanel : BasePanel
{
    public Text Attack;
    public Text Heathl;
    public Text Speed;
    public Text Level;
    public Button Back;
    public override void Show()
    {
        base.Show();
        Attack.text = PlayerSystem.Instance.Attack.ToString();
        Heathl.text = PlayerSystem.Instance.MaxHeathl.ToString();
        Speed.text = PlayerSystem.Instance.MoveSpeed.ToString();
        Level.text = PlayerSystem.Instance.Level.ToString();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
        });
    }
}
