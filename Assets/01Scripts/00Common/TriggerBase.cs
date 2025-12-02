using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour
{
    [Header("检测中心点")]
    public Transform CheckCenterPos;
    [Header("检测半径")]
    public float CheckRadius;
    [Header("提示")]
    public string InputContent;
    private bool InRange;//在范围内
    private bool OutRange;//在范围外
    protected bool IsTrigger;//是否已经触发
    public bool SingleUse = true;//是否只能触发一次
    protected int TriggerCount;

    private void Start()
    {
        InRange = false;
        OutRange = true;
        IsTrigger = false;
        TriggerCount = 0;
    }
    private void Update()
    {
        if (IsTrigger)
        {
            return;
        }
        Collider2D player = Physics2D.OverlapCircle(transform.position, CheckRadius, LayerMask.GetMask("Player"));
        if (player != null && !InRange)
        {
            InRange = true;
            OutRange = false;
            UIService.Instance.ShowPanel<TipPanel>().Init(InputContent);
            EnterRandius();
        }
        if (InRange)
        {
            if (Input.GetKeyDown(KeyCode.R) && !PlayerSystem.Instance.IsPause)
            {
                TriggerEvent();
            }
        }
        //当玩家出去的那一帧，进入这个里头，这时候OutRange为true，代表玩家在范围外，这样保证玩家进入范围和出去范围的方法只执行一次
        if (player == null && !OutRange)
        {
            InRange = false;
            OutRange = true;
            UIService.Instance.HidePanel<TipPanel>();
            ExitRadius();
        }
    }
    public virtual void TriggerEvent()
    {
        if (SingleUse)
        {
            IsTrigger = true;
            UIService.Instance.HidePanel<TipPanel>();
        }
        TriggerCount++;
    }
    public void MyDestoy()
    {
        if (InRange)
        {
            UIService.Instance.HidePanel<TipPanel>();
        }
        Destroy(gameObject);
    }
    public virtual void EnterRandius() { }
    public virtual void ExitRadius() { }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(CheckCenterPos.position, CheckRadius);
    }
}
