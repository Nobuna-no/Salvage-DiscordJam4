using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public UnityEvent OnDeath;

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
        BoidsManager.Instance.Boids.Remove(gameObject);
        Instantiate(Prefab, transform.position, Quaternion.identity);
        OnDeath.Invoke();
        Spawner gao = FindObjectOfType<Spawner>();
        if(gao)
        {
            gao.SpawnAt(gameObject.transform);
        }
        Destroy(this.gameObject);
    }

    public void applyDmg()
    {
        Life--;
    }

}
