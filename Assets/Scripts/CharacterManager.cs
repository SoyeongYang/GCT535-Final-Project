using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public int stageeNum;
    private GameManager gameManager;
    public float score, range;
    public int volume;
    public GameObject[] Stuffs;
    public GameObject Trigger;
    
    void SpawnTriggerObject()
    {
        range = Random.Range(gameManager.rangeCharacterMin, gameManager.rangeCharacterMax);
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
