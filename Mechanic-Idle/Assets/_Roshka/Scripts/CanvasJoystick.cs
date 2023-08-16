using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasJoystick : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 0.5f;
    }
    
    
    
}
