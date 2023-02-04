using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKS : MonoBehaviour
{
  public Dog dogPrefab;
  public Transform dogSpawn;
  public Transform peeSpot;

  void Start()
  {
    StartCoroutine(Test());
  }

  IEnumerator Test()
  {
    var dog = Instantiate<Dog>(dogPrefab);

    yield return StartCoroutine(dog.Walk(dogSpawn.position, peeSpot.position, 3));

    StartCoroutine(dog.Pee(peeSpot.position, 100));

    // potential implementation to respond
    // while (!clicked) { yield return null; }
    yield return new WaitForSeconds(3);

    yield return StartCoroutine(dog.Fetch(peeSpot.position, dogSpawn.position, 0.5f));

    dog.PutDown();
  }
}
