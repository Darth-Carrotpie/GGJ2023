using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ClicableType {
    Canopy,
    Roots,
    Mushroom,
    Blossom,
    Nest,
    Apple,
    Flower,
    Fence,
}
public class Clickable : MonoBehaviour {
    //for this to work on sprite, add Polygon Collider 2D
    public ClicableType type;
    public float longClickTime = 0.5f;
    bool keyDown;
    float timer;

    // Update is called once per frame
    void Update() {
        if (keyDown) {
            timer += Time.deltaTime;
        }
        if (timer > longClickTime) {
            Debug.Log("wanna trigger");
            EventCoordinator.TriggerEvent(EventName.Input.TapUpgrade(), GameMessage.Write().WithClickable(this));
            keyDown = false;
            timer = 0;
        }
    }

    void OnMouseDown() {
        keyDown = true;
        EventCoordinator.TriggerEvent(EventName.Input.TapRegular(), GameMessage.Write().WithClickable(this));
    }
    void OnMouseUp() {
        keyDown = false;
    }
}