using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKS : MonoBehaviour
{
  public Dog dogPrefab;
  public Transform dogSpawn;
  public Transform peeSpot;

  public LJack lJackPrefab;
  public Transform lJackSpawn;
  public Transform cutSpot;

  void Start()
  {
    StartCoroutine(TestDog());
    StartCoroutine(TestLJack());
  }

  IEnumerator TestDog()
  {
    var dog = Instantiate<Dog>(dogPrefab);

    yield return StartCoroutine(dog.Walk(dogSpawn.position, peeSpot.position, 5));

    StartCoroutine(dog.Pee(peeSpot.position, 100));

    // potential implementation to respond
    // while (!clicked) { yield return null; }
    yield return new WaitForSeconds(3);

    yield return StartCoroutine(dog.Fetch(peeSpot.position, dogSpawn.position, 1f));

    dog.PutDown();
  }

  IEnumerator TestLJack()
  {
    var lJack = Instantiate<LJack>(lJackPrefab);
    lJack.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    yield return StartCoroutine(lJack.Walk(lJackSpawn.position, cutSpot.position, 5));

    StartCoroutine(lJack.Spin(cutSpot.position, 100));

    // potential implementation to respond
    // while (!clicked) { yield return null; }
    yield return new WaitForSeconds(3);

    yield return StartCoroutine(lJack.Leave(cutSpot.position, lJackSpawn.position, 3f));

    lJack.PutDown();
  }
}
