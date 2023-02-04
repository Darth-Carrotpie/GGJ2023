using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConsumer : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.TapRegular(), OnClickableClick);
    }

    void OnClickableClick(GameMessage msg) {
        if (msg.clickable.type == ClicableType.Canopy) {
            EventCoordinator.TriggerEvent(EventName.Economy.ConsumeSunshine(), GameMessage.Write());
        }
        if (msg.clickable.type == ClicableType.Roots) {
            EventCoordinator.TriggerEvent(EventName.Economy.ConsumeWater(), GameMessage.Write());
        }
    }
}