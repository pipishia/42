using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BennoController : MonoBehaviour
{
    [SerializeField] GameObject switchPlayer;   
    public BennoInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private SpriteRenderer sr;
    [Header("Basic parameter")]
    public float speed;
    public float jumpForce;
    public GameObject playerHP;//梁家祥加HP
    float hp = 10f;
    public float max_hp;

    void Start()
    {
        hp = max_hp;
    }
    private void Awake()
    {
        inputControl = new BennoInputControl();
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
        //switchPlayer.SetActive(true);
        //new KlayInputControl input = switchPlayer.GetComponents<KlayInputControl>();
        

       
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "scratch")
        {
            hp -= 1;
            print("scratch");
        }
    }
}
