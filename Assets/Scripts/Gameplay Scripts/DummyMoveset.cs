using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DummyMoveset : MonoBehaviour
{
    PlayerMovement Player;
    
    public GameObject StandLight; 
    public GameObject CrouchLight; 
    public GameObject JumpLight; 
    public GameObject UniAnti;
    
    public GameObject StandMedium; 
    public GameObject CrouchMedium; 
    public GameObject JumpMedium; 

    public GameObject StandHeavy; 
    public GameObject CrouchHeavy; 
    public GameObject Jumpheavy; 
    
    public bool isBlocking = false;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
         
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.StandBlock == true || Player.CrouchBlock == true)
        {
          isBlocking = true;
        }
        else
        {
          isBlocking = false;
        }       

    }
    
    public void LightAttack(InputAction.CallbackContext context)
    {
      
      if(context.started && Player.isCrouching == false && Player.isGrounded == true && Player.Input.x == 0)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(StandLight, 4, 6));
        }
        Debug.Log("Standing Light attack had been pressed");
        

        //play animation in here or put it in the singleHitAttack method
      }
      
       if(context.started && Player.isCrouching == true )
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(CrouchLight, 5, 5));
        }
      
         Debug.Log("Crouching Light attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(JumpLight, 6, 15));
        }
      
         Debug.Log("Jumping Light attack had been pressed");
      }

      if(context.started && ((Player.Input.x  < 0 &&  Player.isFacingRight == false) || (Player.Input.x > 0 &&  Player.isFacingRight == true)))
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(UniAnti, 9, 6));
        }
      
         Debug.Log("6L has been pressed");
      }

    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(StandMedium, 6, 9));
        }
        Debug.Log("Standing Medium attack had been pressed");
        

        //play animation in here or put it in the singleHitAttack method
      }
      
       if(context.started && Player.isCrouching == true )
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(CrouchMedium, 6, 9));
        }
      
         Debug.Log("Crouching Medium attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(JumpMedium, 8, 12));
        }
      
         Debug.Log("Jumping Medium attack had been pressed");
      }


    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(StandHeavy, 10, 14));
        }
        Debug.Log("Standing Heavy attack had been pressed");
        

        //play animation in here or put it in the singleHitAttack method
      }
      
       if(context.started && Player.isCrouching == true )
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(CrouchHeavy, 9, 14));
        }
      
         Debug.Log("Crouching Heavy attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        if(isAttacking == false && isBlocking == false)
        {
          StartCoroutine(SingleHitAttack(Jumpheavy, 12, 19));
        }
      
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
