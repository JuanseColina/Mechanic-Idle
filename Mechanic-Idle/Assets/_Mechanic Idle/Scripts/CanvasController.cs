using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Button getInCarButton, getOutCarButton, washButton;

    private void Start()
    {
        EventsManager.Instance.ActionCanEnterInAVehicle += CanEnterInAVehicle;
        EventsManager.Instance.ActionCanModifyVehicle += OnCanModifyVehicle;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionCanEnterInAVehicle -= CanEnterInAVehicle;
        EventsManager.Instance.ActionCanModifyVehicle -= OnCanModifyVehicle;
    }
    
    private void CanEnterInAVehicle(bool can)
    {
        getInCarButton.gameObject.SetActive(can);
    }

    public void ButtonGetInTheCarAction()
    {
        EventsManager.Instance.OnEnterInAVehicle();
        getInCarButton.gameObject.SetActive(false);
        getOutCarButton.gameObject.SetActive(true);
    }
    
    public void ButtonGetOutOfTheCarAction()
    {
        EventsManager.Instance.OnExitFromVehicle();
        getOutCarButton.gameObject.SetActive(false);
    }

    private void OnCanModifyVehicle(bool can)
    {
        washButton.gameObject.SetActive(can);
        //getOutCarButton.gameObject.SetActive(false);
    }
    
    public void ButtonWashCarAction()
    {
        EventsManager.Instance.OnExitFromVehicle();
        EventsManager.Instance.OnModifyVehicle();
        ButtonChangePositionToCleanCar();
        washButton.gameObject.SetActive(false);
    }

    public void ButtonChangePositionToCleanCar()
    {
        CamController.Instance.WashingMachineCam();
    }
}
