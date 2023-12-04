using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CamController : MonoBehaviour
{
    public static CamController Instance;
    
    
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera[] washingMachineCams;

    private CinemachineFramingTransposer transposerCamera;
    
    float originalDistance;
    float originalXDamping;
    float originalYDamping;
    float originalZDamping;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        transposerCamera = mainCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        originalDistance = transposerCamera.m_CameraDistance;
        originalXDamping = transposerCamera.m_XDamping;
        originalYDamping = transposerCamera.m_YDamping;
        originalZDamping = transposerCamera.m_ZDamping;
    }

    private void Start()
    {
        SetMainCam();
    }

    public void SetMainCam()
    {
        ResetCamPriority();
        mainCam.Priority = 1;
    }

    public void CamFollowAtVehicle(Transform target)
    {
        mainCam.Follow = target;
        LeanTween.value(transposerCamera.m_CameraDistance, transposerCamera.m_CameraDistance + 5, .35f)
            .setOnUpdate((value) =>
            {
                transposerCamera.m_CameraDistance = value;
            });
        LeanTween.value(transposerCamera.m_XDamping, 0, .5f)
            .setOnUpdate((value) =>
            {
                transposerCamera.m_XDamping = value;
                transposerCamera.m_YDamping = value;
                transposerCamera.m_ZDamping = value;
            });
    }


    public void CamFollowAtPlayer(Transform playerTransform)
    {
        mainCam.Follow = playerTransform;
        LeanTween.value(transposerCamera.m_CameraDistance, originalDistance, .35f)
            .setOnUpdate((value) => transposerCamera.m_CameraDistance = value);

        LeanTween.value(transposerCamera.m_XDamping, originalXDamping, .5f)
            .setOnUpdate((value) => 
            {
                transposerCamera.m_XDamping = value;
                transposerCamera.m_YDamping = originalYDamping;
                transposerCamera.m_ZDamping = originalZDamping;
            });
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
            _currentWashingMachineCam = 0;
        }
    }

    public void WashingMachineCams()
    {
        ResetCamPriority();
        _currentWashingMachineCam = 0;
        ChangeBetweenWashingCameras();
    }


    private void ResetCamPriority()
    {
        mainCam.Priority = 0;
        foreach (var cam in washingMachineCams)
        {
            cam.Priority = 0;
        }
    }
}
