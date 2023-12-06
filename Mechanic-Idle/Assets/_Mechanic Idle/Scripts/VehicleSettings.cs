using UnityEngine;

public enum VehicleType
{
    Car,
    Truck,
    Motorbike
}
[CreateAssetMenu(menuName = "Car", fileName = "CarName")]
public class VehicleSettings : ScriptableObject
{
    [SerializeField] private VehicleType vehicleType;
    [SerializeField] private float speed;
    [SerializeField] private float drivingControl;
    [SerializeField] private float wheelDirection;
    
    
    
    public VehicleType VehicleType => vehicleType;
    public float DrivingControl => drivingControl;
    public float Speed => speed;
    public float WheelDirection => wheelDirection;
}
