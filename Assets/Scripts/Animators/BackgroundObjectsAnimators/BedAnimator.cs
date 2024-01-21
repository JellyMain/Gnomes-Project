using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BedAnimator : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;
    private AnimationEventHandler animationEventHandler;


    private void Awake()
    {
        animationEventHandler = GetComponent<AnimationEventHandler>();
    }


    private void OnEnable()
    {
        animationEventHandler.OnFinished += SpawnPlayer;
    }


    private void OnDisable()
    {
        animationEventHandler.OnFinished -= SpawnPlayer;
    }



    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }
}
