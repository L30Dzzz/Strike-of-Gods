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
      
      if(context.started && Player.isCrouching == false)
      {
        Debug.Log("Standing Light attack had been pressed");
      }
      
       if(context.started && Player.isCrouching == true)
      {
        Debug.Log("Crouching Light attack had been pressed");
        
      }
      
    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Medium attack had been pressed");
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Heavy attack had been pressed");
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Special Attack had been pressed");
    }

}
