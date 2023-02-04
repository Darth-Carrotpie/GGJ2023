using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCoordinator : Singleton<ResourceCoordinator> {

    public int currentDroplets;
    public int currentLeaves;

    public float maxSunshine = 3f;
    public float maxSunshineMax = 10f;
    public float maxWaterCurrent = 3f;
    public float maxWaterMax = 10f;

    public float currentSunshine;
    public float currentWater;
    WaterController wContr;
    RaysController rContr;

    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ConsumeSunshine(), ConsumeSunshine);
        EventCoordinator.StartListening(EventName.Economy.ConsumeWater(), ConsumeWater);
        if (wContr == null)
            wContr = FindObjectOfType<WaterController>();
        if (rContr == null)
            rContr = FindObjectOfType<RaysController>();
    }

    void Update() {
        AccumualateGlobalRecources();
        float waterFill = currentWater / maxWaterMax;
        wContr.SetWaterLevel(waterFill);
        float sunshineFill = currentSunshine / maxSunshineMax;
        rContr.SetSunshineLevel(sunshineFill * 10f);
    }
    void AccumualateGlobalRecources() {
        if (DayNightCycleCoordinator.GetCycle() == Cycle.day) {
            currentSunshine += Time.deltaTime;
            currentSunshine = Mathf.Clamp(currentSunshine, 0, maxSunshine);
        } else {
            currentWater += Time.deltaTime;
            currentWater = Mathf.Clamp(currentWater, 0, maxWaterCurrent);
        }
    }
    void ConsumeWater(GameMessage msg) {
        currentDroplets += Mathf.FloorToInt(currentWater);
        currentWater -= Mathf.FloorToInt(currentWater);
    }

    void ConsumeSunshine(GameMessage msg) {
        currentLeaves += Mathf.FloorToInt(currentSunshine);
        currentSunshine -= Mathf.FloorToInt(currentSunshine);
    }

    public static int GetDroplets() {
        return Instance.currentDroplets;
    }
    public static int GetLeaves() {
        return Instance.currentLeaves;
    }
}