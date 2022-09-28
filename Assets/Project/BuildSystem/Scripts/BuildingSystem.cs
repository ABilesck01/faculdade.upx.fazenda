using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO building;
    [SerializeField] private BuildingTypeSO.Dir dir = BuildingTypeSO.Dir.Down;

    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float gridSpacing = 10f;

    [Space]
    [SerializeField] private LayerMask mouseLayerMask;

    private Vector3 lastPosition;

    public static BuildingSystem instance;

    public event EventHandler onSelectedChange;
    public event EventHandler onPlaceObject;

    private Grid<GridObject> grid;
    private void Awake()
    {
        instance = this;

        grid = new Grid<GridObject>(gridWidth, gridHeight, gridSpacing, Vector3.zero,
            (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z), true);
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    PlaceObject();
        //}

        DemolishObject();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ManageRotation();
        }
    }

    public float GetCellsize()
    {
        return grid.GetCellSize();
    }

    public void PlaceObject()
    {
        if (building == null) return;

        if(!PlayerMoney.instance.SpendCoins(building.price))
        {
            building = null;
            Debug.Log("Not nough money!");
            MessageBox.Instance.ShowMessage("Dinheiro insuficiente!");
            onPlaceObject?.Invoke(this, null);
            return;
        }

        grid.GetXZ(lastPosition, out int x, out int z);

        List<Vector2Int> gridPositionList = building.GetGridPositionList(new Vector2Int(x, z), dir);

        bool canBuild = true;
        foreach (Vector2Int item in gridPositionList)
        {
            if (!grid.GetValue(item.x, item.y).CanBuild())
            {
                canBuild = false;
                break;
            }
        }

        if (canBuild)
        {
            Vector2Int rotationOffset = building.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) +
                new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

            PlacedObject placedObject = PlacedObject.Create
                (placedObjectWorldPosition, new Vector2Int(x, z), dir, building);

            foreach (Vector2Int item in gridPositionList)
            {
                grid.GetValue(item.x, item.y).SetPlacedObject(placedObject);
            }

            building = null;
            dir = BuildingTypeSO.Dir.Down;
            onPlaceObject?.Invoke(this, null);
        }
        else
        {
            Debug.Log("Cannot build here!");
        }
    }

    public void CancelBuilding()
    {
        building = null;
        onPlaceObject?.Invoke(this, null);
        return;
    }

    private void DemolishObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = grid.GetValue(GetMousePosition());
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();
                List<Vector2Int> gridPositionList = placedObject.getGridPositionList();

                foreach (Vector2Int item in gridPositionList)
                {
                    grid.GetValue(item.x, item.y).ClearPlacedObject();
                }
            }
        }
    }

    public void ManageRotation()
    {
        dir = BuildingTypeSO.GetNextDir(dir);
    }

    public void SetBuilding(BuildingTypeSO building)
    {
        this.building = building;
        onSelectedChange?.Invoke(this, null);
    }

    public BuildingTypeSO GetBuilding()
    {
        return building;
    }

    public Quaternion GetPlacedObjectDirection(out Vector3 position)
    {
        if (building != null)
        {
            grid.GetXZ(lastPosition, out int x, out int z);
            Vector2Int rotationOffset = building.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) +
                new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

            position = placedObjectWorldPosition;

            return Quaternion.Euler(0, building.GetRotationAngle(dir), 0);
        }
        else
        {
            position = Vector3.zero;
            return Quaternion.identity;
        }
    }

    public Vector3 GetMouseGridPosition()
    {
        grid.GetXZ(GetMousePosition(), out int x, out int z);
        if (building != null)
        {
            Vector2Int rotationOffset = building.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) +
                new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return GetMousePosition();
        }
    }

    //private bool PointerOverUI()
    //{
    //    _eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    EventSystem.current.RaycastAll(_eventDataCurrentPosition, _rayResults);

    //    List<RectTransform> uiResults = new List<RectTransform>();
    //    foreach (var rayResult in _rayResults)
    //    {
    //        if (rayResult.gameObject.TryGetComponent(out RectTransform rect))
    //        {
    //            uiResults.Add(rect);
    //        }
    //    }

    //    return uiResults.Count > 0;
    //}

    public bool isPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> uiResults = new List<RaycastResult>();
        for (int i = 0; i < uiResults.Count; i++)
        {
            if (uiResults[i].gameObject.TryGetComponent(out RectTransform rectTransform))
            {
                uiResults.RemoveAt(i);
                i--;
            }
        }

        return uiResults.Count > 0;
    }

    private Vector3 GetMousePosition()
    {
        if (isPointerOverUI())
            return lastPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, mouseLayerMask))
        {
            lastPosition = hit.point;
            return hit.point;
        }
        else
        {
            return lastPosition;
        }
    }
}
