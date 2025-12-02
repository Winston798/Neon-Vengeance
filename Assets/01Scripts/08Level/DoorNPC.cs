using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNPC : TriggerBase
{
    public List<DialogueData_SO> Dialogues;
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        DialogueSystem.Instance.StartDialogue(Dialogues[LevelSystem.Instance.CurrentLevel - 1], () =>
        {
            //打开传送门
            LevelSystem.Instance.OpenDoor(DoorType.Level);
        });
    }
}
