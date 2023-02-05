using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField]
    Sprite _defaultSprite;
    [SerializeField]
    Sprite _clickedSprite;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _defaultSprite;
    }

    public IEnumerator ToggleOnForNSeconds(float seconds)
    {
        _spriteRenderer.sprite = _clickedSprite;
        yield return new WaitForSeconds(seconds);

        _spriteRenderer.sprite = _defaultSprite;
    }
}
