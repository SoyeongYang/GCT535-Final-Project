using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class SimplePlayerUse : MonoBehaviour
{
    public GameObject mainCamera;
    private GameObject objectClicked;
    public GameObject flashlight;
    public KeyCode OpenClose;
    public KeyCode Flashlight;
    private StarterAssetsInputs _input;
    private PlayerInput _playerInput;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        // if (_input.interact) // Open and close action
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastCheck();
            _input.ResetInteraction();
        }
    }

    void RaycastCheck()
    {
        
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Interactable");  // Only hit objects on the Interactable layer
            float maxDistance = 5.3f;

            // Optionally combine masks with bitwise operations if checking multiple layers:
            // int layerMask = (1 << LayerMask.NameToLayer("Interactable")) | (1 << LayerMask.NameToLayer("AnotherLayer"));

            Vector3 rayDirection = mainCamera.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(mainCamera.transform.position, rayDirection, out hit, maxDistance, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<SimpleOpenClose>())
                {
                    // Debug.Log("Object with SimpleOpenClose script found");
                    hit.collider.gameObject.BroadcastMessage("ObjectClicked");
                }
                else
                {
                    Debug.Log("Object doesn't have script SimpleOpenClose attached");
                }
                Debug.DrawRay(mainCamera.transform.position, rayDirection * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(mainCamera.transform.position, rayDirection * maxDistance, Color.blue);
                Debug.Log("Did not Hit");
            }
        


    }

}