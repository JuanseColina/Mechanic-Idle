using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private Vector3 lossyScale;
    [SerializeField] private Vector3 localScale;
    // Update is called once per frame
    void Update()
    {
        lossyScale = transform.lossyScale;
        localScale = transform.localScale;
        
        float val = 1 - lossyScale.x;
        if (val + 1.5f < 1) val = 1f; else val = val + 1.5f;
        localScale.x = val;
        localScale.y = val;
        localScale.z = val;
        
        //transform.localScale = localScale;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 10f);
    }
}
