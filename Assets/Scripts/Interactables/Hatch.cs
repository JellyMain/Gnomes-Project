using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour, IInteractable
{
    [SerializeField] CameraSwither cameraSwither;
    [SerializeField] BasementStairsAnimator basementStairsAnimator;
    [SerializeField] HatchAnimator hatchAnimator;


    public void Interact()
    {
        cameraSwither.SwitchToSecondCamera();
        hatchAnimator.StartOpenHatchAnimation();
        basementStairsAnimator.StartWalkDownAnimation();
    }


    public void SelectItem()
    {
        hatchAnimator.SelectHatch();
    }


    public void DeselectItem()
    {
        hatchAnimator.DeselectHatch();
    }
}
