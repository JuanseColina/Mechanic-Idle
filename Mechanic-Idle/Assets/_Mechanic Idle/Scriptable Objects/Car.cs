using UnityEngine;

[CreateAssetMenu(menuName = "Car", fileName = "CarName")]
public class Car : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float drivingControl;

    public float DrivingControl => drivingControl;
    public float Speed => speed;
}
