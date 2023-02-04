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

    public List<GameObject> potatoPrefabs = new List<GameObject>();
    public List<GameObject> potatoNodes = new List<GameObject>();

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ModifyPotatoes(), ModifyPotatoes);
        targetPotatoes = startingPotatoes;
        ReadNodes();
        GenerateInitialPotatoes();
        AdjustPotatoes(startingPotatoes);
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
            GameObject newPotato = Instantiate(potatoPrefabs[Random.Range(0, potatoPrefabs.Count)]);
            newPotato.transform.parent = potatoNodes[i].transform;
            newPotato.transform.localPosition = Vector3.zero;
            potatoNodes[i].GetComponent<PotatoNode>().Init();
            int flipX = Random.Range(0, 1f) > 0.5f ? 1 : -1;
            newPotato.transform.localScale = new Vector3(flipX, 1, 1) * potatoSizeMod;
            
            newPotato.GetComponent<SpriteRenderer>().sortingOrder = 1;
            Debug.Log(newPotato.GetComponent<SpriteRenderer>().sprite);
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

}