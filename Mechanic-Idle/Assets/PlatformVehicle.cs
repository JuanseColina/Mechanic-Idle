using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVehicle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnCanModifyVehicle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnCanModifyVehicle(false);
        }
    }
}
