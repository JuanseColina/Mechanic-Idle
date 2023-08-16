using System;
using System.Collections;
using System.Collections.Generic;
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
    public void OnCanEnterInAVehicle(bool can) => ActionCanEnterInAVehicle?.Invoke(can);
    public void OnEnterInAVehicle() => ActionEnterInAVehicle?.Invoke();
    public void OnExitFromVehicle() => ActionExitFromVehicle?.Invoke();

}
