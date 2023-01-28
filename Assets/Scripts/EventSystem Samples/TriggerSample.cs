using UnityEngine;

public class TriggerSample : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            EventCoordinator.TriggerEvent(EventName.SampleEventParentFolder.SpawnSampleObject(), GameMessage.Write());
        }
    }
}