using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Animator animator;
    
    [SerializeField] private float speed = 5f;
    private static readonly int State = Animator.StringToHash("state");
    private bool canMove = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        if (!canMove) return;
        controller.Move(Joystick.Instance.GetMoveDirection() * (speed * Time.deltaTime));
          if(Joystick.Instance.GetMoveDirection() != Vector3.zero)
              transform.rotation = Quaternion.LookRotation(Joystick.Instance.GetMoveDirection());
    }
    
    private void LateUpdate()
    {
        if (!canMove) return;
        animator.SetFloat(State, Joystick.Instance.GetMoveDirection().magnitude);
    }
    
    private void OnEnterInAVehicle()
    {
        animator.gameObject.SetActive(false);
    }
    
    private void OnExitFromVehicle()
    {
        animator.gameObject.SetActive(true);
    }
}
