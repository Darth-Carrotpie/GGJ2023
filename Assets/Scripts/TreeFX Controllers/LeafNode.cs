using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafNode : MonoBehaviour {

    public SpriteRenderer rend;
    float fadeSpeed = 1f;
    Coroutine coroutine;
    float currentFade = 0;
    PolygonCollider2D col;

    public void Init() {
        rend = GetComponentsInChildren<SpriteRenderer>()[1];
        rend.color = new Color(1, 1, 1, 0f);
        col = GetComponentInChildren<PolygonCollider2D>();
    }

    public void Activate() {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeIn());
        col.enabled = true;
    }

    public void Deactivate() {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeOut());
        col.enabled = false;
    }

    public void Damage() {

    }
    public void Heal() {

    }

    IEnumerator FadeIn() {
        while (currentFade <= 1f) {
            currentFade += Time.deltaTime * fadeSpeed;
            rend.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 1f;
        rend.color = new Color(1, 1, 1, currentFade);
        yield return null;
    }
    IEnumerator FadeOut() {
        while (currentFade >= 0f) {
            currentFade -= Time.deltaTime * fadeSpeed;
            rend.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 0f;
        rend.color = new Color(1, 1, 1, currentFade);
        yield return null;
    }
}