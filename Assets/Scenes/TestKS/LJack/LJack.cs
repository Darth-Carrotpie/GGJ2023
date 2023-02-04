using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LJack : MonoBehaviour
{
    private Animator _animator;

    private class ActionRef { }
    private ActionRef _actionRef;
    public AudioClip entryClip;
    public AudioClip damageClip;

    private AudioSource _soundSource;


    public UnityEngine.Events.UnityEvent onSwing;
    public float swingInterval;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundSource = this.gameObject.AddComponent<AudioSource>();
        _soundSource.PlayOneShot(entryClip);
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

    public IEnumerator Spin(Vector3 peeSpot)
    {
        var actionRef = new ActionRef();
        _actionRef = actionRef;

        _animator.Play("spin");

        _soundSource.clip = damageClip;
        _soundSource.loop = true;
        _soundSource.Play();

        while (true)
        {
            if (_actionRef != actionRef)
            {
                Debug.Log("Spin animation interrupted");
                _soundSource.Stop();
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Leave(Vector3 from, Vector3 to, float duration)
    {
        var actionRef = new ActionRef();
        _actionRef = actionRef;

        _animator.Play("leave");
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            if (_actionRef != actionRef)
            {
                Debug.Log("Leave animation interrupted");
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
