using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerStateBase
{
    public PlayerHeavyAttackState(Player player, PlayerStateMachine state, string AniName) : base(player, state, AniName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        AudioService.Instance.PlayEffect("大招");
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (InputShift && player.IsCanDash)
        {
            state.ChangeState(player.DashState);
            return;
        }
        if (IsComplete)
        {
            state.ChangeState(player.IdleState);
            return;
        }
    }
    public override void AniEvent()
    {
        base.AniEvent();
        Vector3 AttackPos = new Vector3(player.Dir * player.HeavyAttackData.AttackPos.x, player.HeavyAttackData.AttackPos.y);
        //物理检测攻击范围内是否有敌人
        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.transform.position + AttackPos, player.HeavyAttackData.AttackRaduis, 0, LayerMask.GetMask("Enemy"));
        //如果有，则对敌人造成伤害
        if (colliders.Length != 0)
        {
            AttackEffect.Instance.Shake();
            AttackEffect.Instance.StopFrame(8);
            AudioService.Instance.PlayEffect("Hurt");
            foreach (var i in colliders)
            {
                i.GetComponent<IHurt>()?.Hurt(player.transform, PlayerSystem.Instance.Attack * 4);
            }
        }
    }
}
