using UnityEngine;

public class ListenerSample : MonoBehaviour {

    public GameObject sampleObjectPrefab;

    private void Start() {
        EventCoordinator.StartListening(EventName.SampleEventParentFolder.SpawnSampleObject(), OnSampleTrigger);
    }

    void OnSampleTrigger(GameMessage msg) {
        GameObject newObj = Instantiate(sampleObjectPrefab);
        float distX = Random.Range(0, 3f);
        float distY = Random.Range(0, 3f);
        newObj.transform.position = new Vector3(distX, distY, 0);

        newObj.transform.parent = this.transform;
    }
}