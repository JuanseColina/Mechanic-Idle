using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController Instance;
    
    
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera[] washingMachineCams;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        LookAtPlayerCam();
    }

    public void LookAtPlayerCam()
    {
        ResetCamPriority();
        playerCam.Priority = 1;
    }

    // void to change between playerCam an vehicleCam
    public void ChangeLookAtCam(Transform target)
    {
        playerCam.Follow = target;
    }
    
    private int _currentWashingMachineCam;
    public void ChangeBetweenWashingCameras()
    {
        ResetCamPriority();
        washingMachineCams[_currentWashingMachineCam].Priority = 1;
        if (_currentWashingMachineCam < washingMachineCams.Length - 1)
        {
            _currentWashingMachineCam++;
        }
        else
        {
            _currentWashingMachineCam = 1;
        }
    }

    public void WashingMachineCams()
    {
        ResetCamPriority();
        washingMachineCams[0].Priority = 1;
    }
    
    
    private void ResetCamPriority()
    {
        playerCam.Priority = 0;
        foreach (var cam in washingMachineCams)
        {
            cam.Priority = 0;
        }
    }
}
