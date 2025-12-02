using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatPanel : BasePanel
{
    public Text Content;
    public Button OK;
    private Action Callback;

    public override void Show()
    {
        base.Show();
        OK.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
            Callback?.Invoke();
        });
    }

    public void Init(string Content, Action Callback)
    {
        this.Callback = Callback;
        this.Content.text = Content;
    }
}