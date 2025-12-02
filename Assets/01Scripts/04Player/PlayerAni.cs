using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 调用Player脚本中的动画事件
/// 该脚本就是中转的作用
/// </summary>
public class PlayerAni : MonoBehaviour
{
    public void AniEvent()
    {
        GetComponentInParent<Player>().AniEvent();
    }
}
