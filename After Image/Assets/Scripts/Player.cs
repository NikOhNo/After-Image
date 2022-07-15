using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Player Stats
    [SerializeField] int health = 100;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runMultiplier = 1.5f;
    [SerializeField] Transform firePoint;

    // Movement Variables
    Rigidbody2D myRigidbody2D;
    Vector2 moveInput;
    bool isRunning = false;
    float moveMultiplier;

    // Weapon Variables
    Vector3 mousePosition;


    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveAndLook();
    }

    private void MoveAndLook()
    {
        //move
        if (isRunning)
        {
            moveMultiplier = moveSpeed * runMultiplier;
        }
        else
        {
            moveMultiplier = moveSpeed;
        }
        myRigidbody2D.MovePosition(myRigidbody2D.position + moveInput * moveMultiplier * Time.fixedDeltaTime);

        //look at where the mouse is
        var dir = mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Movement Controls
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    // Weapon Controls
    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Fire!");
            FireGun();
        }
    }

    private void FireGun()
    {
        // NEED TO GET A DIRECTION VECTOR FROM FIRE POINT TO MOUSE (plug this into transform.right everywhere here)
        Debug.Log(transform.right);
        LineRenderer smokeTrail = GetComponentInChildren<LineRenderer>();
        Ray2D ray = new Ray2D(firePoint.position, transform.right);
        RaycastHit2D hitData = Physics2D.Raycast(firePoint.position, transform.right, Mathf.Infinity);
        if(hitData.collider != null)
        {
            Debug.Log("Hit Something!");

            // ADD TEXTURE TO SMOKETRAIL
            smokeTrail.SetPosition(0, firePoint.position);
            smokeTrail.SetPosition(1, hitData.point);
        }
    }

    // Recording Controls
    void OnRecordAndReplay(InputValue value)
    {
        if (value.isPressed)
        {
            FindObjectOfType<RecordMenu>().RecordAtPosition();
        }
    }

    void OnRecordMenu(InputValue value)
    {
        if(value.Get<float>() > 0)
        {
            FindObjectOfType<RecordMenu>().MovePositionRight();
        }
        if(value.Get<float>() < 0)
        {
            FindObjectOfType<RecordMenu>().MovePositionLeft();
        }
    }

    void OnMousePosition(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }
}
