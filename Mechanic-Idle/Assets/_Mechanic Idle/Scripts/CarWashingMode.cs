using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarWashingMode : MonoBehaviour
{
    private float time;
    [SerializeField] private Slider slider;
    private void OnMouseDrag()
    {
        time += Time.deltaTime * 8;
        slider.value = time;
    }
}
