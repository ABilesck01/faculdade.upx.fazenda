using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    public static MouseController Current;
    public LayerMask UILayer;

    private void Awake()
    {
        Current = this;
    }
    
    public bool isPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> uiResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, uiResults);
        
        for (int i = 0; i < uiResults.Count; i++)
        {
            if (uiResults[i].gameObject.layer == UILayer)
            {
                return true;
            }
        }

        return false;
    }
}
