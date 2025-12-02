using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “比较强”的怪
/// </summary>
public class EnemyStrong : EnemyNormal
{
    private int HurtCount;//受伤次数
    protected override void HurtToBack(Transform pos)
    {
        if (IsAttack)
            return;
        HurtCount++;
        if (HurtCount >= 3)
        {
            if (pos.position.x > transform.position.x)
            {
                Flip(true);
            }
            else
            {
                Flip(false);
            }
            state.ChangeState(AttackState);
            HurtCount = 0;
            return;
        }
        base.HurtToBack(pos);
    }
}
