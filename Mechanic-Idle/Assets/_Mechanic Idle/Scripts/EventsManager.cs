using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public event Action<bool, GameObject> ActionCanEnterInAVehicle;
    public event Action<GameObject> ActionEnterInAVehicle;
    public event Action ActionExitFromVehicle;
    public event Action<bool> ActionCanModifyVehicle; 
    
    public void OnCanEnterInAVehicle(bool can, GameObject vehicle) => ActionCanEnterInAVehicle?.Invoke(can, vehicle);
    public void OnEnterInAVehicle(GameObject vehicle) => ActionEnterInAVehicle?.Invoke(vehicle);
    public void OnExitFromVehicle() => ActionExitFromVehicle?.Invoke();
    public void OnCanModifyVehicle(bool can) => ActionCanModifyVehicle?.Invoke(can);

}
