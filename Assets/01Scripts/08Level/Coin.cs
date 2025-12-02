using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUpBase
{
    public override void Trigger()
    {
        base.Trigger();
        //加金币
        PlayerSystem.Instance.CoinCount++;
    }
}
