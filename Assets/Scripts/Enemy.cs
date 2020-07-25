using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Dung Prefab;

    [SerializeField]
    private int life;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            if(life <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        Instantiate(Prefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void applyDmg()
    {
        Life--;
    }

}
