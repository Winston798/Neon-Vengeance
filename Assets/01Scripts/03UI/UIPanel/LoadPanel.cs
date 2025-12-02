using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class LoadPanel : BasePanel
{
    public Slider ProgressSlider;
    public Text ProgressText;
    public override void Show()
    {
        base.Show();
    }
    public void SetProgress(float value)
    {
        ProgressSlider.value = value / 100;
        ProgressText.text = (int)value + "%";
    }
}
