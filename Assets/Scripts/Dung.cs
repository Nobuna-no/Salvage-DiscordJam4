using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dung : MonoBehaviour
{
    [SerializeField]
    Score sc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sc.Increment();
            Destroy(this.gameObject);
        }
    }
}
