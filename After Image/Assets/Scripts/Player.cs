using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runMultiplier = 1.5f;

    Rigidbody2D myRigidbody2D;
    Vector2 moveInput;
    bool isRunning = false;
    float moveMultiplier;


    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            moveMultiplier = moveSpeed * runMultiplier;
        } else {
            moveMultiplier = moveSpeed;
        }

        myRigidbody2D.MovePosition(myRigidbody2D.position + moveInput * moveMultiplier * Time.fixedDeltaTime);
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
}
