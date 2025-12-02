using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//任务数据
[System.Serializable]
public class TaskData
{
    public string TaskNameStr;//任务名字
    public TaskEnum TaskEnum;//任务名字枚举
    public string TaskDes;//任务描述
    public bool IsShowCount;//是否显示数量
    public int TaskNeedCount;//任务所需的数量
    public string RewardDes;//奖励描述
    public int RewardCount;//奖励金币的数量
}
