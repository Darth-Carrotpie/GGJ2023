using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hippy : MonoBehaviour
{
  private Animator _animator;

  private class ActionRef { }
  private ActionRef _actionRef;

  void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public IEnumerator Walk(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    _animator.Play("walk");
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

  public IEnumerator Hug(Vector3 peeSpot)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    _animator.Play("hug");

    while (true)
    {
      if (_actionRef != actionRef)
      {
        Debug.Log("Hug animation interrupted");
        yield break;
      }

      yield return null;
    }
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
