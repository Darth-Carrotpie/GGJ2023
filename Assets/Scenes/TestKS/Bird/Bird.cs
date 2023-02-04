using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bird : MonoBehaviour
{
  private class ActionRef { }
  private ActionRef _actionRef;

  public IEnumerator Fly(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

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
