using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private const string IS_SLOT_SELECTED = "isSlotSelected";
    private const string IS_SLOT_DESELECTED = "isSlotDeselected";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger(IS_SLOT_SELECTED);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger(IS_SLOT_DESELECTED);
    }
}
