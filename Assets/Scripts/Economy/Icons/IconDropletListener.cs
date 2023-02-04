using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconDropletListener : MonoBehaviour {

    TextMeshProUGUI tmp;
    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ModifyDroplets(), OnModifyDroplets);
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnModifyDroplets(GameMessage msg) {
        tmp.text = msg.dropletAmount.ToString();
    }
}