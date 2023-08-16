using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInstruction : MonoBehaviour
{
    [SerializeField] private Transform[] positions;

    [SerializeField] private GameObject hand;

    //private LTSpline visualizePath;
    void Start()
    {
        Vector3[] list = new Vector3[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            list[i] = positions[i].position;
        }

        //visualizePath = new LTSpline(list);
        LeanTween.moveSpline(hand, list, 2f).setLoopCount(-1);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //if(visualizePath!=null) visualizePath.gizmoDraw();
    }
}
