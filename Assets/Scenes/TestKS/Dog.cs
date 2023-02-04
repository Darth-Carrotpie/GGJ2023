using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dog : MonoBehaviour
{
  private Animator _animator;

  private class ActionRef { }
  private ActionRef _actionRef;

  public ParticleSystem peeEmitter;

  void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public IEnumerator Walk(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    _animator.Play("dog_walk");
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

    _animator.Play("dog_pee");
    peeEmitter.Play();

    while (true)
    {
      if (_actionRef != actionRef)
      {
        Debug.Log("Pee animation interrupted");
        peeEmitter.Stop();
        yield break;
      }

      yield return null;
    }
  }

  public IEnumerator Fetch(Vector3 from, Vector3 to, float duration)
  {
    var actionRef = new ActionRef();
    _actionRef = actionRef;

    _animator.Play("dog_jump");
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
