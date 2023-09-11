using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
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

        Vector3 move = Joystick.Instance.GetMoveDirection() * speed;
        move.y = 0;
        rigidbody.velocity = move;
        if (Joystick.Instance.GetMoveDirection() != Vector3.zero)
        {
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
        _player.transform.position = centerVehiclePos.position;
        _player.transform.SetParent(transform);
    }

    private void DetachPlayerFromVehicle()
    {
        _player.transform.localPosition = exitPos.localPosition;
        _player.transform.SetParent(null);
    }
    
    

    private void RotateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(Vector3.right, 1000f * Time.deltaTime);
        }
    }
}
