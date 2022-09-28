using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class PlantingAreaController : MonoBehaviour
{
    [SerializeField] private ModalController noPreparedModalController;
    [SerializeField] private ModalController preparedModalController;
    [SerializeField] private ModalController timerModal;
    [Space]
    [SerializeField] private PlantingTerrainController terrainController;
    [SerializeField] private PlantingGrow plantingGrow;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(terrainController.CanPlant)
                preparedModalController.OpenModal();
            else if(!plantingGrow.PlantIsGrowing && !plantingGrow.PlantIsReady)
                noPreparedModalController.OpenModal();
            else
                timerModal.OpenModal();
        }
    }
}
