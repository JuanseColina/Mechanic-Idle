using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotation : MonoBehaviour
{
    private void Awake()
    {
        transform.localEulerAngles =
            new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
    }

    
}
