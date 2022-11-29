using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using EventSystem = UnityEngine.EventSystems.EventSystem;

public class BuildingGhost : MonoBehaviour
{
    public bool IsDraging = false;
    public GameObject BuildingCanvas;
    public GameObject MainCanvas;
    public BoxCollider BoxCollider;

    private Transform _transform;
    private Transform visual;
    private Vector3 startPosition;
    [SerializeField] private Button confirmButton;

    public static BuildingGhost Current;

    private void Awake()
    {
        Current = this;
    }

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

        IsDraging = false;

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

        IsDraging = true;

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
            IsDraging = false;
        }
    }

    private void OnMouseOver()
    {
        if(MouseController.Current.isPointerOverUI()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            IsDraging = true;
        }
    }

    private void ChangeFollowByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.F))
            IsDraging = !IsDraging;
    }

    private void VisualFollow()
    {
        if (!visual) return;

        if (MouseController.Current.isPointerOverUI())
            return;

        if (IsDraging)
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
