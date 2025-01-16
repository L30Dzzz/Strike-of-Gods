using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DummyMoveset : MonoBehaviour
{
    PlayerMovement Player;
    int Frames = 0;
    public GameObject CrouchingLight; 

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
      
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        Debug.Log("Standing Light attack had been pressed");
        Frames = 10;

        SingleHitAttack(CrouchingLight,Frames, 6, 3);
      }
      
       if(context.started && Player.isCrouching == true )
      {
        Debug.Log("Crouching Light attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        Debug.Log("Jumping Light attack had been pressed");
      }

      if(context.started && ((Player.Input.x < 0 &&  Player.isFacingRight == false) || (Player.Input.x > 0 &&  Player.isFacingRight == true)))
      {
        Debug.Log("6L has been pressed");
      }

    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Medium attack had been pressed");
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        Debug.Log("Standing Heavy attack had been pressed");
      }
      
       if(context.started && Player.isCrouching == true )
      {
        Debug.Log("Crouching Heavy attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
            Debug.Log("Jumping Heavy attack had been pressed");
      }
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Special Attack had been pressed");
    }




    ///////////// Frame data method ////////////

    public void SingleHitAttack( GameObject hitbox, int Frame, int AStart, int AEnd)
    {
      
       for(int s = Frame; s >= 0 ; s--)
       {
         
         if( s <= AStart && s >= AEnd)
         {
           hitbox.SetActive(true);   
           Debug.Log("Active attack frame");
         }
         else
         {
            hitbox.SetActive(false);
         }


       }




    }

}
