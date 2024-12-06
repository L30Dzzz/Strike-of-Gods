using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DummyMoveset : MonoBehaviour
{
    PlayerMovement Player;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LightAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Dummy did a Light attack ");
    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Dummy did a Medium attack ");
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Dummy did a Heavy attack ");
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Dummy did a Special Attack ");
    }

}
