using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    public Slider HealthSlider;
    public Text HealthText;
    public Slider EXPSlider;
    public Text EXPText;
    public Text LevelText;
    public Text CoinText;
    public Text EnemyCount;
    public Image RecoverMask;
    public override void Show()
    {
        base.Show();
        UpdatePanel();
    }
    public override void UpdatePanel()
    {
        base.UpdatePanel();
        HealthSlider.value = PlayerSystem.Instance.CurrentHeathl / PlayerSystem.Instance.MaxHeathl;
        HealthText.text = PlayerSystem.Instance.CurrentHeathl + "/" + PlayerSystem.Instance.MaxHeathl;
        EXPSlider.value = PlayerSystem.Instance.CurrentEXP / 100.0f;
        EXPText.text = PlayerSystem.Instance.CurrentEXP + "/" + 100;
        LevelText.text = PlayerSystem.Instance.Level.ToString();
        CoinText.text = PlayerSystem.Instance.CoinCount.ToString();
        EnemyCount.text = LevelSystem.Instance.EnemyCount.ToString();
    }
    public void UpdateRecover()
    {
        RecoverMask.fillAmount = PlayerSystem.Instance.RecoverCD / PlayerSystem.Instance.BaseRecoverCD;
    }
}
