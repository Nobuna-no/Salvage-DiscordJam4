using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolObject
{
    public UnityEvent OnDeath;


    [SerializeField]
    private float DelayBeforeDestroy = 3f;
    
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
        OnDeath?.Invoke();
        Spawner gao = FindObjectOfType<Spawner>();
        if(gao)
        {
            gao.SpawnAt(gameObject.transform);
        }
        AudioManager.Instance.PlayHumanDyingRandomAudio(transform.position);
        //StartCoroutine(DelayBeforeKill_Coroutine());
        Destroy(this.gameObject);
    }

    public void applyDmg(int dmg)
    {
        Life -= dmg;
    }

    public void Kill()
    {
        Life = 0;
    }

    //IEnumerator DelayBeforeKill_Coroutine()
    //{
    //    GetComponent<Rigidbody2D>().simulated = false;
    //    GetComponentInChildren<Renderer>().enabled = false;
    //    yield return new WaitForSeconds(DelayBeforeDestroy);
    //    Destroy(this.gameObject);
    //}
}
