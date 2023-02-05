using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe ;

public class SpawnStick : MonoBehaviour
{

    [SerializeField] private GameObject stick;
    [SerializeField] private Transform stickSpawner;
    [SerializeField] private SwipeListener swipeListener ;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(stick, stickSpawner.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    private void OnEnable(){
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe){
        UnityEngine.Debug.Log(swipe);
        if (swipe == "Right")
        {
            EventCoordinator.TriggerEvent(EventName.Hostiles.DogFetchStick(), GameMessage.Write());
            Create(swipe);
        }
        if (swipe == "Left")
        {
            EventCoordinator.TriggerEvent(EventName.Hostiles.SendHippy(), GameMessage.Write());
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    public void Create(string direction)
    {
        GameObject newObject = Instantiate(stick) as GameObject;
        var getComp = newObject.GetComponent<StickFlick>();
        getComp.direction = direction;
    }

}
