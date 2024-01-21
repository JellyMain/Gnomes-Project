using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] GameObject buttonVisuals;
    [SerializeField] Animator buttonAnimator;

    private const string IS_CLICKED = "isClicked";

    private void Start()
    {
        buttonVisuals.SetActive(false);
    }


    public void PressButton()
    {
        buttonAnimator.SetTrigger(IS_CLICKED);
    }


    public void ShowButton()
    {
        buttonVisuals.SetActive(true);
    }

    public void HideButton()
    {
        buttonVisuals.SetActive(false);
    }

}
