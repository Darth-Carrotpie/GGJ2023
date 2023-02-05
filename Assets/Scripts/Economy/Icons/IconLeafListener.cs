using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconLeafListener : MonoBehaviour {

    TextMeshProUGUI tmp;
    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ModifyLeaves(), OnModifyRecourse);
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnModifyRecourse(GameMessage msg) {
        tmp.text = msg.leavesAmount.ToString();
    }
}