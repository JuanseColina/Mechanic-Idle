using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScaleAnim : MonoBehaviour
{
    [SerializeField] private float factor = 0.05f;

    [SerializeField] private float speed = 0.3f;
    void Start()
    {
        LeanTween.scale(gameObject, transform.localScale + new Vector3(factor, factor, factor), speed)
            .setLoopPingPong(-1);
    }
}
