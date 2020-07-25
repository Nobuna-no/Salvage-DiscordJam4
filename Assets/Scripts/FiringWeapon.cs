﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringWeapon : Weapon
{
    [SerializeField]
    public Bullet AmmoType;

    public void Shoot()
    {
        Vector2 normalizedDirection = IsometricPlayerMovementController.lastWantedDirection.normalized;
        GameObject Go = Instantiate(AmmoType.gameObject, IsoController.transform.position + 0.5f * new Vector3(normalizedDirection.x, normalizedDirection.y), Quaternion.identity);
        Go.GetComponent<Bullet>().SetRadius(AmmoType.script.Radius);
        Go.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * AmmoType.script.Speed, ForceMode2D.Impulse);
    }


    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();

        if (Input.GetAxis("Fire1") == 1 && Time.time > Lastfired)
        {
            Lastfired = fireRate + Time.time;
            Shoot();
        }
    }
}
