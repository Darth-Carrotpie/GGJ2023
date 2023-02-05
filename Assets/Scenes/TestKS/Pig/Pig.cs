using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pig : MonoBehaviour
{
    private Animator _animator;

    private class ActionRef { }
    private ActionRef _actionRef;

    public ParticleSystem dirtEmitter;
    public Transform bodyTransform;

    public AudioClip entryClip;
    public AudioClip damageClip;

    private AudioSource _soundSource;

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

    public IEnumerator Dig(Vector3 peeSpot, float peeIntensity)
    {
        var actionRef = new ActionRef();
        _actionRef = actionRef;

        _animator.Play("dig");

        _soundSource.clip = damageClip;
        _soundSource.loop = true;
        _soundSource.Play();

        float digTime = 0.25f + Time.time;

        while (true)
        {
            if (_actionRef != actionRef)
            {
                Debug.Log("Dig animation interrupted");
                _soundSource.Stop();
                yield break;
            }

            if (Time.time > digTime)
            {
                dirtEmitter.Play();
                digTime = Time.time + 0.5f;
            }

            yield return null;
        }
    }

    public IEnumerator Leave(Vector3 from, Vector3 to, float duration)
    {
        var actionRef = new ActionRef();
        _actionRef = actionRef;

        _animator.Play("walk");
        float startTime = Time.time;
        // TODO: maybe not hardcode
        // Can reuse walk
        bodyTransform.localScale = new Vector3(-1, 1, 1);

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
