using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGrowth : MonoBehaviour
{
    public SpriteRenderer rootSpriteRend;
    public Material material;
    bool isGrowing = false;

    public float growthIncrement = 0.05f;
    public float growthSpeed = 0.5f;

    public float currentGrowth = 0.01f;
    public float targetGrowth = 0.01f;

    void Start()
    {
        material = rootSpriteRend.material;
        EventCoordinator.StartListening(EventName.Economy.UpgradeRoots(), SetGrow);
    }

    void SetGrow(GameMessage msg){
        targetGrowth += growthIncrement;
        targetGrowth = Mathf.Clamp(targetGrowth, 0f, 1f);
        if(!isGrowing)
            StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        isGrowing = true;
        float startTime = Time.time;
        while (currentGrowth <= targetGrowth)
        {
            currentGrowth += Time.deltaTime * growthSpeed;
            material.SetFloat("_TreeThickness", currentGrowth);
            yield return null;
        }
        isGrowing = false;
        
        yield return null;
    }
}
