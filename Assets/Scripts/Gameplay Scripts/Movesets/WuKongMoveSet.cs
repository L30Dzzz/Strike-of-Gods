using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI;

public class WuKongMoveSet : MonoBehaviour
{
   PlayerMovement Player;
    
    public Color HpColor; 
    Image HealthBar;
    Image Icon;
    Image IconBg; 

    private Image PlayerIconBg;
    public Color IconColor;
    public Sprite Netural;
    public Sprite LowHp;
    public Sprite Winning; 
    
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

    public float fDashPow;
    public float bDashPow;

    
    HealthBar Canvas;

    Animator animationRunner;
    
    public bool isBlocking = false;
    public bool isAttacking = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        Player = GetComponent<PlayerMovement>();

        if(Player != null)
        {
         StartCoroutine(FindHp());
        }

        animationRunner = Player.GetComponent<Animator>();
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      playerCondition();
      IconChanger();

    }

  ////////////////////////////////////////// ATTACK CANCELING///////////////////////////////////////////////////////////////
  public void AttackReset()
  {
    isAttacking = false;
    Player.canAttack = false;
    Player.canRespond = true;
    animationRunner.SetTrigger("Attack Ended");
  }

//////////////////////////////////////// VISUALS /////////////////////////////////////////////

  IEnumerator FindHp()
    {
        yield return new WaitForSeconds(1/60); 
        
        if(Player.yourLayer.value == 64)
        {
            HealthBar = Player.hp.p1Health.GetComponent<Image>();
            Icon = Player.hp.P1PlayerIcon;
            IconBg = Player.hp.P1Icon.GetComponent<Image>();
            //Debug.Log("Found Player 1 hp");
        }
        else
        {
            HealthBar = Player.hp.p2Health.GetComponent<Image>();
            Icon = Player.hp.P2PlayerIcon;
            IconBg = Player.hp.P2Icon.GetComponent<Image>();
           // Debug.Log("Find Player 2 hp");
        }
        HealthBar.color = HpColor;
        IconBg.color = IconColor; 

        //Debug.Log("Loop is done");

        StopCoroutine(FindHp());
    }
    
    public void IconChanger()
    {
        if(Player.basehp > 241 && Player.basehp < 1137.9f)
        {
          Icon.sprite = Netural;
          
        }
        else if(Player.basehp <= 241)
        {
         Icon.sprite = LowHp;
        }
        else if(Player.basehp >= 1137.9f)
        {
          Icon.sprite = Winning;
        }
    }

/////////////////////////////////////// REGULAR ATTACKS /////////////////////////////////////////////////////////
    public void LightAttack(InputAction.CallbackContext context)
    {
      
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        if(isAttacking == false && Player.canRespond == true) 
        {
          if(Player.canCancel == true)
          {
            AttackReset();
            StopCoroutine(SingleHitAttack(StandLight, 4, 6, "Standing Light Attack"));
            animationRunner.SetTrigger("Attack Ended");
            
            
            
            Debug.Log("This works");
          }
          
          StartCoroutine(SingleHitAttack(StandLight, 4, 6, "Standing Light Attack"));
        }
        
        

        //play animation in here or put it in the singleHitAttack method
      }
      
       if(context.started && Player.isCrouching == true)
      {
        if(isAttacking == false && isBlocking == false && Player.canRespond == true)
        {
          StartCoroutine(SingleHitAttack(CrouchLight, 8, 2, "Light Crouch Attack"));
        }
      
         Debug.Log("Crouching Light attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        if(isAttacking == false && isBlocking == false && Player.canRespond == true)
        {
          StartCoroutine(SingleHitAttack(JumpLight, 6, 15, "Jump Light"));
        }
      
         Debug.Log("Jumping Light attack had been pressed");
      }

    }

    

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      if(context.started && Player.isCrouching == false && Player.isGrounded == true)
      {
        if(isAttacking == false && isBlocking == false && Player.canRespond == true)
        {
          StartCoroutine(SingleHitAttack(StandHeavy, 30, 10, "Standing Heavy"));
        }
        Debug.Log("Standing Heavy attack had been pressed");
        

        //play animation in here or put it in the singleHitAttack method
      }
      
       if(context.started && Player.isCrouching == true )
      {
        if(isAttacking == false && isBlocking == false && Player.canRespond == true)
        {
          StartCoroutine(SingleHitAttack(CrouchHeavy, 30, 5, "Crouching Heavy"));
        }
      
         Debug.Log("Crouching Heavy attack had been pressed");
      }
      
      if(context.started && Player.isGrounded == false)
      {
        if(isAttacking == false && isBlocking == false && Player.canRespond == true)
        {
          StartCoroutine(SingleHitAttack(Jumpheavy, 12, 19, "Jump Heavy"));
        }
      
         Debug.Log("Jumping Heavy attack had been pressed");
      }
    }

    public void Dash(InputAction.CallbackContext context)
    {
      if(((Player.isFacingRight == true && (Player.Input.x == 0 || Player.Input.x > 0.2f)) || (Player.isFacingRight == false && (Player.Input.x == 0 || Player.Input.x < -0.2f))) && Player.canRespond == true && Player.isGrounded == true && Player.isCrouching == false)
      {
       Debug.Log("Forward dash");
       StartCoroutine(Player.ForwardDash(fDashPow));


      }
      else if(((Player.isFacingRight == true && Player.Input.x < -0.2f) || (Player.isFacingRight == false && Player.Input.x > 0.2f)) && Player.canRespond == true && Player.isGrounded == true && Player.isCrouching == false)
      {
        Debug.Log("Backwards dash");
        StartCoroutine(Player.BackwardsDash(bDashPow));

      }
      

    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Special Attack had been pressed");
    }



