using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFlick : MonoBehaviour
{
    

    //[SerializeField] private GameObject stick;
    [SerializeField] private GameObject anchorForRot;
    [SerializeField] private float rotSpeed = 100f;
    [SerializeField] private float arcSpeed = 20f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int destoryAfter = 6;

    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        anchorForRot = GameObject.FindGameObjectsWithTag("Anchor").First();
        Destroy(this.gameObject, destoryAfter);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == "Right"){
        transform.RotateAround(this.transform.position, Vector3.back, rotSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        Debug.Log("normal right throw");
        }
        else if(direction == "Left"){
        transform.RotateAround(this.transform.position, Vector3.forward, rotSpeed * Time.deltaTime);
        //transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.RotateAround(anchorForRot.transform.position, Vector3.forward, arcSpeed * Time.deltaTime);
        Debug.Log("arc left throw");
        }

    }
}
