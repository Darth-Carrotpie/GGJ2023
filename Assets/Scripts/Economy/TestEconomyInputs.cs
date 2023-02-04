using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEconomyInputs : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            EventCoordinator.TriggerEvent(EventName.Economy.ConsumeSunshine(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            EventCoordinator.TriggerEvent(EventName.Economy.ConsumeWater(), GameMessage.Write());
        }
    }
}