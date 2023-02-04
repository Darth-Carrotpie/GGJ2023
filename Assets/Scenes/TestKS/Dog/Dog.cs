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
    public Transform bodyTransform;
    public AudioClip barkClip;
    public AudioClip peeClip;

    private AudioSource _soundSource;

    public UnityEngine.Events.UnityEvent onPee;
    public float peeInterval;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundSource = this.gameObject.AddComponent<AudioSource>();
        _soundSource.PlayOneShot(barkClip);
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

    public IEnumerator Pee(Vector3 peeSpot)
    {
        var actionRef = new ActionRef();
        _actionRef = actionRef;

        _soundSource.clip = peeClip;
        _soundSource.loop = true;
        _soundSource.Play();

        _animator.Play("dog_pee");
        peeEmitter.Play();
        // TODO: maybe not hardcode
        bodyTransform.localScale = new Vector3(-1, 1, 1);
        float nextPee = Time.time + peeInterval;

        while (true)
        {
            if (_actionRef != actionRef)
            {
                Debug.Log("Pee animation interrupted");
                peeEmitter.Stop();
                _soundSource.Stop();
                yield break;
            }

            if (Time.time < nextPee)
            {
                nextPee += peeInterval;
                onPee.Invoke();
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
        // TODO: maybe not hardcode
        bodyTransform.localScale = new Vector3(-1, 1, 1);

        while (Time.time < startTime + duration)
        {
            if (_actionRef != actionRef)
            {
                Debug.Log("Fetch animation interrupted");
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
