using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    float AttractorMultiplicator = 100.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Collectible"))
        {
            Vector3 finalPos = transform.position - collision.transform.position;
            finalPos.z = 0;
            finalPos = finalPos.normalized * AttractorMultiplicator;
            collision.GetComponent<Rigidbody2D>().AddForce(finalPos, ForceMode2D.Force);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Collectible"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
