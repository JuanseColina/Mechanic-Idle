using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotationY : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f; // Speed of rotation in degrees per second
    
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
