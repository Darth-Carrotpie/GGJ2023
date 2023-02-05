using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecourseSpawner : MonoBehaviour
{
    private int ecourseCount = 6;
    private string recourseTag = "leaf";
    [SerializeField]private Vector3 spawnPos;
    [SerializeField] private GameObject recourse;
    [SerializeField] private Material[] mats;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            Create();
        }
    }

    void Create(){
        GameObject newObject = Instantiate(recourse) as GameObject;
        var getFloatToTarget = newObject.GetComponent<FloatToTarget>();
        foreach(Material mat in mats){
            
            if (mat.name == $"{recourseTag}_mat")
                getFloatToTarget.material = mat;
                Debug.Log(mat.name);
        }
        getFloatToTarget.recourseCount = ecourseCount;
        getFloatToTarget.recourseTag = recourseTag;
        getFloatToTarget.spawnPos = spawnPos;
    }
}
