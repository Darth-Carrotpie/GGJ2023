using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoNode : MonoBehaviour {

    public SpriteRenderer rendLive;
    public SpriteRenderer rendDead;
    public GameObject livePrefab;
    public GameObject deadPrefab;
    float fadeSpeed = 1f;
    Coroutine coroutine;
    bool isFading;
    float currentFade = 0;

    public void Init() {
        rendLive = GetComponentsInChildren<SpriteRenderer>()[1];
        rendLive.color = new Color(1, 1, 1, 0f);
        
        rendDead = GetComponentsInChildren<SpriteRenderer>()[2];
        rendDead.color = new Color(1, 1, 1, 0f);
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

    public void Die() {
        rendLive.color = new Color(1, 1, 1, 0f);
        rendDead.color = new Color(1, 1, 1, 1f);
    }

    public void Resurrect() {
        rendLive.color = new Color(1, 1, 1, 1f);
        rendDead.color = new Color(1, 1, 1, 0f);
    }

    IEnumerator FadeIn() {
        isFading = true;
        while (currentFade <= 1f) {
            currentFade += Time.deltaTime * fadeSpeed;
            rendLive.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 1f;
        rendLive.color = new Color(1, 1, 1, currentFade);
        isFading = false;
        yield return null;
    }
    IEnumerator FadeOut() {
        isFading = true;
        while (currentFade >= 0f) {
            currentFade -= Time.deltaTime * fadeSpeed;
            rendLive.color = new Color(1, 1, 1, currentFade);
            yield return null;
        }
        currentFade = 0f;
        rendLive.color = new Color(1, 1, 1, currentFade);
        isFading = false;
        yield return null;
    }
}