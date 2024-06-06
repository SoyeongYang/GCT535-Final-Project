using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    public int uniqueNum;
    public GameManager gameManager;
    public float score, range;
    public int volume;
    public GameObject Trigger;

    public Vector3 initPosition = new Vector3(0, 0, 0);

    void SpawnTriggerObject()
    {
        range = Random.Range(gameManager.rangeStuffMin, gameManager.rangeStuffMax);
        score = 100 / range;
        volume = Random.Range(gameManager.rangeVolumeMin, gameManager.rangeVolumeMax);
        Trigger.transform.SetParent(gameObject.transform);
        SphereCollider triggerCollider = Trigger.AddComponent<SphereCollider>();
        triggerCollider.radius = range;
        triggerCollider.isTrigger = true;
    }
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Trigger = new GameObject("Trigger");
        SpawnTriggerObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
