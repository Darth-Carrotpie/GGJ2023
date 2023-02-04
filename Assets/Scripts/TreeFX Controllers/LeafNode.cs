using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafNode : MonoBehaviour {

    public SpriteRenderer rend;
    float fadeSpeed = 1f;
    Coroutine coroutine;
    bool isFading;
    float currentFade = 0;

    public void Init() {
        rend = GetComponentsInChildren<SpriteRenderer>()[1];
        rend.color = new Color(1, 1, 1, 0f);
    }

    public void Activate() {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeIn());
    }

    public void Deactivate() {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeOut());
    }

    public void Damage() {

    }
    public void Heal() {

    }

    IEnumerator FadeIn() {
        isFading = true;
        while (currentFade <= 1f) {
            currentFade += Time.deltaTime * fadeSpeed;
            rend.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 1f;
        rend.color = new Color(1, 1, 1, currentFade);
        isFading = false;
        yield return null;
    }
    IEnumerator FadeOut() {
        isFading = true;
        while (currentFade >= 0f) {
            currentFade -= Time.deltaTime * fadeSpeed;
            rend.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 0f;
        rend.color = new Color(1, 1, 1, currentFade);
        isFading = false;
        yield return null;
    }
}