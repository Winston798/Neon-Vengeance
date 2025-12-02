using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyTaskData
{
    public TaskData Data;
    public int CurrentCount;//当前数量
    public int NeedCount => Data.TaskNeedCount;
    public bool IsComplete;//是否完成
    public bool IsGet;//是否领取
}
