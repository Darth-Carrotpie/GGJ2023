using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSunsAndMoonsController : MonoBehaviour {

    public Animator animSuns;
    public Animator animMoons;
    public GameObject rain;
    void Start() {
        EventCoordinator.StartListening(EventName.Environment.NextCycle(), OnCycleChange);
    }

    void OnCycleChange(GameMessage msg) {
        if (msg.cycle == Cycle.day) {
            animSuns.SetBool("Show", true);
            animMoons.SetBool("Show", false);
            rain.SetActive(false);
        }
        if (msg.cycle == Cycle.night) {
            animSuns.SetBool("Show", false);
            animMoons.SetBool("Show", true);
            rain.SetActive(true);
        }
    }
}