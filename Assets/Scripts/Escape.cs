using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    List<GameObject> m_Escapist;

    [SerializeField]
    float escapeRate;

    private void Start()
    {
        m_Escapist = new List<GameObject>();
        StartCoroutine(SuccessfullEscape());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!m_Escapist.Contains(collision.gameObject))
            {
                m_Escapist.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(m_Escapist.Contains(collision.gameObject))
            { 
                m_Escapist.Remove(collision.gameObject);
            }
        }
    }

    IEnumerator SuccessfullEscape()
    {
        yield return new WaitForSeconds(escapeRate);
        if (m_Escapist.Count != 0)
        {
            GameObject go = m_Escapist[Random.Range(0, m_Escapist.Count)];
            m_Escapist.Remove(go);
            BoidsManager.Instance.Boids.Remove(go);
            Destroy(go);
        }
        StartCoroutine(SuccessfullEscape());
    }

}
