using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Button getInCarButton;
    [SerializeField] private Button getOutCarButton;
    [SerializeField] private Button enterCarWashButton;
    [SerializeField] private Button changeCamInWashMode;
    [SerializeField] private Button exitCarsWashButton;


    private VehicleBehavoiur myVehicle;

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
    
    private void CanEnterInAVehicle(bool can, VehicleBehavoiur vehicle)
    {
        getInCarButton.gameObject.SetActive(can);
        myVehicle = vehicle;

    }

    public void ButtonGetInTheCarAction()
    {
        EventsManager.Instance.OnEnterInAVehicle(myVehicle);
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
        enterCarWashButton.gameObject.SetActive(can);
        getOutCarButton.gameObject.SetActive(!can);
    }

    public void ButtonWashCarAction()
    {
        EventsManager.Instance.OnModifyVehicle();
        CamController.Instance.WashingMachineCams();
        EventsManager.Instance.OnPlayerCanMove(false);
        changeCamInWashMode.gameObject.SetActive(true);
        exitCarsWashButton.gameObject.SetActive(true);
        enterCarWashButton.gameObject.SetActive(false);
    }

    public void ButtonExitCarWash()
    {
        exitCarsWashButton.gameObject.SetActive(false);
        changeCamInWashMode.gameObject.SetActive(false);
        EventsManager.Instance.OnPlayerCanMove(true);
       
        CamController.Instance.SetMainCam();
    }

    public void ChangeBetweenCams() => CamController.Instance.ChangeBetweenWashingCameras();
}
