using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconHeartListener : MonoBehaviour {

    TextMeshProUGUI tmp;
    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ModifyHearts(), OnModifyDroplets);
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnModifyDroplets(GameMessage msg) {
        tmp.text = msg.heartAmount.ToString();
    }
}