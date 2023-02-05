using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PotatoesFactory : MonoBehaviour {

    public int potatoesMax = 10;
    int potatoesCurrent = 0;
    public int targetPotatoes;
    public int startingPotatoes = 1;
    public float potatoSizeMod = 0.01f;

    public GameObject potatoLivePrefab;
    public GameObject potatoDeadPrefab;

    public List<GameObject> potatoNodes = new List<GameObject>();

    /*
        1. Node has function to swap between live and dead prefabs being rendered.
        2. Factory listens to events and makes specific nodes dead
    */


    void Start() {
        EventCoordinator.StartListening(EventName.UI.ModifyPotatoes(), ModifyPotatoes);
        targetPotatoes = startingPotatoes;
        ReadNodes();
        GenerateInitialPotatoes();
        AdjustPotatoes(startingPotatoes);

        EventCoordinator.StartListening(EventName.Health.CurrentPercent(), LivePotatoesByHealth);
    }

    void ReadNodes() {
        List<PotatoNode> nodes = GetComponentsInChildren<PotatoNode>().ToList();
        foreach (PotatoNode node in nodes) {
            potatoNodes.Add(node.gameObject);
            Destroy(node.GetComponent<SpriteRenderer>());
        }
    }

    void GenerateInitialPotatoes() {
        for (int i = 0; i < potatoesMax; i++) {
            int flipX = Random.Range(0, 1f) > 0.5f ? 1 : -1;

            GameObject newPotato = Instantiate(potatoLivePrefab);
            newPotato.transform.parent = potatoNodes[i].transform;
            newPotato.transform.localPosition = Vector3.zero;
            newPotato.transform.localScale = new Vector3(flipX, 1, 1) * potatoSizeMod;
            newPotato.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
            GameObject newDeadPotato = Instantiate(potatoDeadPrefab);
            newDeadPotato.transform.parent = potatoNodes[i].transform;
            newDeadPotato.transform.localPosition = Vector3.zero;
            newDeadPotato.transform.localScale = new Vector3(flipX, 1, 1) * potatoSizeMod;
            newDeadPotato.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
            potatoNodes[i].GetComponent<PotatoNode>().Init();
        }
    }

    void ModifyPotatoes(GameMessage msg) {
        targetPotatoes += msg.potatoesAmount;
        targetPotatoes = Mathf.Clamp(targetPotatoes, 0, potatoesMax);
        int potatoDif = Mathf.Abs(targetPotatoes - potatoesCurrent);
        AdjustPotatoes(Mathf.Abs(potatoDif));
    }

    void AdjustPotatoes(int potatoDif) {
        if (targetPotatoes > potatoesCurrent)
            for (int i = 0; i < potatoDif; i++) {
                potatoNodes[potatoesCurrent + i].GetComponent<PotatoNode>().Activate();
            }
        if (targetPotatoes < potatoesCurrent)
            for (int i = 0; i > -potatoDif; i--) {
                potatoNodes[potatoesCurrent + i].GetComponent<PotatoNode>().Deactivate();
            }
        potatoesCurrent = targetPotatoes;
    }

    void LivePotatoesByHealth(GameMessage msg) {
        int livePotatoes = (int) Mathf.Ceil(potatoesCurrent * msg.health / 100f);
        for (int i = 0; i < livePotatoes; i++) {
            potatoNodes[i].GetComponent<PotatoNode>().Resurrect();
        }
        for (int i = livePotatoes; i < potatoesCurrent; i++) {
            potatoNodes[i].GetComponent<PotatoNode>().Die();
        }
    }

}