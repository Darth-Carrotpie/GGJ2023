using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFxTestInputs : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeTrunk(), GameMessage.Write());
        }
        if(Input.GetKeyDown(KeyCode.D)){
            EventCoordinator.TriggerEvent(EventName.Hostiles.DamageTrunk(), GameMessage.Write());
        }
    }
}
