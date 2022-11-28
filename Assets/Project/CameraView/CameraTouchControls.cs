using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControls : MonoBehaviour
{
    [SerializeField]private Vector3 calculatedPos;
    [SerializeField]private Vector3 transformPos;
    
    [SerializeField] private Transform _transform;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        //_transform = transform;
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
        _transform.localPosition -= transformPos;

    }

}
