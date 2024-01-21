using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchAnimator : MonoBehaviour
{
    private AnimationEventHandler animationEventHandler;
    private Animator animator;
    private const string IS_INTERACTED = "isInteracted";
    private const string IS_SELECTED = "isSelected";


    private void Awake()
    {
        animationEventHandler = GetComponent<AnimationEventHandler>();
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        animationEventHandler.OnFinished += DestroyPlayer;
    }


    private void OnDisable()
    {
        animationEventHandler.OnFinished -= DestroyPlayer;
    }


    public void StartOpenHatchAnimation()
    {
        animator.SetTrigger(IS_INTERACTED);
    }


    public void SelectHatch()
    {
        animator.SetBool(IS_SELECTED, true);
    }


    public void DeselectHatch()
    {
        animator.SetBool(IS_SELECTED, false);
    }


    private void DestroyPlayer()
    {
        Destroy(PlayerMovement.Instance.gameObject);
    }
}
