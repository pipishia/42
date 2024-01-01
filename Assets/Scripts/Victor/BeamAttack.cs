using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamAttack : MonoBehaviour
{
  public Transform Firepoint;
  public GameObject beamPrefab;
  //public Rigidbody2D rb;
  public BennoInputControl inputControl;
  public GameObject player;
  private GameObject beam;
  public float next_spawn_time;
  [SerializeField] private float spawnTimer;
  //public bool canShoot;

  private void Awake()
  {
    inputControl = new BennoInputControl();

    inputControl.Player.Shoot.performed += Shoot;

  }

  private void OnEnable()
  {
    inputControl.Enable();
  }

  private void OnDisable()
  {
    inputControl.Disable();
  }

  void Start()
  {
    //beam.SetActive(false);
    spawnTimer = next_spawn_time;
  }

  void FixedUpdate()
  {
    spawnTimer -= Time.deltaTime;
  }

  private void Shoot(InputAction.CallbackContext context)
  {
    //print("shoot pressed");
    if (spawnTimer <= 0)
    {
      //print("shoot!");

      if (player.GetComponent<SpriteRenderer>().flipX)
      {
        Firepoint.Rotate(new Vector3(0, 180, 0));
        beam = Instantiate(beamPrefab, Firepoint.position, Firepoint.rotation);
        print("Benno looking Left");
        Firepoint.Rotate(new Vector3(0, -180, 0));
        //Destroy(beam, 1.0f);
      }
      else
      {
        
        beam = Instantiate(beamPrefab, Firepoint.position, Firepoint.rotation);
        print("Benno looking Right");
        
        //Destroy(beam, 1.0f);
        
      }
      spawnTimer = next_spawn_time;
    }
  }
}






