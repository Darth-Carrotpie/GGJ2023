using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineToggle : MonoBehaviour
{
    [SerializeField]
    Material _outlineMaterial;
    [SerializeField]
    Material _defaultMaterial;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ToggleOn()
    {
        _spriteRenderer.material = _outlineMaterial;
    }

    public void ToggleOff()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
}
