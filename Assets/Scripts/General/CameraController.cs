using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cameraMy;  
    public GameObject benno;
    public GameObject klay;
    public void SwitchToBenno(){
        cameraMy.Follow = benno.transform;
        cameraMy.LookAt = benno.transform;
    }
    public void SwitchToklay(){
        cameraMy.Follow = klay.transform;
        cameraMy.LookAt = klay.transform;
    }
}
