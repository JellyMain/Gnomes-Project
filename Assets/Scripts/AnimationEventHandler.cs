using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinished;

    private void OnAnimationFinishTrigger()
    {
        OnFinished?.Invoke();
    }
}
