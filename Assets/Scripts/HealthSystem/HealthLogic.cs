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
        EventCoordinator.StartListening(EventName.Health.HealTrunk(), TakeHealing);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Hostiles.DamageTrunk(), TakeDamage);
        EventCoordinator.StopListening(EventName.Health.HealTrunk(), TakeHealing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
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
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (this.currentHealth <= 0) {
            EventCoordinator.TriggerEvent(EventName.Health.HealthEmpty(), GameMessage.Write());
        }
    }
}
