using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerMovement playerController;
    public PlayerMovement player2Controller;
    public bool player1active = true;
    public bool player2active = false;
    [SerializeField]GameObject player2;
    [SerializeField] GameObject player1;
    Vector2 originalPos;
    Transform player1Pos;
    BeamAttack beamAttack;
    
    void Start(){
        player1Pos = player1.GetComponent<Transform>(); 
        originalPos = new Vector2(player1.transform.position.x, player1.transform.position.y);
        player1active = true;
        player2.SetActive(false);
        beamAttack = GameObject.FindGameObjectWithTag("Player1").GetComponent<BeamAttack>();
        // beamAttack.MethodBeam(gameObject);
    }


    void Update()
    {
         originalPos = new Vector2(player1.transform.position.x, player1.transform.position.y); 
        if(Input.GetKeyDown(KeyCode.RightShift)){
            switchPlayer();
        }
    }
    void switchPlayer(){
        player2.transform.position = originalPos;
        if(player1active){
            playerController.enabled = false;
            player2Controller.enabled = true;
            player1active = false;
            player2.SetActive(true);
            print("gg");
            player1.GetComponent<BeamAttack>().enabled = false;
             player1.GetComponent<Animator>().SetInteger("state",0);
        }
        else{
            playerController.enabled = true;
            player2Controller.enabled = false;
            player1active = true;
            player2.SetActive(false); 
            print(originalPos);
            player1.GetComponent<BeamAttack>().enabled = true;
           
        }

    }
    
}
