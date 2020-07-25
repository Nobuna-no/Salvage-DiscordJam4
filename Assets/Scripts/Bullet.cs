using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletSO script;

    [SerializeField]
    CircleCollider2D collider2d;

    public void SetRadius(float value)
    {
        collider2d.radius = value;
    }
}
