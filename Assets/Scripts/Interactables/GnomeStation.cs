using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeStation : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject gnomeStationUI;
    [SerializeField] InteractButton interactButton;


    private void OnEnable()
    {
        GameInput.OnPlayerCloseUIAction += HideGnomeStationUI;
    }


    private void OnDisable()
    {
        GameInput.OnPlayerCloseUIAction -= HideGnomeStationUI;
    }


    public void Interact()
    {
        interactButton.PressButton();
        gnomeStationUI.SetActive(true);
    }


    private void HideGnomeStationUI()
    {
        gnomeStationUI.SetActive(false);
    }


    public void SelectItem()
    {
        interactButton.ShowButton();
    }


    public void DeselectItem()
    {
        interactButton.HideButton();
    }
}
