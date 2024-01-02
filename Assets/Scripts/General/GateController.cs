using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator animator;
    public GameObject Gate;
    public AudioClip passaudio;
     private AudioSource myAudioSource;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
        myAudioSource= GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetInteger("close", 1);

            StartCoroutine(WaitForAnimation());
                     if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = passaudio;
        myAudioSource.Play();
        }
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(Gate);
    }
}