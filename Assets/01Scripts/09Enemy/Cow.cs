using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyBoss
{
    protected override void InitState()
    {
        SPCD = 7;
        BossStartState = new BossStartState(this, state, "Idle");
        BossIdleState = new BossIdleState(this, state, "Idle");
        BossChaseState = new BossChaseState(this, state, "Chase");
        BossAttackState = new BossAttackState(this, state, "Attack");
        BossSPAttackState = new CowSPAttackState(this, state, "SPAttack");
        BossDieState = new BossDieState(this, state, "Die");
        state.Init(BossStartState);
    }
}
