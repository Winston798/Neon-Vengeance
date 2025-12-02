using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/AllTask")]
public class TaskData_SO : ScriptableObject
{
    public List<TaskData> AllTask = new List<TaskData>();
}
