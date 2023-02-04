using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public Animator animDay;
    public Animator animNight;
    public Animator blueFilter;

    void Start() {
        EventCoordinator.StartListening(EventName.Environment.NextCycle(), OnCycleChange);
    }

    void OnCycleChange(GameMessage msg) {
        if (msg.cycle == Cycle.day) {
            animNight.SetBool("Show", false);
            animDay.SetBool("Show", true);
            blueFilter.SetBool("Show", false);

        }
        if (msg.cycle == Cycle.night) {
            animNight.SetBool("Show", true);
            animDay.SetBool("Show", false);
            blueFilter.SetBool("Show", true);
        }
    }
}