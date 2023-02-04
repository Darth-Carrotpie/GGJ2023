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

  public Hippy hippyPrefab;
  public Transform hippySpawn;
  public Transform hugSpot;

  public Bird birdPrefab;
  public Transform birdSpawn;
  public Transform birdEnd;

  void Start()
  {
    StartCoroutine(TestDog());
    StartCoroutine(TestLJack());
    StartCoroutine(TestHippy());
    StartCoroutine(TestBird());
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
    // lJack.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    yield return StartCoroutine(lJack.Walk(lJackSpawn.position, cutSpot.position, 5));

    StartCoroutine(lJack.Spin(cutSpot.position));

    // potential implementation to respond
    // while (!hugged) { yield return null; }
    yield return new WaitForSeconds(3);

    yield return StartCoroutine(lJack.Leave(cutSpot.position, lJackSpawn.position, 3f));

    lJack.PutDown();
  }

  IEnumerator TestHippy()
  {
    yield return new WaitForSeconds(4);

    var hippy = Instantiate<Hippy>(hippyPrefab);
    // hippy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    yield return StartCoroutine(hippy.Walk(hippySpawn.position, hugSpot.position, 2));

    StartCoroutine(hippy.Hug(hugSpot.position));

    yield return new WaitForSeconds(2);

    yield return StartCoroutine(hippy.Walk(hugSpot.position, hippySpawn.position, 6f));

    hippy.PutDown();
  }

  IEnumerator TestBird()
  {
    var bird = Instantiate<Bird>(birdPrefab);

    yield return StartCoroutine(bird.Fly(birdSpawn.position, birdEnd.position, 6));

    bird.PutDown();
  }
}
