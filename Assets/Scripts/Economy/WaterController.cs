using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

    public SpriteRenderer rend;
    float cellOffset = 2;

    void Start() {
        EventCoordinator.StartListening(EventName.Economy.ConsumeWater(), OnWaterConsume);
    }

    void OnWaterConsume(GameMessage msg) {
        cellOffset += 0.57f;
        rend.material.SetFloat("_CellOffset", cellOffset);
    }

    public void SetWaterLevel(float setToLevel) {
        rend.material.SetFloat("_WaterFill", setToLevel);
    }
}