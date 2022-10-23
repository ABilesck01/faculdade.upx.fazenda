using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EventSystem = UnityEngine.EventSystems.EventSystem;

public class BuildingGhost : MonoBehaviour
{
    public bool Follow;
    public GameObject BuildingCanvas;
    public GameObject MainCanvas;
    public BoxCollider BoxCollider;

    private Transform _transform;
    private Transform visual;
    private Vector3 startPosition;
    [SerializeField] private Button confirmButton;

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
        BuildingCanvas.SetActive(false);
        MainCanvas.SetActive(true);

        Follow = false;

        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }
        _transform.position = startPosition;
    }

    private void Instance_onSelectedChange(object sender, EventArgs e)
    {
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        BuildingCanvas.SetActive(true);
        MainCanvas.SetActive(false);

        Follow = true;

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
            Follow = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Follow = true;
        }
    }

    private void ChangeFollowByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Follow = !Follow;
    }

    private void VisualFollow()
    {
        if (!visual) return;

        if (BuildingSystem.instance.isPointerOverUI())
            return;

        if (Follow)
        {
            Vector3 targetPosition = BuildingSystem.instance.GetMouseGridPosition();
            targetPosition.y = 2f;
            _transform.position = Vector3.Lerp(_transform.position, targetPosition, Time.deltaTime * 15f);
        }

        Quaternion targetDirection = 
            BuildingSystem.instance.GetPlacedObjectDirection(out Vector3 pos);
        _transform.position = Vector3.Lerp(_transform.position, pos, Time.deltaTime * 15f);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, targetDirection, Time.deltaTime * 15f);
    }

}
