using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject getInCarButton, getOutCarButton;

    private void Start()
    {
        EventsManager.Instance.ActionCanEnterInAVehicle += CanEnterInAVehicle;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionCanEnterInAVehicle -= CanEnterInAVehicle;
    }
    
    private void CanEnterInAVehicle(bool can)
    {
        if (can)
        {
            getInCarButton.SetActive(true);
        }
        else
        {
            getInCarButton.SetActive(false);
        }
    }

    public void GetInTheCar()
    {
        EventsManager.Instance.OnEnterInAVehicle();
        getInCarButton.SetActive(false);
        getOutCarButton.SetActive(true);
    }
    
    public void GetOutOffTheCar()
    {
        EventsManager.Instance.OnExitFromVehicle();
        getOutCarButton.SetActive(false);
    }
}
