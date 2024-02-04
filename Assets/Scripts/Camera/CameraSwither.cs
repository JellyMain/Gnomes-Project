using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwither : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera firstCamera;
    [SerializeField] CinemachineVirtualCamera secondCamera;



    public void SwitchToSecondCamera()
    {
        firstCamera.Priority = 0;
        secondCamera.Priority = 1;
    }


    public void SwitchToFirstCamera()
    {
        firstCamera.Priority = 1;
        secondCamera.Priority = 0;
    }

}
