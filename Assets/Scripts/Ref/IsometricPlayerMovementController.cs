using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyIntEvent : UnityEvent<int>
{
}

public class IsometricPlayerMovementController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer FireWeapon;
    [SerializeField]
    SpriteRenderer Vacum;

    public static Vector2 lastWantedDirection = Vector2.zero;
    public float movementSpeed = 1f;
    public float AngularSpeed = 1f;
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

        BoidsManager.Instance.Predators.Add(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("RightHorizontal"), Input.GetAxisRaw("RightVertical"));
        direction = direction * AngularSpeed;

        if (direction != Vector2.zero)
        {
            if(LastDirection != direction)
            { 
                LastDirection = direction;
                isoRenderer.SetDirection(direction);
                //OwnTransform.rotation = Quaternion.Lerp(OwnTransform.rotation, Quaternion.LookRotation(LastDirection), Time.deltaTime * AngularSpeed);
            }
        }
        else if(MousePosition != Input.mousePosition)
        {
            direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            //OwnTransform.rotation = Quaternion.Lerp(OwnTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * AngularSpeed);
            MousePosition = Input.mousePosition;
            isoRenderer.SetDirection(direction);
        }

        if(direction != Vector2.zero)
        {
            lastWantedDirection = direction;
        }


        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    public void SwitchWeapon(int WeaponIndex)
    {
        if(WeaponIndex == 0)
        {
            FireWeapon.enabled = true;
            Vacum.enabled = false;
            Vacum.GetComponent<VacumWeapon>().EndVacume();
        }
        else
        {
            FireWeapon.enabled = false;
            Vacum.enabled = true;
        }
    }
}
