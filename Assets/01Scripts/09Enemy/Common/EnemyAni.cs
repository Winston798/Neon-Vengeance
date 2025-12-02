using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 中转脚本
/// </summary>
public class EnemyAni : MonoBehaviour
{
    public void AniEvent()
    {
        GetComponentInParent<Enemy>().AniEvent();
    }
}
