using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float totalTime;
    private float startingIntencity;

    public static CinemachineShake Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }


    private void OnEnable()
    {
        Player.Instance.Hp.OnDamaged += OnDamagedShake;
    }


    public void ShakeCamera(float intencity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        startingIntencity = intencity;
        totalTime = time;

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intencity;
        shakeTimer = time;
    }


    private void OnDamagedShake(float notNeeded)
    {
        ShakeCamera(3, 0.5f);
    }


    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntencity, 0, 1 - (shakeTimer / totalTime));
        }
    }
}
