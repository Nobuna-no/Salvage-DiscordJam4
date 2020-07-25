using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VacumWeapon : Weapon
{
    [SerializeField]
    CircleCollider2D circle;

    public UnityEvent OnVacumStart;
    public UnityEvent OnVacumEnd;
    public UnityEvent OnVacumeOverHeatMax;
    public UnityEvent OnVacumeOverHeatEnd;

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
                OnVacumeOverHeatMax?.Invoke();
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
                    OnVacumeOverHeatEnd?.Invoke();
                }
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().applyDmg();
        }
    }

    bool m_LastVacumValue = false;
    public void Vacume(bool value)
    {
        circle.enabled = value;
        if (m_LastVacumValue  != value)
        {
            m_LastVacumValue = value;

            if(value)
            {
                IsoController.SwitchWeapon(1);
                OnVacumStart?.Invoke();
            }
            else
            {
                OnVacumEnd?.Invoke();
            }
        }
    }

    public void EndVacume()
    {
        OnVacumEnd?.Invoke();
    }


}
