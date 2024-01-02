using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D colli;
    private Animator anim;
    private float dirX = 0f;
    private SpriteRenderer sprite;
    private Transform transform;
    [SerializeField]private float movespeed = 7f;
    [SerializeField]private float jumpforce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState{Idle,Running,Jumping,Falling,Attack}
    // [SerializeField] GameObject Firepoint;


    // Start is called before the first frame update
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        colli = GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
       
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed,rb.velocity.y);

        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x,jumpforce);
        }
        UpdateAnimationState();  
    }
    private void UpdateAnimationState(){
        MovementState state;
        //  Transform firePointTransform = Firepoint.GetComponent<Transform>();

         if(dirX>0f){
            state = MovementState.Running;
          transform.rotation = Quaternion.Euler(0, 0, 0);
            // firePointTransform.localScale = new Vector3(-1f, 1f, 1f);

        }
        else if(dirX <0f){
              state = MovementState.Running;
               transform.rotation = Quaternion.Euler(0, 180, 0);
            // firePointTransform.localScale = new Vector3( -firePointTransform.localScale, 1f, 1f);
        }
        else{
               state = MovementState.Idle;
        }
        if (rb.velocity.y >.1f){
             state = MovementState.Jumping;
        }
        if (rb.velocity.y<-.1f){
            state = MovementState.Falling;
        }
        if (Input.GetKey(KeyCode.E)){
            state = MovementState.Attack;
        }
        anim.SetInteger("state",(int)state);
    }
    private bool IsGrounded(){
        return Physics2D.BoxCast(colli.bounds.center,colli.bounds.size,0f,Vector2.down,.1f,jumpableGround);
    }

}
