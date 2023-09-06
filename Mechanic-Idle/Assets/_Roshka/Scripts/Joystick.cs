using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Joystick Instance;
    
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform pad;
    [SerializeField] private CanvasGroup canvasJoystick;
    
    private Vector3 _move;
    private Vector3 _clickPos;
    private bool _isMoving = false;
    private int _tapId = -1;
    [SerializeField] private Camera _mainCamera;
    
    private void Awake() => Instance = this;

  

    public void Show(bool isVisible = true)
    {
        if (isVisible) canvasJoystick.alpha = 1;
        else canvasJoystick.alpha = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_isMoving) return;

        Vector3 posTmp = eventData.position;
        posTmp.z = 0f;

        Vector3 newPos = _clickPos - posTmp;

        pad.transform.localPosition = newPos * -1;
        pad.transform.localPosition =Vector2.ClampMagnitude(pad.transform.localPosition, joystick.rect.width * 0.55f);
        _move = new Vector3(pad.transform.localPosition.x, 0, pad.transform.localPosition.y).normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_tapId != eventData.pointerId) return;
        _tapId = -1;
        Show(false);
        pad.transform.localPosition = Vector3.zero;
        _move = Vector3.zero;
        _isMoving = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tapId != -1) return;

        Show(true);
        _tapId = eventData.pointerId;

        Vector3 clickPos = eventData.position;

        joystick.transform.localPosition = clickPos;
        Vector3 tmpLocalPos = joystick.transform.localPosition;
        tmpLocalPos.z = 0;
        joystick.transform.localPosition = tmpLocalPos;

        _clickPos = joystick.transform.localPosition;
        _isMoving = true;
    }

    public Vector3 GetMoveDirection() => _move;
    public bool IsMoving() => _isMoving;
    
    //  *** HOW TO USE WITH CHARACTER CONTROLLER
    //  controller.Move(Joystick.Instance.GetMoveDirection() * speed * Time.deltaTime);
    //  *** HOW TO USE WITH RIGIDBODY
    //  Vector3 move = Joystick.Instance.GetMoveDirection() * speed;
    //  move.y = 0;
    //  rigidBody.velocity = move;
    //  *** HOW TO ROTATE
    //  if(Joystick.Instance.GetMoveDirection() != Vector3.zero)
    //      rotation = Quaternion.LookRotation(Joystick.Instance.GetMoveDirection());
}
