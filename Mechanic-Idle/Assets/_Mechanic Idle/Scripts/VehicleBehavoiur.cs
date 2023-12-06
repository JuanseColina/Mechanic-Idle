using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class VehicleBehavoiur : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] public VehicleSettings vehicleSettings; 
    private float speed;
    private float drivingControl;
    private float wheelsDirectionControl;

    [Header("References")] 
    [SerializeField] private Transform exitPos;
    [SerializeField] private GameObject[] frontWheels;
    [SerializeField] private GameObject[] backWheels;

    private Player _player;
    private bool isPlayerOnVehicle;

    private void Awake()
    {
        speed = vehicleSettings.Speed;
        drivingControl = vehicleSettings.DrivingControl;
        wheelsDirectionControl = vehicleSettings.WheelDirection;
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
        if (!isPlayerOnVehicle) return;

        if (Joystick.Instance.GetMoveDirection() != Vector3.zero)
        {
            RotateWheels();
            // WheelsDirection();
        }
    }

    private void FixedUpdate()
    {
        if (!isPlayerOnVehicle) return;
        
        if (Joystick.Instance.IsMoving())
        {
            Vector3 direccion = new Vector3(Joystick.Instance.GetMoveDirection().x, 0, Joystick.Instance.GetMoveDirection().z);
            _rigidbody.velocity = direccion * speed;

            // Calcular la rotación basada en la dirección horizontal del joystick
            float anguloGiro = Mathf.Atan2(Joystick.Instance.GetMoveDirection().x, Joystick.Instance.GetMoveDirection().z) * Mathf.Rad2Deg;
            Quaternion rotacion = Quaternion.Euler(0, anguloGiro, 0);

            // Aplicar la rotación al auto
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, drivingControl * Time.deltaTime);
            
            
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
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
        EventsManager.Instance.OnCanEnterInAVehicle(true, this);
    }

    private void ResetPlayer()
    {
        _player = null;
        EventsManager.Instance.OnCanEnterInAVehicle(false, null);
    }
    
    private void OnEnterInAVehicle(VehicleBehavoiur vehicleBehaviour)
    {
        isPlayerOnVehicle = true;
        AlignPlayerWithVehicle();
        CamController.Instance.CamFollowAtVehicle(transform);
    }

    private void OnExitFromVehicle()
    {
        isPlayerOnVehicle = false;
        CamController.Instance.CamFollowAtPlayer(_player.transform);
    }

    private void AlignPlayerWithVehicle()
    {
        _player.transform.rotation = transform.rotation;
        _player.transform.position = transform.position;
        _player.transform.SetParent(transform);
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

    /// <summary>
    /// El vehiculo tiene su propio movimiento separado totalmente del player
    /// </summary>
    private void WheelsDirection()
    {
        foreach (var wheels in frontWheels)
        {
            Vector3 moveDirection = Joystick.Instance.GetMoveDirection();
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            Quaternion limitedRotation = Quaternion.Euler(0f, Mathf.Clamp(targetRotation.eulerAngles.y, -35f, 35f), 0f);
    
            float rotationAngle = Quaternion.Angle(wheels.transform.rotation, limitedRotation);
    
            if (rotationAngle > 0.1f)  
            {
                wheels.transform.rotation = Quaternion.RotateTowards(wheels.transform.rotation, limitedRotation, wheelsDirectionControl * Time.deltaTime);
            }
            else
            {
                wheels.transform.rotation = limitedRotation;
            }
        }

    }
    
    public Vector3 GetCarExitPosition()
    {
        var pos = exitPos.position;
        pos.y = 0;
        return pos;
    }
}
