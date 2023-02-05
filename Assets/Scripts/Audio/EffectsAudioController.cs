using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAudioController : MonoBehaviour
{
    public List<AudioClip> waterClips;
    public List<AudioClip> sunClips;
    public List<AudioClip> healClips;
    public List<AudioClip> damageClips;
    public List<AudioClip> stickClips;
    public List<AudioClip> upgradeClips;


    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        EventCoordinator.StartListening(EventName.Hostiles.DamageRoots(), PlayDamage);
        EventCoordinator.StartListening(EventName.Hostiles.DamageTrunk(), PlayDamage);
        EventCoordinator.StartListening(EventName.Health.HealTree(), PlayHeal);
        EventCoordinator.StartListening(EventName.Economy.ConsumeWater(), PlayWater);
        EventCoordinator.StartListening(EventName.Economy.ConsumeSunshine(), PlaySun);
        EventCoordinator.StartListening(EventName.Hostiles.DogFetchStick(), PlayStick);
        EventCoordinator.StartListening(EventName.Economy.UpgradeRoots(), PlayUpgrade);
        EventCoordinator.StartListening(EventName.Economy.UpgradeTrunk(), PlayUpgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayDamage(GameMessage msg)
    {
        PlayRandomFromList(damageClips);
    }
    void PlayHeal(GameMessage msg)
    {
        PlayRandomFromList(healClips);
    }
    void PlayWater(GameMessage msg)
    {
        PlayRandomFromList(waterClips);
    }
    void PlaySun(GameMessage msg)
    {
        PlayRandomFromList(sunClips);
    }
    void PlayStick(GameMessage msg)
    {
        PlayRandomFromList(stickClips);
    }
    void PlayUpgrade(GameMessage msg)
    {
        PlayRandomFromList(upgradeClips, 0.7f);
    }

    void PlayRandomFromList(List<AudioClip> list, float vol = 1.0f)
    {
        int index = Random.Range(0, list.Count);
        audioSource.PlayOneShot(list[index], vol);
    }
}
