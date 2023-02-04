using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCoordinator : Singleton<ResourceCoordinator> {

    public int currentDroplets;
    public int currentLeaves;

    public float maxSunshine = 3f;
    public float maxWater = 3f;

    public float currentSunshine;
    public float currentWater;

    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ConsumeSunshine(), ConsumeSunshine);
        EventCoordinator.StartListening(EventName.Economy.ConsumeWater(), ConsumeWater);
    }

    void Update() {
        AccumualateGlobalRecources();
    }
    void AccumualateGlobalRecources() {
        if (DayNightCycleCoordinator.GetCycle() == Cycle.day) {
            currentSunshine += Time.deltaTime;
            currentSunshine = Mathf.Clamp(currentSunshine, 0, maxSunshine);
        } else {
            currentWater += Time.deltaTime;
            currentWater = Mathf.Clamp(currentWater, 0, maxWater);
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