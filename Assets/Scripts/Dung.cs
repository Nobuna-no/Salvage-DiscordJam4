using UnityEngine;

public class Dung : MonoBehaviour
{
    [SerializeField]
    Score sc;

    Vector2 m_MinScale = new Vector2(0.1f, 0.1f);
    float CurrenTime = 0.0f;

    [SerializeField, Range(0.1f, 3)]
    float TimeToAbsorb = 1f;

	bool wasVacuumed = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
		if (wasVacuumed)
			return;

        if(collision.gameObject.layer == LayerMask.NameToLayer("Vacuum"))
        {
            if (CurrenTime >= TimeToAbsorb)
            {
                sc.Increment();
                Destroy(gameObject);
				wasVacuumed = true;
			}
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, m_MinScale, TimeToAbsorb * Time.deltaTime);
                CurrenTime += Time.deltaTime;
            }
        }
    }
}
