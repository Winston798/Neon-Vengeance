using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 伤害接口基类
/// </summary>
public interface IHurt
{
    void Hurt(Transform pos, float Value);
}
