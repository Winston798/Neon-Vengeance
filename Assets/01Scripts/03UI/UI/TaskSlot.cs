using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour
{
    public Text TaskName;
    public Text TaskDes;
    public Text TaskProgress;
    public Text TaskReward;
    public Text GetText;
    public Button Get;
    private MyTaskData Data;

    public void Init(MyTaskData Data)
    {
        this.Data = Data;
        Get.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            TaskSystem.Instance.GetTaskReward(Data.Data.TaskEnum);
        });
        TaskName.text = Data.Data.TaskNameStr;
        TaskDes.text = Data.Data.TaskDes;
        TaskReward.text = Data.Data.RewardDes;
        if (Data.Data.IsShowCount)
        {
            TaskProgress.text = Data.CurrentCount + "/" + Data.NeedCount;
        }
        else
        {
            TaskProgress.text = "Unfinished";
        }
        if (Data.IsComplete)
        {
            TaskProgress.text = "Completed";
            Get.interactable = true;
            GetText.text = "Claim";
        }
        else
        {
            Get.interactable = false;
            GetText.text = "Unfinished";
        }
        if (Data.IsGet)
        {
            Get.interactable = false;
            GetText.text = "Collected";
        }
    }
}