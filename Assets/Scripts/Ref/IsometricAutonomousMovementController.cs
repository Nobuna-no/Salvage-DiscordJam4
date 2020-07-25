using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameWorld;

public class IsometricAutonomousMovementController : MonoBehaviour
{
    public static Vector2 lastWantedDirection = Vector2.zero;
    public float movementSpeed = 1f;
    
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    Vector2 LastDirection;
    Transform OwnTransform;
    Vector3 MousePosition;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        OwnTransform = transform;
        BoidsManager.Instance.Boids.Add(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //m_ownedActor.AddMovementInput(MathUtils.ForwardVector.Rotate(m_ownedActor.Rotation), 1);

        Vector2 groupingAcc, separationAcc, cohesionAcc;
        BoidsManager.Instance.BoidsGSC(gameObject, out groupingAcc, out separationAcc, out cohesionAcc);
        Vector2 fleeingAcc = BoidsManager.Instance.AvoidPredator(gameObject);
        Vector2 borderBouncingAcc = BoidsManager.BorderBouncing(gameObject);

        // POLISH: if near to predator, break the grouping beahviour 
        // and increase the border bouncing in order to better avoid walls in panic.
        if (fleeingAcc != Vector2.zero)
        {
            groupingAcc *= -1.0f;
            borderBouncingAcc *= 5.0f;
        }
        Vector2 forceSum = groupingAcc + separationAcc + cohesionAcc + fleeingAcc + borderBouncingAcc;
        GetComponent<Rigidbody2D>().AddForce(forceSum * movementSpeed, ForceMode2D.Force);
        isoRenderer.SetDirection(GetComponent<Rigidbody2D>().velocity);
    }
}
