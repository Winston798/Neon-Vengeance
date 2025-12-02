using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可拾取物品的基类
/// 比如金币
/// /// </summary>

public class PickUpBase : MonoBehaviour
{
    private Rigidbody2D rig;
    public float Radius;
    private bool IsInit;
    private float timer;
    public void Init()
    {
        rig = GetComponent<Rigidbody2D>();
        float RamdomForce = Random.Range(-20, 20);
        rig.AddForce(new Vector2(RamdomForce, 100), ForceMode2D.Impulse);
        timer = 0;
        IsInit = true;
    }
    private void Update()
    {
        if (!IsInit)
            return;
        timer += Time.deltaTime;
        if (timer <= 0.2f)
            return;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, Radius, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            Trigger();
        }
    }
    //触发函数
    public virtual void Trigger()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
