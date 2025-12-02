using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaoPanel : BasePanel
{
    public Button Back;
    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            HideMe();
        });
    }
}
