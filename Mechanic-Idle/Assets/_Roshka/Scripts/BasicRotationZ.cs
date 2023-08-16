using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotationZ : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f; // Speed of rotation in degrees per second
    
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
