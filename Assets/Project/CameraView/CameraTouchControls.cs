using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControls : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private int Yclamp;
    [SerializeField] private int Xclamp;
    
    private Vector3 calculatedPos;
    private Vector3 transformPos;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Drag();
    }

    private void Drag()
    {
        if (Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);
        
        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            currentPos = touch.position;
        }

        calculatedPos = (currentPos - startPos).normalized;
        transformPos = new Vector3(calculatedPos.x, 0, calculatedPos.y);
        
        Vector3 localPosition = _transform.localPosition - transformPos;
        localPosition.x = Mathf.Clamp(localPosition.x, -Xclamp, Xclamp);
        localPosition.z = Mathf.Clamp(localPosition.z, -Yclamp, Yclamp);

        _transform.localPosition = localPosition;
    }

}
