using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCoordinator : Singleton<ResourceCoordinator> {

    public int currentDroplets;
    public int currentLeaves;
    public int currentBranches;
    public int currentHearts;

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
        int delta = Mathf.FloorToInt(currentWater);
        currentDroplets += delta;
        currentWater -= delta;
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyDroplets(), GameMessage.Write().WithDropletAmount(currentDroplets).WithDelta(delta));
    }

    void ConsumeSunshine(GameMessage msg) {
        int delta = Mathf.FloorToInt(currentSunshine);
        currentLeaves += delta;
        currentSunshine -= delta;
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(currentLeaves).WithDelta(delta));
    }

    public static int GetDroplets() {
        return Instance.currentDroplets;
    }
    public static int GetLeaves() {
        return Instance.currentLeaves;
    }

    public static bool ModifyBranches(int amount) {
        if (Instance.currentBranches + amount < 0)
            return false;
        Instance.currentBranches += amount;
        Instance.currentBranches = Mathf.Clamp(Instance.currentBranches, 0, 999);
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyLeaves(), GameMessage.Write().WithBranchAmount(Instance.currentBranches).WithDelta(amount));
        return true;
    }
    public static bool ModifyLeaves(int amount) {
        if (Instance.currentLeaves + amount < 0)
            return false;
        Instance.currentLeaves += amount;
        Instance.currentLeaves = Mathf.Clamp(Instance.currentLeaves, 0, 999);
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyLeaves(), GameMessage.Write().WithBranchAmount(Instance.currentLeaves).WithDelta(amount));
        return true;
    }
    public static bool ModifyDroplets(int amount) {
        if (Instance.currentDroplets + amount < 0)
            return false;
        Instance.currentDroplets += amount;
        Instance.currentDroplets = Mathf.Clamp(Instance.currentDroplets, 0, 999);
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyLeaves(), GameMessage.Write().WithBranchAmount(Instance.currentDroplets).WithDelta(amount));
        return true;
    }
    public static bool ModifyHearts(int amount) {
        if (Instance.currentHearts + amount < 0)
            return false;
        Instance.currentHearts += amount;
        Instance.currentHearts = Mathf.Clamp(Instance.currentHearts, 0, 999);
        EventCoordinator.TriggerEvent(EventName.Economy.ModifyLeaves(), GameMessage.Write().WithBranchAmount(Instance.currentHearts).WithDelta(amount));
        return true;
    }
}