using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private float speed;

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform centerVehiclePos, exitPos;
    [SerializeField] private GameObject[] wheels;
    
    private Player _player;
    private bool isOnVehicle;

    private void Awake()
    {
        speed = car.Speed;
    }

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
            InitializePlayer(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetPlayer();
        }
    }

    private void InitializePlayer(Collider playerCollider)
    {
        _player = playerCollider.GetComponent<Player>();
        EventsManager.Instance.OnCanEnterInAVehicle(true);
        playerTransform = playerCollider.transform;
    }

    private void ResetPlayer()
    {
        _player = null;
        EventsManager.Instance.OnCanEnterInAVehicle(false);
        playerTransform = null;
    }
    
    private void OnEnterInAVehicle()
    {
        isOnVehicle = true;
        _player.ActualVehicle = gameObject;
        _player.ChangeSpeed(speed);
        AlignPlayerWithVehicle();
        CamController.Instance.ChangeLookAtCam(transform);
    }

    private void OnExitFromVehicle()
    {
        isOnVehicle = false;
        _player.ActualVehicle = null;
        DetachPlayerFromVehicle();
        CamController.Instance.ChangeLookAtCam(playerTransform);
    }

    private void AlignPlayerWithVehicle()
    {
        playerTransform.rotation = transform.rotation;
        TweenPlayerVehicle(playerTransform, centerVehiclePos, () =>
        {
            if (centerVehiclePos != exitPos)
                transform.SetParent(playerTransform);
        });
    }

    private void DetachPlayerFromVehicle()
    {
        transform.SetParent(null);
        TweenPlayerVehicle(playerTransform, exitPos);
    }

    private void TweenPlayerVehicle(Transform playerTr, Transform position, System.Action onComplete = null)
    {
        LeanTween.move(playerTr.gameObject, position.position, 0.1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(onComplete);
    }

    private void Update()
    {
        if (!isOnVehicle) return;

        if(Joystick.Instance.GetMoveDirection() != Vector3.zero) 
            RotateWheels();
    }

    private void RotateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(Vector3.right, 1000f * Time.deltaTime);
        }
    }
}
