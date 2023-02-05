using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem woodchips;
    public ParticleSystem water;
    public ParticleSystem branches;
    public ParticleSystem leaves;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Hostiles.DamageTrunk(), Chip);
        EventCoordinator.StartListening(EventName.Economy.ModifyDroplets(), CollectWater);
        EventCoordinator.StartListening(EventName.Economy.ModifyBranches(), CollectBranches);
        EventCoordinator.StartListening(EventName.Economy.ModifyLeaves(), CollectLeaves);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Hostiles.DamageTrunk(), Chip);
        EventCoordinator.StartListening(EventName.Economy.ModifyDroplets(), CollectWater);
        EventCoordinator.StartListening(EventName.Economy.ModifyBranches(), CollectBranches);
        EventCoordinator.StartListening(EventName.Economy.ModifyLeaves(), CollectLeaves);
    }

    void Chip(GameMessage msg)
    {
        woodchips.Play();
    }

    void CollectWater(GameMessage msg)
    {
        if (msg.delta > 0)
        {
            water.Play();
        }
    }

    void CollectBranches(GameMessage msg)
    {
        if (msg.delta > 0)
        {
            branches.Play();
        }
    }

    void CollectLeaves(GameMessage msg)
    {
        if (msg.delta > 0)
        {
            leaves.Play();
        }
    }
}
