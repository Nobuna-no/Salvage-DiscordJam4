using UnityEngine;

public class Dung : MonoBehaviour
{
    [SerializeField]
    Score sc;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Vacuum"))
        {
            sc.Increment();
            Destroy(gameObject);
        }
    }
}
