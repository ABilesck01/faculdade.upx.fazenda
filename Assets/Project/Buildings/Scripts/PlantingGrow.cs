using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantingGrow : MonoBehaviour
{
    public Slider timerSlider;
    public GameObject btnHarvest;
    [HideInInspector]public bool PlantIsGrowing;
    [HideInInspector]public bool PlantIsReady;

    [SerializeField] private Transform spawn;
    [SerializeField] private PlantingTerrainController terrainController;
    [SerializeField] private ModalController timerModal;

    public DateTime startDate;


    private double currentGrowTime = 0;
    private int maxGrowTime = 0;
    private SeedSO currentSeed;
    private GameObject[] states;

    private int currentState = 0;

    public class OnPlantSeedEventArgs : EventArgs
    {
        public SeedSO seed;
        public DateTime startDate;

        public OnPlantSeedEventArgs(SeedSO seed, DateTime startDate)
        {
            this.seed = seed;
            this.startDate = startDate;
        }
    }

    public event EventHandler<OnPlantSeedEventArgs> OnPlantSeed;

    public void PlantSeed(SeedSO seed)
    {
        currentSeed = seed;
        maxGrowTime = seed.TimeToFullGrow;
        currentGrowTime = 0;
        PlantIsGrowing = true;
        PlantIsReady = false;
        currentState = 0;
        terrainController.CanPlant = false;
        startDate = DateTime.Now;

        states = new GameObject[3]
        {
            Instantiate(seed.stepOne, spawn.position, spawn.rotation, spawn),
            Instantiate(seed.stepTwo, spawn.position, spawn.rotation, spawn),
            Instantiate(seed.stepTree, spawn.position, spawn.rotation, spawn)
        };

        timerSlider.maxValue = maxGrowTime;
        timerSlider.value = (float)currentGrowTime;
        btnHarvest.SetActive(false);
        btnHarvest.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                btnHarvest_click();
            });

        OnPlantSeed?.Invoke(this, new OnPlantSeedEventArgs(currentSeed, startDate));

        ActvateState();
    }

    public void PlantSeed(SeedSO seed, DateTime startTime)
    {
        currentSeed = seed;
        maxGrowTime = seed.TimeToFullGrow;
        currentGrowTime = 0;
        PlantIsGrowing = true;
        PlantIsReady = false;
        currentState = 0;
        terrainController.CanPlant = false;
        startDate = startTime;

        states = new GameObject[3]
        {
            Instantiate(seed.stepOne, spawn.position, spawn.rotation, spawn),
            Instantiate(seed.stepTwo, spawn.position, spawn.rotation, spawn),
            Instantiate(seed.stepTree, spawn.position, spawn.rotation, spawn)
        };

        timerSlider.maxValue = maxGrowTime;
        timerSlider.value = (float)currentGrowTime;
        btnHarvest.SetActive(false);
        btnHarvest.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                btnHarvest_click();
            });

        ActvateState();
    }

    private void btnHarvest_click()
    {
        SeedSO seed;
        terrainController.GetPlant();
        ClearStates();
        PlantIsGrowing = false;
        PlantIsReady = false;
        timerModal.CloseModal();
        seed = null;
        startDate = DateTime.MinValue;
        currentGrowTime = 0;
        //TODO add seed
    }

    private void Update()
    {

        if (PlantIsGrowing)
        {
            currentGrowTime = (DateTime.Now - startDate).TotalSeconds;
            timerSlider.value = (float)currentGrowTime;


            if (currentGrowTime >= (maxGrowTime / 2) && currentState == 0)
            {
                currentState = 1;
                ActvateState();
            }

            if(currentGrowTime >= maxGrowTime && currentState == 1)
            {
                currentState = 2;
                ActvateState();
                PlantIsReady = true;
                PlantIsGrowing = false;
                btnHarvest.SetActive(true);
            }
        }
    }

    private void ActvateState()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i == currentState)
                states[i].SetActive(true);
            else
                states[i].SetActive(false);
        }
    }

    private void ClearStates()
    {
        foreach (Transform item in spawn)
        {
            Destroy(item.gameObject);
        }

        states = new GameObject[0];
    }
}
