using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VacuumWeapon : Weapon
{
    [SerializeField]
    CircleCollider2D circle;

    public UnityEvent OnVacumStart;
    public UnityEvent OnVacumEnd;
    public UnityEvent OnVacumeOverHeatMax;
    public UnityEvent OnVacumeOverHeatEnd;

    bool m_canVacuum = true;
    public bool IsWorking = false;

    private void Start()
    {
        Lastfired = fireRate;
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();

        if (Input.GetAxis("collect") == 1 && m_canVacuum)
        {
            if(fireRate > 0)
            {
                Vacume(true);
                IsWorking = true;
                fireRate -= Time.deltaTime;
            }
            else
            {
                OnVacumeOverHeatMax?.Invoke();
                Vacume(false);
                IsWorking = false;
                m_canVacuum = false;
            }
        }
        else if(!m_canVacuum || Input.GetAxis("collect") == 0)
        {
            Vacume(false);
            IsWorking = false;
            fireRate = Mathf.Min(fireRate + Time.deltaTime, Lastfired);
            if(!m_canVacuum)
            {
                m_canVacuum = fireRate == Lastfired;
                if(m_canVacuum)
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
            collision.gameObject.GetComponent<Enemy>().Kill();
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
                IsoController.SlowDown(value);
            }
            else
            {
                IsoController.SlowDown(value);
                OnVacumEnd?.Invoke();
            }
        }
    }

    public void EndVacume()
    {
        OnVacumEnd?.Invoke();
    }


}
