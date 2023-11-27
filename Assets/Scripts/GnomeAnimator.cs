using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeAnimator : MonoBehaviour
{

    private Animator animator;

    private const string DAMAGED = "Damaged";
    private const string DEAD = "Dead";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        Gnome.Instance.Hp.OnDamaged += PlayDamageAnimation;
        Gnome.Instance.Hp.OnDead += PlayDeathAnimation;
    }


    private void OnDisable()
    {
        Gnome.Instance.Hp.OnDamaged -= PlayDamageAnimation;
        Gnome.Instance.Hp.OnDead -= PlayDeathAnimation;
    }


    private void PlayDamageAnimation(float notNeeded)
    {
        animator.SetTrigger(DAMAGED);
    }


    private void PlayDeathAnimation()
    {
        animator.SetTrigger(DEAD);
    }

}
