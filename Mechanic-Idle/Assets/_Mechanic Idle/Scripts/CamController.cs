using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController Instance;
    
    
    [SerializeField] private CinemachineVirtualCamera playerCam, vehicleCam;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // void to change between playerCam an vehicleCam
    public void ChangeLookAtCam(Transform target)
    {
        playerCam.Follow = target;
    }
}
