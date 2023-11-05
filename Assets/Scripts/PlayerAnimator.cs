using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string DAMAGED = "Damaged";
    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        Player.Instance.Hp.OnDamaged += PlayDamageAnimation;

    }


    private void OnDisable()
    {
        Player.Instance.Hp.OnDamaged += PlayDamageAnimation;
    }


    private void PlayDamageAnimation(float notNeeded)
    {
        animator.SetTrigger(DAMAGED);
    }

}
