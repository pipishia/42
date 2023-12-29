using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class KlayController : MonoBehaviour
{
    public KlayInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private SpriteRenderer sr;
    [Header("Basic parameter")]
    public float defSpeed;
    public float speed;
    public float targetSpeed;
    public float sprintRate;
    public float jumpForce;


    private void Awake()
    {
        inputControl = new KlayInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();

        inputControl.Player.Jump.started += Jump;
        inputControl.Player.Sprint.performed += Sprint;
        inputControl.Player.Sprint.canceled += SprintCanceled;
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //change player speed

        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //change player flip

        int faceDir = (int)inputDirection.x;
        if (faceDir < 0)
            sr.flipX = true;
        if (faceDir > 0)
            sr.flipX = false;
        // transform.localScale = new Vector3( )
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround || physicsCheck.isWall)
        {
            Debug.Log("jump pressed");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            Debug.Log("sprint pressed");
            //speed += (targetSpeed - speed) * sprintRate;
            speed = targetSpeed;
        }
    }
    private void SprintCanceled(InputAction.CallbackContext context)
    {
        speed = defSpeed;
    }

}
