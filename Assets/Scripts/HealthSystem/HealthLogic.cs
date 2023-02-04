using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    const int startingHealth = 1000;

    private int maxHealth;
    private int currentHealth;
    private int regenOverTime;
    private int framesPassed;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth = startingHealth;
        this.regenOverTime = 0;

        EventCoordinator.StartListening(EventName.Hostiles.DamageTrunk(), TakeDamage);
        EventCoordinator.StartListening(EventName.Hostiles.DamageRoots(), TakeDamage);
        EventCoordinator.StartListening(EventName.Health.HealTree(), TakeHealing);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Hostiles.DamageTrunk(), TakeDamage);
        EventCoordinator.StopListening(EventName.Hostiles.DamageRoots(), TakeDamage);
        EventCoordinator.StopListening(EventName.Health.HealTree(), TakeHealing);
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime;
    }

    void TakeDamage(GameMessage msg)
    {
        ChangeHealth(-msg.damage);
    }

    void TakeHealing(GameMessage msg)
    {
        ChangeHealth(msg.health);
    }

    void ChangeHealth(int amount)
    {
        this.currentHealth += amount;
        if (this.currentHealth > this.maxHealth) {
            this.currentHealth = this.maxHealth;
        }
        if (this.currentHealth < 0) {
            this.currentHealth = 0;
        }
        CheckIfDead();

        int percent = (currentHealth * 100) / maxHealth;
        EventCoordinator.TriggerEvent(EventName.Health.CurrentPercent(), GameMessage.Write().WithHealth(percent));
    }

    void CheckIfDead()
    {
        if (this.currentHealth <= 0) {
            EventCoordinator.TriggerEvent(EventName.Health.HealthEmpty(), GameMessage.Write());
        }
    }
}
