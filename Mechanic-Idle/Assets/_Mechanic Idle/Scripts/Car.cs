using UnityEngine;

[CreateAssetMenu(menuName = "Car", fileName = "CarName")]
public class Car : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float drivingControl;
    [SerializeField] private float wheelDirection;

    public float DrivingControl => drivingControl;
    public float Speed => speed;
    public float WheelDirection => wheelDirection;
}
