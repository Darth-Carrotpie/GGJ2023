using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.TapUpgrade(), UpgradeTree);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeTree(GameMessage msg){
        if (msg.clickable.type == ClicableType.Roots) {
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeRoots(), GameMessage.Write());
            EventCoordinator.TriggerEvent(EventName.UI.ModifyPotatoes(), GameMessage.Write().WithPotatoesAmount(1));
        }
        if (msg.clickable.type == ClicableType.Canopy) {
            EventCoordinator.TriggerEvent(EventName.Economy.UpgradeTrunk(), GameMessage.Write());
            EventCoordinator.TriggerEvent(EventName.UI.ModifyLeaves(), GameMessage.Write().WithLeavesAmount(2));
        }
    }
}
