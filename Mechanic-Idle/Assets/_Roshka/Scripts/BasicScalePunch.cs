using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScalePunch : MonoBehaviour
{
    [SerializeField] private float iniDelay = 0;
    [SerializeField] private float endDelay = 0;
    void Start()
    {
        Invoke("Animate", iniDelay);
    }

    private void Animate()
    {
        Vector3 scaleStart = transform.localScale * 0.8f;
        Vector3 scaleEnd = transform.localScale;
        LeanTween.scale(gameObject, scaleStart, 0.4f)
            .setEase(LeanTweenType.easeInBack)
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, scaleEnd, 0.8f)
                    .setEase(LeanTweenType.easeOutElastic)
                    .setOnComplete(() =>
                    {
                        Invoke("Animate", endDelay);
                    });
                
            });
    }
    
}
