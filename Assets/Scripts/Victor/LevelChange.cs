using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Enter_trigger");
        if(collision.tag =="Player"){
            Debug.Log("SwitchScenetoPass");
            SceneManager.LoadScene("Pass");
        }
    }

}