//////////////////////////////////////////////////// Frame data method ////////////////////////////////

    private IEnumerator SingleHitAttack( GameObject hitbox, float AStart, float AEnd)
    {
       isAttacking = true; 
       Player.canAttack = true;
       
      AStart = (AStart/60) * Time.timeScale;
      AEnd = (AEnd/60) * Time.timeScale;
      
      yield return new WaitForSeconds(AStart);   
      
      if(Player.canAttack == true)
      {
           
           hitbox.SetActive(true);   
           //Debug.Log("Active attack frame");
      }
      else
      {
        Debug.Log("HitAttack is done");
      }
      
           
          
       yield return new WaitForSeconds(AEnd);  
            hitbox.SetActive(false);
            
            //Debug.Log("Attack is not active");
          
         

         AttackReset();
         
         // going to replace this with a coroutine
    }

    ///This one is made so I can test out the animations without breaking this entire script
    private IEnumerator SingleHitAttack( GameObject hitbox, float AStart, float AEnd, string aniName)
    {
       isAttacking = true; 
       Player.canAttack = true;

       Animator Animation = Player.Animator;

       Animation.SetTrigger(aniName);

      AStart = (AStart/60) * Time.timeScale;
      AEnd = (AEnd/60) * Time.timeScale;

       yield return new WaitForSeconds(AStart);     
           if(Player.canAttack == true)
           {
            hitbox.SetActive(true);   
            //Debug.Log("Active attack frame");
           }
           else
           {
            Animation.SetTrigger("Attack Ended");
           }
           
           
          
       yield return new WaitForSeconds(AEnd);  
            hitbox.SetActive(false);
            
            //Debug.Log("Attack is not active");
          
         
          

          AttackReset();

          
         
         
         // going to replace this with a coroutine
    }

    private void playerCondition()
    {
      
     ////////////////////////////////////////////////// Blocking Check /////////////////////////////////////////////////
      if(Player.StandBlock == true || Player.CrouchBlock == true)
      {
        isBlocking = true;
      }
      else
      {
        isBlocking = false;
      }   
      //////////////////////////////////////////// Attacking Check ///////////////////////////////////////

    
   

      /////////////////////////////////// TIMER CHECK /////////////////////////////////////////////

      Canvas = GameObject.FindObjectOfType<HealthBar>();

      if(Canvas.currentTime < 1)
      {
        Player.canRespond = false; 
      }

      /////////////////////////////////////////  HEALTH CHECK /////////////////////////////////////////////////

      if(Player.basehp <= 0)
      {
        Player.canRespond = false; 
        Player.Animator.SetBool("is Hurt", true);
      }
      else if(isAttacking == true)
      {
        Player.canRespond = false; 
      }
      
      if(Canvas.currentTime == Canvas.MaxTime)
      {
        Player.canRespond = true;
      }

    }


}
