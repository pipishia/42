using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public bool isWall;
    public LayerMask groundCheckLayer;
    public float bottomCheckRadius;
    public float sideCheckRadius;


    public Vector2 bottomOffset;
    public Vector2 sideOffset;

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        //check is on the ground
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, bottomCheckRadius, groundCheckLayer);
        isWall = Physics2D.OverlapCircle((Vector2)transform.position + sideOffset, sideCheckRadius, groundCheckLayer);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, bottomCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + sideOffset, sideCheckRadius);
    }




}
