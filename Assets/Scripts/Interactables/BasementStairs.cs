using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementStairs : MonoBehaviour, IInteractable
{
    [SerializeField] CameraSwither cameraSwither;
    [SerializeField] BasementStairsAnimator basementStairsAnimator;
    [SerializeField] Transform bedroomSpawnPoint;

    public void Interact()
    {
        basementStairsAnimator.StartWalkingUpAnimation();
        cameraSwither.SwitchToFirstCamera();
        PlayerMovement.Instance.transform.position = bedroomSpawnPoint.position;
    }

    public void SelectItem()
    {
        basementStairsAnimator.SelectStairs();
    }

    public void DeselectItem()
    {
        basementStairsAnimator.DeselectStairs();
    }
}
