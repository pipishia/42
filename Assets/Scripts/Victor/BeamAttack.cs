using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
  public Transform Firepoint;
  public GameObject beamPrefab;
  public Rigidbody2D rb;
  public GameObject player;
  private GameObject beam;
  public float next_spawn_time;
  private float spawnTimer;
  public bool canShoot;

  void Start()
  {
    //beam.SetActive(false);
    spawnTimer = next_spawn_time;
  }

  void Update()
  {
    spawnTimer -= Time.deltaTime;
    if (spawnTimer <= 0 )
    {
      Shoot();
      spawnTimer = next_spawn_time;
    }
  }
  void Shoot()
  {
    if (Input.GetKey(KeyCode.Q))
    {
      if (player.GetComponent<Transform>().rotation.y < 0)
      {
        beam = Instantiate(beamPrefab, Firepoint.position, Firepoint.rotation);
        Debug.Log("You're looking Left");
        Destroy(beam, 1.0f);
      }

      else if (player.GetComponent<Transform>().rotation.y == 0)
      {
        beam = Instantiate(beamPrefab, Firepoint.position, Firepoint.rotation);
        Debug.Log("You're looking Right");
        Destroy(beam, 1.0f);
      }
    }
  }
}






