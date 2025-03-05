using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DummyMoveset : MonoBehaviour
{
    PlayerMovement Player;
    int Frames = 0;
    public GameObject CrouchingLight; 
    bool isAttacking = false;

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
        if(isAttacking == false)
        {
          StartCoroutine(SingleHitAttack(CrouchingLight, 30, 10));
        }
        Debug.Log("Standing Light attack had been pressed");
        

        //play animation in here or put it in the singleHitAttack method
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

    public IEnumerator SingleHitAttack( GameObject hitbox, float AStart, float AEnd)
    {
       isAttacking = true; 
       
      AStart = (AStart/60) * Time.timeScale;
      AEnd = (AEnd/60) * Time.timeScale;

       yield return new WaitForSeconds(AStart);     
           hitbox.SetActive(true);   
           Debug.Log("Active attack frame");
           
          
       yield return new WaitForSeconds(AEnd);  
            hitbox.SetActive(false);
            
            Debug.Log("Attack is not active");
          
         

         isAttacking = false;
         
         // going to replace this with a coroutine
    }

}
