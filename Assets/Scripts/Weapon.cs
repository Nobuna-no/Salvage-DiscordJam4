using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    Bullet AmmoType;

    bool fired = false;
    void Shoot()
    {
        
        GameObject Go = Instantiate(AmmoType.gameObject, FindObjectOfType<IsometricPlayerMovementController>().transform.position + 0.5f * new Vector3(IsometricPlayerMovementController.lastWantedDirection.normalized.x, IsometricPlayerMovementController.lastWantedDirection.normalized.y), Quaternion.identity);
        Go.GetComponent<Bullet>().SetRadius(AmmoType.script.Radius);
        Go.GetComponent<Rigidbody2D>().AddForce(IsometricPlayerMovementController.lastWantedDirection.normalized * AmmoType.script.Speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if(Input.GetAxis("Fire1") == 1 && !fired)
        {
            fired = true;
            Shoot();
        }
        else if(Input.GetAxis("Fire1") == 0)
        {
            fired = false;
        }
    }
}
