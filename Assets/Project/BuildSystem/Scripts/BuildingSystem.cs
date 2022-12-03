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

    private Camera cam;
    
    private void Awake()
    {
        instance = this;

        cam = Camera.main;
        
        grid = new Grid<GridObject>(gridWidth, gridHeight, gridSpacing, Vector3.zero,
            (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z), true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ManageRotation();
        }
    }

    public float GetCellsize()
    {
        return grid.GetCellSize();
    }

    public void PlaceObjectAtPosition(Vector3 position, 
        bool willSpendMoney = true)
    {
        if (building == null) return;

        if (willSpendMoney && !PlayerMoney.instance.SpendCoins(building.price))
        {
            PlayerMoney.instance.SpendCoins(building.price);
            building = null;
            Debug.Log("Not nough money!");
            MessageBox.Instance.ShowMessage("Dinheiro insuficiente!");
            onPlaceObject?.Invoke(this, null);
            return;
        }

        grid.GetXZ(position, out int x, out int z);

        List<Vector2Int> gridPositionList =
            building.GetGridPositionList(new Vector2Int(x, z), dir);

        BuildBuilding(x, z, gridPositionList, CanBuild(gridPositionList));
    }

    public void PlaceObject(bool willSpendMoney = true)
    {
        if (building == null) return;

        if (willSpendMoney && !PlayerMoney.instance.SpendCoins(building.price))
        {
            //PlayerMoney.instance.SpendCoins(building.price);
            building = null;
            Debug.Log("Not nough money!");
            MessageBox.Instance.ShowMessage("Dinheiro insuficiente!");
            onPlaceObject?.Invoke(this, null);
            return;
        }

        PlayerFarmDataController.instance.UpdateJsonData();

        grid.GetXZ(lastPosition, out int x, out int z);

        List<Vector2Int> gridPositionList =
            building.GetGridPositionList(new Vector2Int(x, z), dir);

        BuildBuilding(x, z, gridPositionList, CanBuild(gridPositionList));
    }

    private bool CanBuild(List<Vector2Int> gridPositionList)
    {
        bool canBuild = true;
        foreach (Vector2Int item in gridPositionList)
        {
            if (!grid.GetValue(item.x, item.y).CanBuild())
            {
                canBuild = false;
                break;
            }
        }

        return canBuild;
    }

    private void BuildBuilding(int x, int z, List<Vector2Int> gridPositionList, bool canBuild)
    {
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
            PlayerFarmDataController.instance.UpdateJsonData();
            onPlaceObject?.Invoke(this, null);
        }
        else
        {
            Debug.Log("Cannot build here!");
            MessageBox.Instance.ShowMessage("Não pode construir aqui!");
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

    public void DeleteBuilding(Vector3 position)
    {
        //GridObject gridObject = grid.GetValue(position.x, position.y);
        GridObject gridObject = grid.GetValue(position);
        PlacedObject placedObject = gridObject.GetPlacedObject();
        if (placedObject != null)
        {
            placedObject.DestroySelf();
            List<Vector2Int> gridPositionList = placedObject.getGridPositionList();

            foreach (Vector2Int item in gridPositionList)
            {
                grid.GetValue(item.x, item.y).ClearPlacedObject();
            }

            PlayerFarmDataController.instance.UpdateJsonData();
        }
    }


    public void ManageRotation()
    {
        dir = BuildingTypeSO.GetNextDir(dir);
    }

    public void SetDir(BuildingTypeSO.Dir dir)
    {
        this.dir = dir;
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

    private Vector3 GetMousePosition()
    {
        if (MouseController.Current.isPointerOverUI())
            return lastPosition;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
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
