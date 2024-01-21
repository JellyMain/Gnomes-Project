using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementStairsAnimator : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform basementSpawnPoint;
    private AnimationEventHandler animationEventHandler;
    private Animator animator;

    private const string IS_WALKING_DOWN = "isWalkingDown";
    private const string IS_WALKING_UP = "isWalkingUp";
    private const string IS_SELECTED = "isSelected";


    private void Awake()
    {
        animationEventHandler = GetComponent<AnimationEventHandler>();
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        animationEventHandler.OnFinished += SpawnPlayer;
    }


    private void OnDisable()
    {
        animationEventHandler.OnFinished -= SpawnPlayer;
    }


    public void StartWalkDownAnimation()
    {
        animator.SetTrigger(IS_WALKING_DOWN);
    }


    public void StartWalkingUpAnimation()
    {
        animator.SetTrigger(IS_WALKING_UP);
    }


    public void SelectStairs()
    {
        animator.SetBool(IS_SELECTED, true);
    }


    public void DeselectStairs()
    {
        animator.SetBool(IS_SELECTED, false);
    }


    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, basementSpawnPoint.position, Quaternion.identity);
    }
}

