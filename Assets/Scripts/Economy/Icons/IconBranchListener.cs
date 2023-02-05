using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconBranchListener : MonoBehaviour {

    TextMeshProUGUI tmp;
    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ModifyBranches(), OnModifyDroplets);
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnModifyDroplets(GameMessage msg) {
        tmp.text = msg.branchAmount.ToString();
    }
}