using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicVerticalAnim : MonoBehaviour
{
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private float targetY;

    [SerializeField] private bool isRectTransform = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isRectTransform)
        {
            LeanTween.moveY(gameObject.GetComponent<RectTransform>(), targetY, speed).setLoopPingPong(-1);
        }
        else
        {
            LeanTween.moveLocalY(gameObject, targetY, speed).setLoopPingPong(-1);
        }
        
    }

}
