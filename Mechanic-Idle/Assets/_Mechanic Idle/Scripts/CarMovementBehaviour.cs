using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Car car;
    private float speed;
    private float drivingControl;
    private float wheelsDirectionControl;

    [Header("References")] 
    [SerializeField] private Transform exitPos;
    [SerializeField] private GameObject[] frontWheels;
    [SerializeField] private GameObject[] backWheels;
    
    private Player _player;
    private Transform _playerTransform;
    private bool isOnVehicle;

    private void Awake()
    {
        speed = car.Speed;
        drivingControl = car.DrivingControl;
        wheelsDirectionControl = car.WheelDirection;
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
            RotateWheels();
            WheelDirections();
        }
    }

    private void FixedUpdate()
    {
        if (!isOnVehicle) return;
        
        Vector3 move = Joystick.Instance.GetMoveDirection() * speed;
        move.y = 0;
        rigidbody.velocity = move;
        if (Joystick.Instance.GetMoveDirection() != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Joystick.Instance.GetMoveDirection()), drivingControl * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InitializePlayer(other);
            _playerTransform = _player.transform;
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
        _player.ActualVehicle = null;
        isOnVehicle = false;
        DetachPlayerFromVehicle();
        CamController.Instance.ChangeLookAtCam(_playerTransform);
    }

    private void AlignPlayerWithVehicle()
    {
        _player.transform.rotation = transform.rotation;
        _player.transform.position = transform.position;
        _player.transform.SetParent(transform);
    }

    private void DetachPlayerFromVehicle()
    {
        _playerTransform.position = exitPos.position;
        _playerTransform.SetParent(null);
    }
    
    

    private void RotateWheels()
    {
        foreach (var wheel in frontWheels)
        {
            wheel.transform.Rotate(Vector3.right, (speed * 100) * Time.deltaTime);
        }
        foreach (var wheel in backWheels)
        {
            wheel.transform.Rotate(Vector3.right, speed * 100 * Time.deltaTime);
        }
    }

    private void WheelDirections()
    {
        foreach (var wheels in frontWheels)
        {
            // Obtiene la dirección de movimiento del joystick
            Vector3 moveDirection = Joystick.Instance.GetMoveDirection();

            // Calcula la rotación deseada en función de la dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));

            // Aplica la rotación solo en el eje Y
            wheels.transform.rotation = Quaternion.RotateTowards(wheels.transform.rotation, targetRotation, wheelsDirectionControl * Time.deltaTime);
        }
    }
}
