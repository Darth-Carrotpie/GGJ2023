using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFxTestInputs : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeTrunk(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeRoots(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            EventCoordinator.TriggerEvent(EventName.Hostiles.DamageTrunk(), GameMessage.Write().WithDamage(100));
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            EventCoordinator.TriggerEvent(EventName.Hostiles.DamageRoots(), GameMessage.Write().WithDamage(100));
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            EventCoordinator.TriggerEvent(EventName.Health.HealTree(), GameMessage.Write().WithHealth(50));
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            EventCoordinator.TriggerEvent(EventName.UI.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(2));
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            EventCoordinator.TriggerEvent(EventName.UI.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(-2));
        }
    }
}