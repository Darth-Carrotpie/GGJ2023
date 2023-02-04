using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFxTestInputs : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeTrunk(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            EventCoordinator.TriggerEvent(EventName.Hostiles.DamageTrunk(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            EventCoordinator.TriggerEvent(EventName.UI.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(2));
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            EventCoordinator.TriggerEvent(EventName.UI.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(-2));
        }
    }
}