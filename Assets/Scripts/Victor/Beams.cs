using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Beams : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = -1 * speed * transform.right;
        rb.velocity = speed * transform.right;

    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }

}
