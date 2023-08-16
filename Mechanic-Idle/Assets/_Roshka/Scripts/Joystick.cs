using System;
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
    private Camera _mainCamera;

    private float delayStart = 0.5f;
    private void Awake() => Instance = this;
    private void Start()
    { 
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        
        if (PlayerPrefs.GetInt("dynamicJoystick", 0) == 1)
        {
            isDynamic = true;
        }
        else
        {
            isDynamic = false;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private Vector3 targetPos;
    private Vector3 velocity;

    private bool isDynamic = false;

    public void ChangeJoystickType()
    {
        if (PlayerPrefs.GetInt("dynamicJoystick", 0) == 1)
        {
            isDynamic = true;
        }
        else
        {
            isDynamic = false;
        }
    }
    
    public void Show(bool isVisible = true)
    {
        if (isVisible) canvasJoystick.alpha = 1;
        else canvasJoystick.alpha = 0;
    }

    private void Update()
    {
        delayStart -= Time.deltaTime;
        // if(GameController.Instance.GetIsPauseTime())
        // {
        //     StopMoving();
        //     return;
        // }
        if (isDynamic)
        {
            float distance = Vector3.Distance(targetPos, joystick.transform.localPosition);
            if (distance > 100)
            {
                //joystick.transform.position = Vector3.Lerp(joystick.transform.position, targetPos, Time.deltaTime);
                joystick.transform.localPosition = Vector3.SmoothDamp(joystick.transform.localPosition, targetPos, ref velocity, 0.35f);
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_isMoving) return;
        //if(GameController.Instance.GetIsPauseTime()) return;
        
        if (isDynamic)
        {
            targetPos = eventData.position;
            targetPos.z = 0;
        }
        
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
        //if(!GameController.Instance.IsLevelStarted() && delayStart < 0) EventsManager.Instance.OnStartLevel();
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

    public void StopMoving()
    {
        _tapId = -1;
        Show(false);
        pad.transform.localPosition = Vector3.zero;
        _move = Vector3.zero;
        _isMoving = false;
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
