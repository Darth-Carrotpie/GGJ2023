using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitchTestInput : MonoBehaviour
{
    [SerializeField]
    SpriteSwitcher[] _spriteSwitcher;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach(var spriteSwitcher in _spriteSwitcher) 
                StartCoroutine(spriteSwitcher.ToggleOnForNSeconds(0.5f));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (var spriteSwitcher in _spriteSwitcher)
                StartCoroutine(spriteSwitcher.ToggleOnForNSeconds(0.5f));
        }
    }
}
