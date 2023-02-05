using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToTarget : MonoBehaviour
{
    private Vector3 targetPos;
    private GameObject recourseObject;
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private float speed = 10f;
    public int recourseCount = 1;
    public Material material;
    public string recourseTag = "heart";
    public Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        recourseObject = GameObject.FindGameObjectsWithTag(recourseTag)[0];
        targetPos = recourseObject.transform.position;
        Destroy(this.gameObject, 1.45f);
        particleSys.emission.GetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, recourseCount)});
        particleSys.GetComponent<ParticleSystemRenderer>().material = material;

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, recourseObject.transform.position, step);
    }
}
