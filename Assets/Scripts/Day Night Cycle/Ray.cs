using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godray : MonoBehaviour {
    public SpriteRenderer rend;
    float fadeSpeed = 1f;
    Coroutine coroutine;
    float currentFade = 0;
    float peakTransparency = 0.5f;
    Color myColor;

    private void Start() {
        if (rend == null) {
            rend = GetComponent<SpriteRenderer>();
        }
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
    public void SetColor(Color color) {
        myColor = color;
        rend.color = new Color(myColor.r, myColor.g, myColor.b, currentFade);
    }
    IEnumerator FadeIn() {
        Color currentCol = rend.color;
        while (currentFade <= peakTransparency) {
            currentFade += Time.deltaTime * fadeSpeed;
            rend.color = new Color(myColor.r, myColor.g, myColor.b, currentFade);
            yield return null;
        }
        currentFade = peakTransparency;
        rend.color = new Color(myColor.r, myColor.g, myColor.b, currentFade);
        yield return null;
    }
    IEnumerator FadeOut() {
        Color currentCol = rend.color;
        while (currentFade >= 0f) {
            currentFade -= Time.deltaTime * fadeSpeed;
            rend.color = new Color(myColor.r, myColor.g, myColor.b, currentFade);
            yield return null;
        }
        currentFade = 0f;
        rend.color = new Color(myColor.r, myColor.g, myColor.b, currentFade);
        yield return null;
    }
}