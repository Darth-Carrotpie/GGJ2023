using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Health.HealthEmpty(), OnDeath);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Health.HealthEmpty(), OnDeath);
    }

    void OnDeath(GameMessage msg)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
    }
}
