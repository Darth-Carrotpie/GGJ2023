using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioController : MonoBehaviour
{
    public AudioClip dayBg;
    public AudioClip nightBg;
    public AudioClip dayMusic;
    public AudioClip nightMusic;

    AudioSource dayBgSource;
    AudioSource nightBgSource;
    AudioSource dayMusicSource;
    AudioSource nightMusicSource;
    float fadeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.Environment.NextCycle(), SwitchCycleAudio);
        dayBgSource = this.gameObject.AddComponent<AudioSource>();
        dayBgSource.loop = true;
        dayBgSource.clip = dayBg;
        nightBgSource = this.gameObject.AddComponent<AudioSource>();
        nightBgSource.loop = true;
        nightBgSource.clip = nightBg;
        dayMusicSource = this.gameObject.AddComponent<AudioSource>();
        dayMusicSource.loop = true;
        dayMusicSource.clip = dayMusic;
        nightMusicSource = this.gameObject.AddComponent<AudioSource>();
        nightMusicSource.loop = true;
        nightMusicSource.clip = nightMusic;

        dayBgSource.Play();
        dayMusicSource.Play();
        nightBgSource.volume = 0;
        nightBgSource.Play();
        nightMusicSource.volume = 0;
        nightMusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchCycleAudio(GameMessage msg)
    {
        if (msg.cycle == Cycle.day) {
            StartCoroutine(StartFade(dayBgSource, fadeSpeed, 1f));
            StartCoroutine(StartFade(dayMusicSource, fadeSpeed, 1f));
            StartCoroutine(StartFade(nightBgSource, fadeSpeed, 0f));
            StartCoroutine(StartFade(nightMusicSource, fadeSpeed, 0f));
        }

        if (msg.cycle == Cycle.night) {
            StartCoroutine(StartFade(dayBgSource, fadeSpeed, 0f));
            StartCoroutine(StartFade(dayMusicSource, fadeSpeed, 0f));
            StartCoroutine(StartFade(nightBgSource, fadeSpeed, 1f));
            StartCoroutine(StartFade(nightMusicSource, fadeSpeed, 1f));
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
