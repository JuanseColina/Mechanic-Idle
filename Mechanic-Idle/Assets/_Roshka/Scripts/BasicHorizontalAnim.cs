using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHorizontalAnim : MonoBehaviour
{
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private float targetX;

    [SerializeField] private bool isRectTransform = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isRectTransform)
        {
            LeanTween.moveX(gameObject.GetComponent<RectTransform>(), targetX, speed).setLoopPingPong(-1);
        }
        else
        {
            LeanTween.moveLocalX(gameObject, targetX, speed).setLoopPingPong(-1);
        }
        
    }
}
