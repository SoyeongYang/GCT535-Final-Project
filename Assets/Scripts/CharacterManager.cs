using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public int stageNum;
    private GameManager gameManager;
    public float score = 0f;
    public float range = 1f;
    public int volume;
    public GameObject[] Stuffs;
    // public GameObject Trigger;
    public GameObject collisionTarget; 
    
    void SpawnTriggerObject()
    {
        range = Random.Range(gameManager.rangeCharacterMin, gameManager.rangeCharacterMax);
        score = 100 / range;
        volume = Random.Range(gameManager.rangeVolumeMin, gameManager.rangeVolumeMax);
        // Trigger.transform.SetParent(gameObject.transform);
        
        // SphereCollider triggerCollider = Trigger.AddComponent<SphereCollider>();
        // triggerCollider.radius = 2f;
        // triggerCollider.isTrigger = true;

        // Rigidbody triggerRigidbody = Trigger.AddComponent<Rigidbody>();
        // triggerRigidbody.isKinematic = true;  // Trigger는 물리적인 영향을 받지 않도록 설정
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Trigger = new GameObject("Trigger");
        SpawnTriggerObject();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == collisionTarget)
        {
            Debug.Log("Player collided with character: " + name);
            gameManager.PlayerCollidedWithCharacter(this);
        }
        else
        {
            Debug.Log("Other object collided: " + other.name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
