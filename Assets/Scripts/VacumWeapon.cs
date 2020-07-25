using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VacumWeapon : Weapon
{
    [SerializeField]
    CircleCollider2D circle;

    public static UnityEvent OnVacumStart;
    public static UnityEvent OnVacumEnd;
    public static UnityEvent OnVacumeOverHeatMax;
    public static UnityEvent OnVacumeOverHeatEnd;

    bool canVacume = true;

    private void Start()
    {
        Lastfired = fireRate;
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();

        if (Input.GetAxis("collect") == 1 && canVacume)
        {
            if(fireRate > 0)
            {
                Vacume(true);
                fireRate -= Time.deltaTime;
            }
            else
            {
                OnVacumeOverHeatMax.Invoke();
                Vacume(false);
                canVacume = false;
            }
        }
        else if(!canVacume || Input.GetAxis("collect") == 0)
        {
            Vacume(false);
            fireRate = Mathf.Min(fireRate + Time.deltaTime, Lastfired);
            if(!canVacume)
            { 
                canVacume = fireRate == Lastfired;
                if(canVacume)
                {
                    OnVacumeOverHeatEnd.Invoke();
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().applyDmg();
        }
    }

    public void Vacume(bool value)
    {
        circle.enabled = value;
        if (value)
        {
            OnVacumStart.Invoke();
        }
        else
        {
            OnVacumEnd.Invoke();
        }
    }

}
