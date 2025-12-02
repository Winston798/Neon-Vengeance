using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : BasePanel
{
    public InputField NameInput;
    public Button OK;
    public override void Show()
    {
        base.Show();
        OK.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            DataSystem.Instance.Named(NameInput.text);
            UIService.Instance.ShowPanel<StartPanel>();
            HideMe();
        });
    }
}
