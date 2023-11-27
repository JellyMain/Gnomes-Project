using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour, IInteractable
{
    [SerializeField] CameraSwither cameraSwither;
    private bool isInArea = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInArea)
        {
            Interact();
        }
    }


    public void Interact()
    {
        cameraSwither.SwitchToSecondCamera();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = false;
        }
    }

}
