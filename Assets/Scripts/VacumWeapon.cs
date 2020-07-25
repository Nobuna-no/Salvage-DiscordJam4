using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumWeapon : Weapon
{
    [SerializeField]
    public Bullet AmmoType;

    public void Vacume()
    {
        Vector2 normalizedDirection = IsometricPlayerMovementController.lastWantedDirection.normalized;
        GameObject Go = Instantiate(AmmoType.gameObject, IsoController.transform.position + 0.5f * new Vector3(normalizedDirection.x, normalizedDirection.y), Quaternion.identity);
        Go.GetComponent<Bullet>().SetRadius(AmmoType.script.Radius);
        Go.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * AmmoType.script.Speed, ForceMode2D.Impulse);
    }

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
            Vacume();
            if(fireRate > 0)
            {
                fireRate -= Time.deltaTime;
            }
            else
            {
                canVacume = false;
            }
        }
        else if(!canVacume || Input.GetAxis("collect") == 0)
        {
            fireRate = Mathf.Min(fireRate + Time.deltaTime, Lastfired);
            if(!canVacume)
            { 
                canVacume = fireRate == Lastfired;
            }
        }
    }
}
