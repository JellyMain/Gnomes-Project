using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoor : MonoBehaviour, IInteractable
{
    [SerializeField] HouseDoorAnimator houseDoorAnimator;

    public void Interact()
    {
        SceneLoader.Instance.LoadMapScene();
    }

    public void SelectItem()
    {
        houseDoorAnimator.SelectDoor();
    }

    public void DeselectItem()
    {
        houseDoorAnimator.DeselectDoor();
    }
}
