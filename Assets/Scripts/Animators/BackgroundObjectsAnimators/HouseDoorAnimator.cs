using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoorAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_SELECTED = "isSelected";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void SelectDoor()
    {
        animator.SetBool(IS_SELECTED, true);
    }


    public void DeselectDoor()
    {
        animator.SetBool(IS_SELECTED, false);
    }
}
