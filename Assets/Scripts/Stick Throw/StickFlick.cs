using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFlick : MonoBehaviour
{
    

    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject anchorForRot;
    [SerializeField] private float rotSpeed = 100f;
    [SerializeField] private float arcSpeed = 20f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int destoryAfter = 6;

    int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        float randomNumber = Random.Range(0, 10);
        Debug.Log(randomNumber);
        Destroy(stick, destoryAfter);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomNumber == 0){
        transform.RotateAround(stick.transform.position, Vector3.back, rotSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        Debug.Log("normal right throw");
        }
        else{
        transform.RotateAround(stick.transform.position, Vector3.forward, rotSpeed * Time.deltaTime);
        //transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.RotateAround(anchorForRot.transform.position, Vector3.forward, arcSpeed * Time.deltaTime);
        Debug.Log("arc left throw");
        }

    }
}
