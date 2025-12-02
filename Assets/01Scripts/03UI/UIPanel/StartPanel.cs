using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button StartButton;
    public Button SetButton;
    public Button CaoButton;
    public Button ShopButton;
    public Button HeroButton;
    public Button BagButton;
    public Button SkillButton;
    public Button TaskButton;
    public Text NameText;
    public Text CoinText;
    public Text LevelText;

    public override void Show()
    {
        base.Show();
        AudioService.Instance.PlayBK("StartBK");
        UpdatePanel();
        StartButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            if (LevelSystem.Instance.CurrentLevel >= 7)
            {
                UIService.Instance.ShowPanel<FloatPanel>().Init("Your task is accomplished", null);
                return;
            }
            //切换场景
            HideMe();
            LevelSystem.Instance.LoadTown();
        });
        TaskButton.onClick.AddListener(() =>
        {
            UIService.Instance.ShowPanel<TaskPanel>(1);
        });
        SetButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<SetPanel>(1);
        });
        CaoButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<CaoPanel>(1);
        });
        ShopButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<ShopPanel>(1);
        });
        HeroButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<HeroPanel>(1);
        });
        BagButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<BagPanel>(1);
        });
        SkillButton.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            UIService.Instance.ShowPanel<SkillPanel>(1);
        });
    }

    public override void UpdatePanel()
    {
        NameText.text = DataSystem.Instance.CurrentLoginData.PlayerName;
        CoinText.text = PlayerSystem.Instance.CoinCount.ToString();
        LevelText.text = PlayerSystem.Instance.Level.ToString();
    }
}