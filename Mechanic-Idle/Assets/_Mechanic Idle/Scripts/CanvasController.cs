using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Button getInCarButton, getOutCarButton, repairButton;

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
    
    private void CanEnterInAVehicle(bool can, GameObject vehicle)
    {
        getInCarButton.gameObject.SetActive(can);
    }

    public void ButtonGetInTheCarAction()
    {
        //EventsManager.Instance.OnEnterInAVehicle();
        getInCarButton.gameObject.SetActive(false);
        getOutCarButton.gameObject.SetActive(true);
    }
    
    public void ButtonGetOutOffTheCarAction()
    {
        //EventsManager.Instance.OnExitFromVehicle();
        getOutCarButton.gameObject.SetActive(false);
    }
    
    public void OnCanModifyVehicle(bool can)
    {
        repairButton.gameObject.SetActive(can);
    }
}
