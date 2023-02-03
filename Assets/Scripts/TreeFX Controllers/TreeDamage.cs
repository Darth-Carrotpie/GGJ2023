using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDamage : MonoBehaviour
{
    public SpriteRenderer treeSpriteRend;
    public Material material;
    bool isFlashing = false;

    public float flashSpeed = 2.5f;

    public float currentEdge = 0.00f;
    public float secondStrength = 0.1f;
    public float firstStrength = 0.2f;

    Coroutine coroutine;

    public float currentThreeGrowth;

    void Start()
    {
        material = treeSpriteRend.material;
        EventCoordinator.StartListening(EventName.Hostiles.DamageTrunk(), FlashOutline);
    }

    void FlashOutline(GameMessage msg){

        if(!isFlashing)
            coroutine = StartCoroutine(Grow());
        else{
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Grow());
        }
    }

    IEnumerator Grow()
    {
        isFlashing = true;
        float startTime = Time.time;
        float progress = 0f;
        currentThreeGrowth = material.GetFloat("_TreeThickness");
        while (progress <= 1)
        {
            progress += Time.deltaTime * flashSpeed * 1.5f;
            currentEdge = Mathf.Sin(progress * Mathf.PI * 2)  * Mathf.Min(firstStrength, currentThreeGrowth-0.01f);
            material.SetFloat("_OutlineThickness", currentEdge);
            yield return null;
        }
        progress = 0f;
        while (progress <= 1)
        {
            progress += Time.deltaTime * flashSpeed;
            currentEdge = Mathf.Sin(progress * Mathf.PI * 2) * Mathf.Min(secondStrength, currentThreeGrowth-0.01f);
            material.SetFloat("_OutlineThickness", currentEdge);
            yield return null;
        }
        material.SetFloat("_OutlineThickness", 0);
        isFlashing = false;
        yield return null;
    }
}
