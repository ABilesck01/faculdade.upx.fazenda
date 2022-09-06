using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem = UnityEngine.EventSystems.EventSystem;

public class BuildingGhost : MonoBehaviour
{
    public bool follow;
    public GameObject canvas;

    private Transform _transform;
    private Transform visual;
    private Vector3 startPosition;

    private void Start()
    {
        _transform = transform;
        BuildingSystem.instance.onSelectedChange += Instance_onSelectedChange;
        BuildingSystem.instance.onPlaceObject += Instance_onPlaceObject;
        startPosition = _transform.position;
    }

    private void OnDisable()
    {
        BuildingSystem.instance.onSelectedChange -= Instance_onSelectedChange;
        BuildingSystem.instance.onPlaceObject -= Instance_onPlaceObject;
    }

    private void Instance_onPlaceObject(object sender, EventArgs e)
    {
        canvas.SetActive(false);

        follow = false;

        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }
        _transform.position = startPosition;
    }

    private void Instance_onSelectedChange(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        canvas.SetActive(true);

        follow = true;

        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        BuildingTypeSO buildingTypeSO = BuildingSystem.instance.GetBuilding();
        visual = Instantiate(buildingTypeSO.visual, Vector3.zero, Quaternion.identity);
        visual.parent = _transform;
        visual.localPosition = Vector3.zero;
        visual.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        VisualFollow();
        ChangeFollowByKeyboard();
        if(Input.GetMouseButtonUp(0))
        {
            follow = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            follow = true;
        }
    }

    private void ChangeFollowByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.F))
            follow = !follow;
    }

    private void VisualFollow()
    {
        if (BuildingSystem.instance.isPointerOverUI())
            return;

        if (follow)
        {
            Vector3 targetPosition = BuildingSystem.instance.GetMouseGridPosition();
            Quaternion targetDirection = BuildingSystem.instance.GetPlacedObjectDirection();
            targetPosition.y = 2f;
            _transform.position = Vector3.Lerp(_transform.position, targetPosition, Time.deltaTime * 15f);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetDirection, Time.deltaTime * 15f);
        }
    }

}
