using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private const string IS_HOUSE_SELECTED = "isHouseSelected";
    private const string IS_HOUSE_DESELECTED = "isHouseDeselected";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger(IS_HOUSE_SELECTED);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger(IS_HOUSE_DESELECTED);
    }
}
