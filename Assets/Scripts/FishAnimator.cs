using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class FishAnimator : MonoBehaviour
{
    public event Action OnStartedFading;

    [SerializeField] float fadeTime = 2f;
    [SerializeField] float delayBetweenFading = 2f;
    private Animator animator;
    private SpriteRenderer fishSpriteRenderer;
    private Fish fish;

    private const string DAMAGED = "Damaged";
    private const string DEAD = "Dead";
    private const string ATTACK = "Attack";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        fishSpriteRenderer = GetComponent<SpriteRenderer>();
        fish = GetComponentInParent<Fish>();
    }


    private void Start()
    {
        if (fish is PredatorFish)
        {
            (fish as PredatorFish).OnAttacked += PlayAttackAnimation;
        }

        fish.Hp.OnDamaged += PlayDamageAnimation;
        fish.Hp.OnDead += PlayDeathAnimation;
    }


    private void OnDisable()
    {
        if (fish is PredatorFish)
        {
            (fish as PredatorFish).OnAttacked -= PlayAttackAnimation;
        }

        fish.Hp.OnDamaged -= PlayDamageAnimation;
        fish.Hp.OnDead -= PlayDeathAnimation;
    }


    private void PlayDamageAnimation(float notNeeded)
    {
        animator.SetTrigger(DAMAGED);
    }


    private void PlayAttackAnimation()
    {
        animator.SetTrigger(ATTACK);
    }


    private async void PlayDeathAnimation()
    {
        animator.SetTrigger(DEAD);
        await Task.Delay((int)(delayBetweenFading * 1000));
        OnStartedFading?.Invoke();
        fishSpriteRenderer.DOFade(0, fadeTime).OnComplete(() => Destroy(transform.parent.gameObject));
    }

}
