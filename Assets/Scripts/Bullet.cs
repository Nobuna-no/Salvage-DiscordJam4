using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public UnityEvent OnHit;

    public BulletSO script;

    [SerializeField]
    private float DelayBeforeDestroy = 2f;

    [SerializeField]
    CircleCollider2D collider2d;

    private int life;
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if(life <= 0)
            {
                StartCoroutine(DelayBeforeKill());
            }
        }
    }

    private void Awake()
    {
        Life = script.Life;
    }

    public void SetRadius(float value)
    {
        collider2d.radius = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Life--;
            collision.gameObject.GetComponent<Enemy>().applyDmg();
            OnHit?.Invoke();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Life = 0;
            Vector3 location = collision.ClosestPoint(transform.position);
            Vector3 dir = (location - transform.position);
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, dir, 360, 360);
            OnHit?.Invoke();
        }
    }


    IEnumerator DelayBeforeKill()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(DelayBeforeDestroy);
        Destroy(this.gameObject);
    }
}
