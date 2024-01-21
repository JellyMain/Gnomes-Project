using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private GameInput gameInput;

    private const string IS_WALKING = "isWalking";
    private const string PLAYER_WALK_DOWN_STAIRS = "PlayerWalkDownStairs";



    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameInput = GameInput.Instance;
    }


    private bool IsWalking()
    {
        return gameInput.GetPlayerNormilizedMovementInput() != Vector2.zero;
    }


    public void PlayStairsAniamation()
    {
        animator.Play(PLAYER_WALK_DOWN_STAIRS);
    }


    private void Update()
    {
        if (IsWalking())
        {
            animator.SetBool(IS_WALKING, IsWalking());
        }
        else
        {
            animator.SetBool(IS_WALKING, IsWalking());
        }
    }

}
