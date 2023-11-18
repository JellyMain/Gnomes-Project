using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishAnimator : MonoBehaviour
{
    [SerializeField] float fadeTime = 2f;
    private Animator animator;
    private SpriteRenderer fishSpriteRenderer;
    private Fish fish;


    private const string DAMAGED = "Damaged";
    private const string DEAD = "Dead";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        fishSpriteRenderer = GetComponent<SpriteRenderer>();
        fish = GetComponentInParent<Fish>();
    }


    private void Start()
    {
        fish.Hp.OnDamaged += PlayDamageAnimation;
        fish.Hp.OnDead += PlayDeathAnimation;
    }


    private void PlayDamageAnimation(float notNeeded)
    {
        animator.SetTrigger(DAMAGED);
    }


    private void PlayDeathAnimation()
    {
        animator.SetTrigger(DEAD);
        fishSpriteRenderer.DOFade(0, fadeTime).OnComplete(() => Destroy(transform.parent.gameObject));
    }
}
