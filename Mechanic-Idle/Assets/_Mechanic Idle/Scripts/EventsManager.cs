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
    public event Action<bool> ActionCanEnterInAVehicle;
    public event Action ActionEnterInAVehicle;
    public event Action ActionExitFromVehicle;
    public event Action<bool> ActionCanModifyVehicle;
    public event Action ActionModifyVehicle;
    public event Action<bool> ActionPlayerCanMove;
    public void OnCanEnterInAVehicle(bool can) => ActionCanEnterInAVehicle?.Invoke(can);
    public void OnEnterInAVehicle() => ActionEnterInAVehicle?.Invoke();
    public void OnExitFromVehicle() => ActionExitFromVehicle?.Invoke();
    public void OnCanModifyVehicle(bool can) => ActionCanModifyVehicle?.Invoke(can);
    public void OnModifyVehicle() => ActionModifyVehicle?.Invoke();
    public void OnPlayerCanMove(bool can) => ActionPlayerCanMove?.Invoke(can);
}
