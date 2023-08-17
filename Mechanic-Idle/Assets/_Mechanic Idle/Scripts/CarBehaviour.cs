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
            EventsManager.Instance.OnCanEnterInAVehicle(true, this.gameObject);
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnCanEnterInAVehicle(false, null);
            playerTransform = null;
        }
    }

    private void OnEnterInAVehicle( GameObject vehicle)
    {
        playerTransform.rotation = transform.rotation;
        TweenPlayerVehicle(playerTransform, centerVehiclePos);
        CamController.Instance.ChangeLookAtCam(transform);
    }

    private void TweenPlayerVehicle(Transform playerTr, Transform position)
    {

        LeanTween.move(playerTr.gameObject, position.position, 0.1f).setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() =>
            {
                if (position != exitPos) transform.SetParent(playerTransform);
            });
    }

    private void OnExitFromVehicle()
    {
        transform.SetParent(null);
        TweenPlayerVehicle(playerTransform, exitPos);
        CamController.Instance.ChangeLookAtCam(playerTransform);
    }
}
