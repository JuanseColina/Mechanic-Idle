using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        EventsManager.Instance.ActionPlayerCanMove += OnPlayerCanMove;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionPlayerCanMove -= OnPlayerCanMove;
    }

    void OnPlayerCanMove(bool can) => FindObjectOfType<Joystick>().enabled = can;
}
