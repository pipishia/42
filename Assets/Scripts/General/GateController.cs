using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator animator;
    public GameObject Gate;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetInteger("close", 1);

            StartCoroutine(WaitForAnimation());
        }
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(Gate);
    }
}