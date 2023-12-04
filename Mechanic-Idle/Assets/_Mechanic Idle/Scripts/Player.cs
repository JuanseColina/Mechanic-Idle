using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Collider _collider;
    [SerializeField] Animator animator;
    [SerializeField] VehicleBehavoiur actualVehicle;
    [SerializeField] private float speed = 5f;
    private float actualSpeed;
    private static readonly int State = Animator.StringToHash("state");
    private bool canMove = true;


    private void Awake()
    {
        actualSpeed = speed;
    }

    private void Start()
    {
        EventsManager.Instance.ActionEnterInAVehicle += OnEnterInAVehicle; 
        EventsManager.Instance.ActionExitFromVehicle += OnExitFromVehicle;
        EventsManager.Instance.ActionPlayerCanMove += OnPlayerCanMove;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionEnterInAVehicle -= OnEnterInAVehicle;
        EventsManager.Instance.ActionExitFromVehicle -= OnExitFromVehicle;
        EventsManager.Instance.ActionPlayerCanMove -= OnPlayerCanMove;
    }

    private void Update()
    {
        if (!canMove) return;
        if (actualVehicle) return;
        controller.Move(Joystick.Instance.GetMoveDirection() * (speed * Time.deltaTime));
        if (Joystick.Instance.GetMoveDirection() != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(Joystick.Instance.GetMoveDirection());
    }
    
    private void LateUpdate()
    {
        if (!canMove) return;
        if (actualVehicle) return;
        animator.SetFloat(State, Joystick.Instance.GetMoveDirection().magnitude);
    }
    
    private void OnEnterInAVehicle(VehicleBehavoiur vehicleBehaviour)
    {
        animator.gameObject.SetActive(false);
        actualVehicle = vehicleBehaviour;
        controller.enabled = false;
        _collider.enabled = false;
    }
    
    private void OnExitFromVehicle()
    {
        transform.SetParent(null);
        transform.position = actualVehicle.GetCarExitPosition();
        animator.gameObject.SetActive(true);
        actualVehicle = null;
        controller.enabled = true;
        _collider.enabled = true;
    }
    
    private void ResetSpeed()
    {
        speed = actualSpeed;
    }
    public void ChangeSpeed(float value)
    {
        speed = value;
    }

    private void OnPlayerCanMove(bool can)
    {
        canMove = can;
    }

    public Vector3 GetPlayerPosition()
    {
        var pos = transform.position;
        return pos;
    }
}
