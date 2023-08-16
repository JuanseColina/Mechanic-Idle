using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform centerVehiclePos, exitPos;

    private void Start()
    {
        EventsManager.Instance.ActionEnterInAVehicle += OnEnterInAVehicle;
        EventsManager.Instance.ActionExitFromVehicle += OnExitFromVehicle;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionEnterInAVehicle -= OnEnterInAVehicle;
        EventsManager.Instance.ActionExitFromVehicle -= OnExitFromVehicle;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnCanEnterInAVehicle(true);
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnCanEnterInAVehicle(false);
            playerTransform = null;
        }
    }

    private void OnEnterInAVehicle()
    {
        playerTransform.rotation = transform.rotation;
        playerTransform.position = centerVehiclePos.position;
        transform.SetParent(playerTransform);
        CamController.Instance.ChangeLookAtCam(transform);
    }
    
    private void OnExitFromVehicle()
    {
        playerTransform.position = exitPos.position;
        transform.SetParent(null);
        CamController.Instance.ChangeLookAtCam(playerTransform);
    }
}
