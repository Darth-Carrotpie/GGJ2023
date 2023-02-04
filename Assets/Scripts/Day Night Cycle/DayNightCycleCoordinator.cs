using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cycle {
    day,
    night,
}

public class DayNightCycleCoordinator : Singleton<DayNightCycleCoordinator> {

    Cycle currentCycle;
    public float timeForCycle = 15f;
    float currentTime;

    public static Cycle GetCycle() {
        return Instance.currentCycle;
    }
    void Update() {
        currentTime += Time.deltaTime;
        if (currentTime > timeForCycle) {
            SwitchCycle();
            currentTime = 0;
        }
    }
    private void Start() {
        EventCoordinator.TriggerEvent(EventName.Environment.NextCycle(), GameMessage.Write().WithCycle(Cycle.day));
    }
    void SwitchCycle() {
        Cycle nextCycle;
        if (currentCycle == Cycle.day)
            nextCycle = Cycle.night;
        else {
            nextCycle = Cycle.day;
        }
        EventCoordinator.TriggerEvent(EventName.Environment.NextCycle(), GameMessage.Write().WithCycle(nextCycle));
        currentCycle = nextCycle;
    }

}