using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRed : EnemyBoss
{
    protected override void InitState()
    {
        SPCD = 8;
        BossStartState = new BossStartState(this, state, "Idle");
        BossIdleState = new BossIdleState(this, state, "Idle");
        BossChaseState = new BossChaseState(this, state, "Chase");
        BossAttackState = new BossAttackState(this, state, "Attack");
        BossSPAttackState = new SmallRedSPAttackState(this, state, "SPAttack");
        BossDieState = new BossDieState(this, state, "Die");
        state.Init(BossStartState);
    }
}
