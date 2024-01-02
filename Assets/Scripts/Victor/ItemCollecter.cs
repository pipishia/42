using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollecter : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField]private Text CherriesText;
    private void  OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Cherry")){
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log(cherries);
            CherriesText.text = "cherries:"+cherries;
        }
    }
   

}
