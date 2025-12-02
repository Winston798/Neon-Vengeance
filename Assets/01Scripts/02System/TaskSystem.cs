using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskEnum
{
    None,
    通过第一关 = 1,
    通过第二关 = 2,
    通过第三关 = 3,
    通过第四关 = 4,
    通过第五关 = 5,
    击败老鼠 = -1,
    击败野猪 = -2,
}

/// <summary>
/// 任务系统
/// </summary>
public class TaskSystem : SingletonBase<TaskSystem>
{
    public List<MyTaskData> MyTask;
    public override void Init()
    {
        base.Init();
    }
    public void LoadDefaultTaskData()
    {
        foreach (var i in ResService.Instance.GetAllTaskData())
        {
            MyTaskData Data = new MyTaskData();
            Data.Data = i;
            Data.CurrentCount = 0;
            Data.IsComplete = false;
            Data.IsGet = false;
            MyTask.Add(Data);
        }
    }
    public void Load()
    {
        if (DataSystem.Instance.CurrentLoginData.MyTask.Count == 0)
        {
            LoadDefaultTaskData();
        }
        else
        {
            MyTask = DataSystem.Instance.CurrentLoginData.MyTask;
        }
    }
    public void SubmitTask(TaskEnum taskEnum)
    {
        //判断任务类型
        //如果是通关之类的任务，则直接完成任务
        foreach (var i in MyTask)
        {
            if (i.Data.TaskEnum == taskEnum && !i.IsComplete)
            {
                i.CurrentCount++;
                if (i.CurrentCount >= i.NeedCount)
                {
                    i.IsComplete = true;
                }
            }
        }
    }
    public void GetTaskReward(TaskEnum taskEnum)
    {
        foreach (var i in MyTask)
        {
            if (i.Data.TaskEnum == taskEnum)
            {
                i.IsGet = true;
                PlayerSystem.Instance.CoinCount += i.Data.RewardCount;
            }
        }
        UIService.Instance.UpdatePanel<TaskPanel>();
        DataSystem.Instance.Save();
    }
}
