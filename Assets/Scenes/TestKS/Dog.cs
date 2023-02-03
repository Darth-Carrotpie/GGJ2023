using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
  public Sprite walkSprite;
  public Sprite peeSprite;
  public Sprite jumpSprite;

  private class ActionRef { }
  private ActionRef _actionRef;

  public IEnumerator Walk(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    GetComponent<SpriteRenderer>().sprite = walkSprite;
    float startTime = Time.time;

    while (Time.time < startTime + duration)
    {
      if (_actionRef != actionRef)
      {
        Debug.Log("Walk animation interrupted");
        yield break;
      }

      float t = (Time.time - startTime) / duration;
      transform.position = Vector2.Lerp(from, to, t);

      yield return null;
    }

    transform.position = to;
  }

  public IEnumerator Pee(Vector3 peeSpot, float peeIntensity)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    GetComponent<SpriteRenderer>().sprite = peeSprite;

    while (true)
    {
      if (_actionRef != actionRef)
      {
        Debug.Log("Pee animation interrupted");
        yield break;
      }

      yield return null;
    }
  }

  public IEnumerator Fetch(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    GetComponent<SpriteRenderer>().sprite = jumpSprite;
    float startTime = Time.time;

    while (Time.time < startTime + duration)
    {
      if (_actionRef != actionRef)
      {
        Debug.Log("Fetch animation interrupted");
        yield break;
      }

      // TODO: animate jump
      float t = (Time.time - startTime) / duration;
      transform.position = Vector2.Lerp(from, to, t);

      yield return null;
    }

    transform.position = to;
  }

  public void Stop()
  {
    _actionRef = null;
  }

  public void PutDown()
  {
    _actionRef = null;
    Destroy(gameObject);
  }
}
