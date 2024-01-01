using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class KlayController : MonoBehaviour
{
    public GameObject cameraManager;
    public GameObject switchPlayer;
    private Vector2 nowPos;
    public KlayInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private SpriteRenderer sr;
    [Header("Basic parameter")]
    //public float defSpeed;
    public float speed;
    //public float defJumpForce;
 
    public float jumpForce;
    public GameObject playerHP;//梁家祥加HP
    float hp = 10f;
    public float max_hp;

    void Start()
    {
        hp = max_hp;
        switchPlayer.SetActive(false);
    }
    private void Awake()
    {
        inputControl = new KlayInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();

        inputControl.Player.Jump.started += Jump;
        inputControl.Player.Switch.started += Switch;
 
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    void Update()
    {
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
        if (hp <= 0)
        {
            //Destroy(gameObject);
            
        }
        float _percent = (hp / max_hp);
        playerHP.transform.localScale = new Vector3(_percent, playerHP.transform.localScale.y, playerHP.transform.localScale.z);
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

    private void Switch(InputAction.CallbackContext context)
    {
        nowPos = rb.position;
        switchPlayer.transform.position = (Vector3)nowPos;
        switchPlayer.SetActive(true);
        
        inputControl.Disable();
        cameraManager.GetComponent<CameraController>().SwitchToBenno();
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("scratch"))
        {
            hp -= 1;
            print("scratch");
        }
    }

}
