using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : BasePanel
{
    public Button Back;
    public Transform TaskSlotRoot;
    public GameObject TaskSlot;
    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
        });
        UpdatePanel();
    }
    public override void UpdatePanel()
    {
        base.UpdatePanel();
        for (int i = 0; i < TaskSlotRoot.childCount; i++)
        {
            Destroy(TaskSlotRoot.GetChild(i).gameObject);
        }
        foreach (var i in TaskSystem.Instance.MyTask)
        {
            Instantiate(TaskSlot, TaskSlotRoot).GetComponent<TaskSlot>().Init(i);
        }
    }
}
