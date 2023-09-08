using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Car car;
    [SerializeField] private float speed;
    [SerializeField] private float drivingControl;

    [Header("References")] 
    [SerializeField] private Transform centerVehiclePos;
    [SerializeField] private Transform exitPos;
    [SerializeField] private GameObject[] wheels;
    
    private Player _player;
    private bool isOnVehicle;

    private void Awake()
    {
        speed = car.Speed;
        drivingControl = car.DrivingControl;
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

    private void Update()
    {
        if (!isOnVehicle) return;

        if (Joystick.Instance.GetMoveDirection() != Vector3.zero)
        {
            controller.Move(Joystick.Instance.GetMoveDirection() * (speed * Time.deltaTime));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Joystick.Instance.GetMoveDirection()), drivingControl * Time.deltaTime);
            RotateWheels();
        }
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
    }

    private void ResetPlayer()
    {
        _player = null;
        EventsManager.Instance.OnCanEnterInAVehicle(false);
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
        CamController.Instance.ChangeLookAtCam(_player.transform  );
    }

    private void AlignPlayerWithVehicle()
    {
        _player.transform.rotation = transform.rotation;
        transform.position = centerVehiclePos.position;
        _player.transform.SetParent(transform);
    }

    private void DetachPlayerFromVehicle()
    {
        _player.transform.position = exitPos.position;
        _player.transform.parent = null;
        // TweenPlayerVehicle(positionForPlayerInCar, exitPos);
    }

    // private void TweenPlayerVehicle(Transform playerTr, Transform position, System.Action onComplete = null)
    // {
    //     LeanTween.move(playerTr.gameObject, position.position, 0.1f)
    //         .setEase(LeanTweenType.easeInOutSine)
    //         .setOnComplete(onComplete);
    // }

    

    private void RotateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(Vector3.right, 1000f * Time.deltaTime);
        }
    }
}
