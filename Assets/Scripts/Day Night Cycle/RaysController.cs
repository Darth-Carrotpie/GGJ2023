using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RaysController : MonoBehaviour {
    [System.Serializable]
    public class RayLevel {
        [SerializeField]
        public List<int> indexesOfActives = new List<int>();
    }

    public GameObject godrayPrefab;

    public List<GameObject> raysGO = new List<GameObject>();
    public List<Godray> rays = new List<Godray>();
    //List<Godray> activeRays = new List<Godray>();

    [SerializeField]
    public List<RayLevel> levels = new List<RayLevel>();
    public Color nightRayColor;
    public Color dayRayColor;
    public int currentLevel = 0;
    //int previousLevel = 0;
    //int nextLevel = 0;
    private void Start() {
        foreach (GameObject go in raysGO) {
            rays.Add(go.GetComponentInChildren<Godray>());
            List<Animator> anims = go.GetComponentsInChildren<Animator>().ToList();
            anims[0].SetFloat("Offset", Random.Range(0.0f, 1.0f));
            anims[1].SetFloat("Offset", Random.Range(0.0f, 1.0f));
        }
        ResetLevel();
        EventCoordinator.StartListening(EventName.Environment.NextCycle(), OnCycleChange);
    }
    void OnCycleChange(GameMessage msg) {
        if (msg.cycle == Cycle.day) {
            SetColor(dayRayColor);
        }
        if (msg.cycle == Cycle.night) {
            SetColor(nightRayColor);
        }
    }
    public void SetSunshineLevel(float level) {
        if (Mathf.FloorToInt(level) - currentLevel > 0) {
            currentLevel = Mathf.FloorToInt(level);
            IncreaseLevel();
        }
        if (Mathf.FloorToInt(level) - currentLevel < 0) {
            ResetLevel();
            currentLevel = 0;
        }
    }

    void IncreaseLevel() {
        ResetLevel();
        //activeRays.Clear();

        foreach (int index in levels[currentLevel - 1].indexesOfActives) {
            rays[index].Activate();
            //activeRays.Add(activeRays[index]);
        }
    }

    void ResetLevel() {
        foreach (Godray godray in rays) {
            godray.Deactivate();
        }
    }

    void SetColor(Color color) {
        foreach (Godray godray in rays) {
            godray.SetColor(color);
        }
    }
}