using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOutlineInputs : MonoBehaviour {
    [SerializeField]
    OutlineToggle _outlineToggle;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            _outlineToggle.ToggleOn();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _outlineToggle.ToggleOff();
        }
    }
}