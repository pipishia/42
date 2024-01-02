using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject cameraManager;
    public GameObject Charger;
    private Animator animator;
    //public GameObject Gate;
    public AudioClip passaudio;
    private AudioSource myAudioSource;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Charger.GetComponent<Charger>().gateIsOpen)
        {
            GateOpen();
        }

    }
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         animator.SetInteger("close", 1);

    //         StartCoroutine(WaitForAnimation());
    //         if (myAudioSource == null)
    //         {
    //             myAudioSource = gameObject.AddComponent<AudioSource>();
    //         }
    //         myAudioSource.clip = passaudio;
    //         myAudioSource.Play();
    //     }
    // }
    void GateOpen()
    {
        cameraManager.GetComponent<CameraController>().SwitchToGate();
        animator.SetInteger("close", 1);

        StartCoroutine(WaitForAnimation());
        if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        myAudioSource.clip = passaudio;
        myAudioSource.Play();
    }
    IEnumerator WaitForAnimation()
    {

        yield return new WaitForSeconds(2f);
        cameraManager.GetComponent<CameraController>().SwitchToBenno();
        Destroy(gameObject);
    }
}