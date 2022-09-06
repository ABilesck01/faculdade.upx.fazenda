using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControls : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        RotateOnDrag();
    }

    private void RotateOnDrag()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                _transform.Rotate(0,touch.deltaPosition.x * Time.deltaTime,0);
            }
        }
    }

}
