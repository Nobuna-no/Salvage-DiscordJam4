using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletSO script;

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
                Destroy(this.gameObject);
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
            Debug.Log("Enemy Hurt");
            Life--;
            collision.gameObject.GetComponent<Enemy>().applyDmg();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Life = 0;
        }
    }
}
