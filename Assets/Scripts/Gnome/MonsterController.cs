using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform monTransform;
    public Transform KlayTransform;
    public Transform playerTransform;
    private int moveDirection;
    public float speed = 3f;
    public float detectionRange1;
    public float detectionRange2;
    private bool isChasing1 = false;
    private bool isChasing2 = false;
    public Vector3 originalPosition;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    public Animator monAnimator;
    public GameObject monHP;
    float hp = 10f;
    public float max_hp = 0;

    void Start()
    {
        hp = max_hp;
        monTransform = this.transform;
        originalPosition = transform.position;
        if (GameObject.Find("Klay") != null)
        {
            KlayTransform = GameObject.Find("Klay").transform;
        }
        if (GameObject.Find("player") != null)
        {
            playerTransform = GameObject.Find("player").transform;
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        float _percent = (hp / max_hp);
        monHP.transform.localScale = new Vector3(_percent, monHP.transform.localScale.y, monHP.transform.localScale.z);

        float distanceToPlayer1 = Vector2.Distance(monTransform.position, KlayTransform.position);
        float distanceToPlayer2 = Vector2.Distance(monTransform.position, playerTransform.position);

        if (distanceToPlayer1 <= detectionRange1)
        {
            isChasing1 = true;
        }
        else
        {
            isChasing1 = false;
        }
        if (distanceToPlayer2 <= detectionRange2)
        {
            isChasing2 = true;
        }
        else
        {
            isChasing2 = false;
        }

        // float originalToMonster = Vector2.Distance(originalPosition,transform.position);
        // if(originalToMonster<=5)
        // {
        //      if (distanceToPlayer <= detectionRange)
        //     {
        //         isChasing1 = true;
        //     }
        //     else
        //     {
        //         isChasing1 = false;
        //     }
        //     print("22");
        // }
        // else if(originalToMonster>=6)
        // {
        //     isChasing1 = false;
        //     print("11");
        // }
        if (isChasing1)
        {
            // 根據面朝方向設定速度
            if (KlayTransform.position.x - monTransform.position.x >= 0.3)
            {
                moveDirection = 1;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (KlayTransform.position.x - monTransform.position.x <= -0.3)
            {
                moveDirection = -1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                moveDirection = 0;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            AttackPlayer();
            transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);// 移動怪物
        }
        else if (isChasing2)
        {
            // 根據面朝方向設定速度
            if (playerTransform.position.x - monTransform.position.x >= 0.3)
            {
                moveDirection = 1;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (playerTransform.position.x - monTransform.position.x <= -0.3)
            {
                moveDirection = -1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                moveDirection = 0;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            AttackPlayer();
            transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);// 移動怪物
        }
        else
        {
            monAnimator.SetInteger("Chase", 0);
            ReturnToOriginalPosition();
        }

    }
    void ReturnToOriginalPosition()
    {
        float step = speed * Time.deltaTime;
        if (transform.position.x >= originalPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        float newX = Mathf.MoveTowards(transform.position.x, originalPosition.x, step);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    void AttackPlayer()
    {
        // 檢查攻擊冷卻時間
        if (Time.time - lastAttackTime > attackCooldown)
        {
            monAnimator.SetInteger("Chase", 1);// 執行攻擊邏輯，這裡你可以添加傷害等具體操作
            // 更新上一次攻擊時間
            lastAttackTime = Time.time;
        }
        else
        {
            monAnimator.SetInteger("Chase", 0);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "hit")
        {
            hp -= 1;
        }
    }

}
