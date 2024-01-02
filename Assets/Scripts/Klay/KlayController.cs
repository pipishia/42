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
    public float jumpCD;
    [SerializeField] private float jumpTimer;
    public GameObject playerHP;//Kevin Add HP
    public float hp;
    public float max_hp;
    public int takeDamageAmount;
    public bool isDie;
    public bool isAttack;
    public float attackTimer;
    public float attackCD;
    public bool isSwitch;

    public int potionHeal;
    public GameObject Potion;
    public AudioClip switchaudio=null;
    public AudioClip jumpaudio;
    public AudioClip deadaudio=null;
    public AudioClip pickupaudio=null;
    public AudioClip hurtaudio=null;
    public AudioClip hitaudio=null;
    private AudioSource myAudioSource;


    void Start()
    {
        hp = max_hp;
        jumpTimer = jumpCD;
        attackTimer = attackCD;
        //switchPlayer.SetActive(false);
        isDie = false;
        myAudioSource= GetComponent<AudioSource>();
        inputControl.Disable();
        
        
    }
    private void Awake()
    {
        inputControl = new KlayInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();

        inputControl.Player.Jump.started += Jump;
        inputControl.Player.Switch.started += Switch;
        inputControl.Player.Attack.started += KlayAttack;
        //nputControl.Player.Attack.canceled += KlayAttackEnd;

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
        float _percent = hp / max_hp;
        playerHP.transform.localScale = new Vector3(_percent, playerHP.transform.localScale.y, playerHP.transform.localScale.z);
    }

    private void FixedUpdate()
    {
        Move();
        jumpTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
    }
    private void KlayAttack(InputAction.CallbackContext context)
    {

        if (attackTimer <= 0)
        {
            isAttack = true;
            attackTimer = attackCD;
                  if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = hitaudio;
        myAudioSource.Play();
        }
    }
    // private void KlayAttackEnd(InputAction.CallbackContext context)
    // {
    //     isAttack = false;
    // }

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
        if (jumpTimer <= 0)
        {
            if (physicsCheck.isGround || physicsCheck.isWall)
            {
                //Debug.Log("jump pressed");
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = jumpCD;
               if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = jumpaudio;
        myAudioSource.Play();
            }
             
        }
        
    }

    private void Switch(InputAction.CallbackContext context)
    {
        nowPos = rb.position;
        switchPlayer.transform.position = (Vector3)nowPos;
        switchPlayer.SetActive(true);
        isSwitch = true;
        inputControl.Disable();
        cameraManager.GetComponent<CameraController>().SwitchToBenno();
              if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = switchaudio;
        myAudioSource.Play();

    }  

    private void KlayIsDead()
    {
        inputControl.Disable();
        playerHP.SetActive(false);
        isDie = true;
               if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = deadaudio;
        myAudioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("scratch"))
            hp -= takeDamageAmount;
                  if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = hurtaudio;
        myAudioSource.Play();
        if (hp <= 0)
        {
            KlayIsDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("potion"))
        {
            hp += potionHeal;
            if (hp >= max_hp)
                hp = max_hp;
            Destroy(Potion);
                if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = pickupaudio;
        myAudioSource.Play();
        }

    }


}
