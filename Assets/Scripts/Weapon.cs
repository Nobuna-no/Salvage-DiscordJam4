using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private readonly int IsometricRangePerYUnit = 100;

    [SerializeField]
    Bullet AmmoType;

    float Lastfired;

    [SerializeField]
    float fireRate = 1.0f;

   

    [SerializeField]
    IsometricPlayerMovementController IsoController;



    [Header("Weapon - RENDERING")]
    [SerializeField, Range(-1, 1)]
    float weaponDistanceFromPlayer = 1;
    [SerializeField, Range(-360, 360)]
    float bonusDegree = 90f;
    [SerializeField, Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    private int TargetOffsetFactor = -50;

    void Shoot()
    {
        Vector2 normalizedDirection = IsometricPlayerMovementController.lastWantedDirection.normalized;
        GameObject Go = Instantiate(AmmoType.gameObject, IsoController.transform.position + 0.5f * new Vector3(normalizedDirection.x, normalizedDirection.y), Quaternion.identity);
        Go.GetComponent<Bullet>().SetRadius(AmmoType.script.Radius);
        Go.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * AmmoType.script.Speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        Vector2 normalizedDirection = IsometricPlayerMovementController.lastWantedDirection.normalized;
        transform.position = IsoController.transform.position + weaponDistanceFromPlayer * new Vector3(normalizedDirection.x, normalizedDirection.y);

        transform.eulerAngles = new Vector3(0,0, Mathf.Rad2Deg * Mathf.Atan2(normalizedDirection.x, -normalizedDirection.y) + bonusDegree);

        float offset = transform.position.y - IsoController.transform.position.y;
        GetComponent<Renderer>().sortingOrder = -(int)(IsoController.transform.position.y * IsometricRangePerYUnit) + (int)(offset * TargetOffsetFactor);
        

        if (Input.GetAxis("Fire1") == 1 && Time.time > Lastfired)
        {
            Lastfired = fireRate + Time.time;
            Shoot();
        }
    }
}
