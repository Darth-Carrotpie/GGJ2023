using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class LeavesFactory : MonoBehaviour {

    public int leavesMax = 40;
    int leavesCurrent = 0;
    public int targetLeaves;
    public int startingLeaves = 5;
    public float leafSizeMod = 0.6f;
    public float leafBehindPercentage = 0.7f;

    public List<GameObject> leavesPrefabs = new List<GameObject>();
    public List<GameObject> leafNodes = new List<GameObject>();

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ModifyLeaves(), ModifyLeaves);
        targetLeaves = startingLeaves;
        ReadNodes();
        GenerateInitialLeaves();
        AdjustLeaves(startingLeaves);
    }

    void ReadNodes() {
        List<LeafNode> nodes = GetComponentsInChildren<LeafNode>().ToList();
        foreach (LeafNode node in nodes) {
            leafNodes.Add(node.gameObject);
            Destroy(node.GetComponent<SpriteRenderer>());
        }
    }

    void GenerateInitialLeaves() {
        for (int i = 0; i < leavesMax; i++) {
            GameObject newLeaf = Instantiate(leavesPrefabs[Random.Range(0, leavesPrefabs.Count)]);
            newLeaf.transform.parent = leafNodes[i].transform;
            newLeaf.transform.localPosition = Vector3.zero;
            leafNodes[i].GetComponent<LeafNode>().Init();
            //newLeaf.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 5));
            //random range mirror the sprite if its 1; scale.x = -1
            int flipX = Random.Range(0, 1f) > 0.5f ? 1 : -1;
            newLeaf.transform.localScale = new Vector3(flipX, 1, 1) * leafSizeMod;
            //then randomly put behind branches, i.e. 70%
            int behind = Random.Range(0, 1f) > leafBehindPercentage ? 1 : -1;
            newLeaf.GetComponent<SpriteRenderer>().sortingOrder = behind;
        }
    }

    void ModifyLeaves(GameMessage msg) {
        targetLeaves += msg.leavesAmount;
        targetLeaves = Mathf.Clamp(targetLeaves, 0, leavesMax);
        int leavesDif = Mathf.Abs(targetLeaves - leavesCurrent);
        AdjustLeaves(Mathf.Abs(leavesDif));
    }

    void AdjustLeaves(int leavesDif) {
        if (targetLeaves > leavesCurrent)
            for (int i = 0; i < leavesDif; i++) {
                leafNodes[leavesCurrent + i].GetComponent<LeafNode>().Activate();
            }
        if (targetLeaves < leavesCurrent)
            for (int i = 0; i > -leavesDif; i--) {
                leafNodes[leavesCurrent + i].GetComponent<LeafNode>().Deactivate();
            }
        leavesCurrent = targetLeaves;
    }

}