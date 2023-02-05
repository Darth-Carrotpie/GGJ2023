using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Dog dogPrefab;
    public Transform dogSpawn;
    public Transform peeSpot;

    public LJack lJackPrefab;
    public Transform lJackSpawn;
    public Transform cutSpot;

    public Hippy hippyPrefab;
    public Transform hippySpawn;
    public Transform hugSpot;

    public float dogsPerMinute = 10;
    public float lJacksPerMinute = 5;
    public int peeDamage = 100;
    public int axeDamage = 100;
    [Range(0.0f, 10.0f)]
    public float locationSpread = 2.0f;

    private float nextDogT = 60;
    private float nextLJackT = 60;

    private Queue<Dog> _dogs = new Queue<Dog>();
    private Queue<LJack> _lJacks = new Queue<LJack>();

    public bool isTesting = false;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Hostiles.DogFetchStick(), DogFetch);
        EventCoordinator.StartListening(EventName.Hostiles.SendHippy(), SendHippy);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Hostiles.DogFetchStick(), DogFetch);
        EventCoordinator.StopListening(EventName.Hostiles.SendHippy(), SendHippy);
    }

    void DogFetch(GameMessage msg)
    {
        if (_dogs.Count == 0)
        {
            // Any way to cancel message?
            Debug.LogWarning("Stop dog when no dog needs to be stopped");
            return;
        }

        var dog = _dogs.Dequeue();
        // It should be peeing
        dog.Stop();
    }

    void SendHippy(GameMessage msg)
    {
        if (_lJacks.Count == 0)
        {
            // Any way to cancel message?
            Debug.LogWarning("Stop lJack when no lJack needs to be stopped");
            return;
        }

        var lJack = _lJacks.Dequeue();

        StartCoroutine(SendHippyCO(lJack));
    }

    void Update()
    {
        nextDogT -= Time.deltaTime * dogsPerMinute;
        if (nextDogT < 0)
        {
            nextDogT += 60;
            StartCoroutine(SpawnDogCO());
        }
        nextLJackT -= Time.deltaTime * lJacksPerMinute;
        if (nextLJackT < 0)
        {
            nextLJackT += 60;
            StartCoroutine(SpawnLJackCO());
        }

        if (isTesting)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                EventCoordinator.TriggerEvent(EventName.Hostiles.SendHippy(), GameMessage.Write());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                EventCoordinator.TriggerEvent(EventName.Hostiles.DogFetchStick(), GameMessage.Write());
            }
        }
    }

    void OnPee()
    {
        // Debug.Log("OnPEE");
        EventCoordinator.TriggerEvent(EventName.Hostiles.DamageRoots(), GameMessage.Write().WithDamage(peeDamage));
    }

    void OnAxe()
    {
        // Debug.Log("OnAXE");
        EventCoordinator.TriggerEvent(EventName.Hostiles.DamageTrunk(), GameMessage.Write().WithDamage(axeDamage));
    }

    IEnumerator SpawnDogCO()
    {
        var dog = Instantiate<Dog>(dogPrefab);
        dog.onPee.AddListener(OnPee);
        var offset = Offset();

        yield return StartCoroutine(dog.Walk(dogSpawn.position + offset, peeSpot.position + offset, 5));

        _dogs.Enqueue(dog);

        // Pee until interrupted
        yield return StartCoroutine(dog.Pee(peeSpot.position + offset));

        yield return StartCoroutine(dog.Fetch(peeSpot.position + offset, dogSpawn.position + offset, 2));

        dog.PutDown();
    }

    public int DogCount()
    {
        return _dogs.Count;
    }

    IEnumerator SpawnLJackCO()
    {
        var lJack = Instantiate<LJack>(lJackPrefab);
        lJack.onSwing.AddListener(OnAxe);
        var offset = Offset();

        yield return StartCoroutine(lJack.Walk(lJackSpawn.position + offset, cutSpot.position + offset, 5));

        _lJacks.Enqueue(lJack);
        // Pee until interrupted
        yield return StartCoroutine(lJack.Spin(cutSpot.position + offset));

        yield return StartCoroutine(lJack.Walk(cutSpot.position + offset, lJackSpawn.position + offset, 2));

        lJack.PutDown();
    }

    IEnumerator SendHippyCO(LJack lJack)
    {
        var hippy = Instantiate<Hippy>(hippyPrefab);
        var offset = lJack.transform.position - cutSpot.position;

        yield return StartCoroutine(hippy.Walk(hippySpawn.position + offset, hugSpot.position + offset, 1));

        StartCoroutine(hippy.Hug(hugSpot.position + offset));

        yield return new WaitForSeconds(2);

        lJack.Stop();

        yield return StartCoroutine(hippy.Walk(hugSpot.position + offset, hippySpawn.position + offset, 6f));

        hippy.PutDown();
    }

    public int LJackCount()
    {
        return _lJacks.Count;
    }

    Vector3 Offset()
    {
        Vector3 offset = Random.insideUnitCircle * locationSpread;
        offset.z = -offset.x * 0.1f + offset.y;
        offset.x *= 2;
        return offset;
    }

}
