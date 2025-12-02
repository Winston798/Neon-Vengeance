using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    public Button BackMenu;
    public Button NextLevel;
    public override void Show()
    {
        base.Show();
        PlayerSystem.Instance.Pause();
        BackMenu.onClick.AddListener(() =>
        {
            HideMe();
            ResService.Instance.LoadScene("Start", () =>
            {
                UIService.Instance.HideAllPanel();
                UIService.Instance.ShowPanel<StartPanel>();
            });
        });
        if (LevelSystem.Instance.CurrentLevel > 6)
        {
            NextLevel.interactable = false;
        }
        NextLevel.onClick.AddListener(() =>
        {
            LevelSystem.Instance.LoadTown();
            HideMe();
        });
    }
    public override void Hide()
    {
        base.Hide();
        PlayerSystem.Instance.Continue();
    }
}
