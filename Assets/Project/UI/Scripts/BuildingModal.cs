using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModal : MonoBehaviour
{
    public void btnConfirm_Click()
    {
        BuildingSystem.instance.PlaceObject();
    }

    public void btnRotate_Click()
    {
        BuildingSystem.instance.ManageRotation();
    }

    public void btnCancel_Click()
    {
        BuildingSystem.instance.CancelBuilding();
    }
}
