using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
  [SerializeField] private Slider slider;
  float battery = 0f;
  float max_battery = 3f;
  public float increaseAmount;
  [SerializeField]GameObject door;
  
  

  private void OnCollisionEnter2D(Collision2D collision)
  {
    GameObject beamObject = GameObject.FindGameObjectWithTag("Beam");
    if (collision.gameObject.tag == "Beam")
    {
      Debug.Log("hit");
      ChargeUp();
      Destroy(collision.gameObject);
    }

  }
  public void UpdateCharger(float current_val, float max_val)
  {
    slider.value = current_val / max_val;
  }

  public void ChargeUp()
  {
    battery += increaseAmount;
    UpdateCharger(battery, max_battery);

    if (battery>=max_battery){
      door.SetActive(false); 
    }
  }


}
