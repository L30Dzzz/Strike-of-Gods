using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))] // auto adds a character controller to anything with this script
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public Vector2 Input; // think of this as a horziontal and vertical input method mushed together
   // Note that this type of way to get the x and y inputs for your player is good when using unity new input system as far as I know
   public Rigidbody2D  rb; 
   public float weight; 
   float speed; 
   [SerializeField] float movespeed1; // normal movement speed
   [SerializeField] float movespeed2; // moving back movement speed
   float movespeed3; // running movment speed 
   public float regJump; // How high the character will jump
   public int jsFrame; 
   public int jsFrameStart;
   public float Dash;
   private int layerAsLayerMask;
   int airMovementFrames; 
   public float basehp = 0;
   public float Meter = 0; 
   
   
   public bool isFacingRight = true; 
   public bool isCrouching; 
   public bool isGrounded = false;
   public bool CrouchBlock = false;
   public bool StandBlock = false; 
   public bool canRespond = true;
   public bool canAttack = false;
   public bool canCancel = false; 

  // Define the size and direction for the BoxCast
   public Vector2 boxSize = new Vector2(0.5f, 6);
   

   bool jumpSquat; 
   private CharacterController playerController; 
   public HealthBar hp; 
   public Animator Animator;

   public PlayerTemplate playerTemplate; 
   public LayerMask yourLayer;
   public LayerMask opsLayer;




   
  
  // String[] inputCommand; //work on this later
   //enum motionInput{}; 
   // public GameObject cube; 


    // Start is called before the first frame update
    void Awake()
    {

       yourLayer = playerTemplate.yourLayer; 
       opsLayer = playerTemplate.opsLayer;
       layerAsLayerMask = (1 << this.gameObject.layer);

        GetHealt_N_Meter();
        
        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      Health_n_Meter();
     

      if(canRespond == true)
      {
      MovementFunction();
      Jumping();  
      characterFlipFunction();
      isBlocking();
      AniCheck();
      }

      
      
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Gizmos.DrawCube(rb.position + new Vector2(2,0), boxSize);
        Gizmos.DrawCube(rb.position + new Vector2(-2,0), boxSize);
    }

 ////////////////////////////////   MOVEMENT     //////////////////////////////////////////////////////////////////////////////
    public void Move(InputAction.CallbackContext context)
    {
        Input.x = context.ReadValue<Vector2>().x;
        Input.y = context.ReadValue<Vector2>().y; 
    }

    private void MovementFunction()
    {
       // allows the player to move left and right only if they not crouching 
       if(Input.y < -0.4 && isGrounded == true)
       {
         isCrouching = true; 
       }
       else
       {
        if(isGrounded == true)
        {
          transform.Translate(Vector2.right * Input.x * Time.deltaTime * speed); // this makes the player actually move 
        }
        
        isCrouching = false; 
       }
       
       
       // changes the speed of your character depending on what side you are facing 
       if((isFacingRight == true && Input.x < 0) || (isFacingRight == false && Input.x > 0))
       {
         speed = movespeed2; 
       }
       else if((isFacingRight == true && Input.x > 0) || (isFacingRight == false && Input.x < 0)) 
       {
         speed = movespeed1; 
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
          isGrounded = true;
          airMovementFrames = 5;
        }
        else
        {
          airMovementFrames -= 1; 
        }
    }

  
   public IEnumerator ForwardDash(float dashPow)
   {
    
    Dash = dashPow; 
    
    if(canRespond == true)
    {
      canRespond = false; 

      Animator.SetTrigger("Dash Forward");

      if(transform.localScale.x > 0)
      {
        rb.velocity = Vector2.right * Dash;
      }
      else if(transform.localScale.x < 0)
      {
        rb.velocity = -Vector2.right * Dash;
      }

      yield return new WaitForSeconds(1);

      canRespond = true;
    }

    
     
  }

  public IEnumerator BackwardsDash(float dashPow)
   {
    
    Dash = dashPow; 
    
    if(canRespond == true )
    {
      canRespond = false; 

      Animator.SetTrigger("Dash Backwards");

      if(transform.localScale.x > 0)
      {
        rb.velocity = Vector2.left * Dash;
      }
      else if(transform.localScale.x < 0)
      {
        rb.velocity = -Vector2.left * Dash;
      }

      yield return new WaitForSeconds(0.5f);

      canRespond = true;
    }

   }
   
  ////////////////////////////////////////////MOVEMENT ANIMATION CHECKING///////////////////////////////////////////////////////////////

  public void AniCheck()
  {
        if(isFacingRight == true && isCrouching == false)
        {
            Animator.SetFloat("Movement", Input.x);
        }
        else if(isFacingRight == true && isCrouching == false)
        {
            Animator.SetFloat("Movement", -Input.x);
        }

        Animator.SetBool("IsCrouching", isCrouching);

        
  }
   

/////////////////////////////////////////////// JUMP FUNCTION START ////////////////////////////////
  public void Jumping()
  {
    if(Input.y > 0.4 && isGrounded == true)
      {
        jumpSquat = true;
              
      }

    if(jumpSquat == true && jsFrame != 0)
        {
          jsFrame -= 1;
        }
      else if (jsFrame == 0) 
        {
          Animator.SetTrigger("JumpForward");
          rb.velocity = new Vector2(rb.velocity.x, regJump);
          jumpSquat = false; 
          jsFrame = jsFrameStart;
          isGrounded = false; 

          ////////Forward jumping////////
          if((isFacingRight == true && Input.x > 0.5f) || (isFacingRight == false && Input.x < -0.5f))
          {
            if(transform.localScale.x > 0)
            {
              rb.AddForce(Vector2.right * (regJump/2), ForceMode2D.Impulse);
            }
            else if(transform.localScale.x < 0)
            {
              rb.AddForce(-Vector2.right * (regJump/2), ForceMode2D.Impulse);
            }
              
            
          }
          ////////backwards jumping///////////
          else if((isFacingRight == true && Input.x < -0.5) || (isFacingRight == false && Input.x > 0.5))
          {
            if(transform.localScale.x < 0)
            {
              rb.AddForce(Vector2.right * (regJump/2), ForceMode2D.Impulse);
            }
            else if(transform.localScale.x > 0)
            {
              rb.AddForce(-Vector2.right * (regJump/2), ForceMode2D.Impulse);
            }
              
          }
        }

  }
   
/////////////////////////////////////////////// JUMP FUNCTION END ////////////////////////////////



//////////////////ATTACKING/////////////////////////
    
    //Attacking will be done on the players respective character script. Like for example if they was playing oni then most of their attacking will come from the oni script inside of this script(if that makes sense)
    
 

///////////////// Input History ///////////////////////////


//////////////////////////////////////////////////////  Health & Meterchecking    //////////////////////////

  public void GetHealt_N_Meter()
  {
      hp = GameObject.FindObjectOfType<HealthBar>();

      if(hp != null)
      {
       if(yourLayer.value == 64)
       {       
        basehp = hp.p1Health.GetComponent<Image>().rectTransform.rect.width;
        Meter = hp.p1Meter.GetComponent<Image>().rectTransform.rect.width;
       }
       else
       {
        basehp = hp.p2Health.GetComponent<Image>().rectTransform.rect.width;
        Meter = hp.p2Meter.GetComponent<Image>().rectTransform.rect.width;
       }
      }
      else
      {
        Debug.Log("Damn it");
      }
  }



  public void Health_n_Meter()
  {

    if(yourLayer.value == 64)
    {
       if(hp.isRunning == true)
       {
         hp.p1HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, basehp);
       }
       else
       {
        basehp = hp.basehp; 
        hp.p1HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp.basehp);
       }
       hp.p1Meter.GetComponent<Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Meter);
    }
    else
    {
       if(hp.isRunning == true)
       {
         hp.p2HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, basehp);
       }
       else
       {
        basehp = hp.basehp2;
        hp.p2HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp.basehp2);
       }
       hp.p2Meter.GetComponent<Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Meter);
    }


  }

//////////////////////////////////////////////////// BLOCKING     ////////////////////////////////
  public void isBlocking()
  {
    if((isFacingRight == true && Input.x < 0 && isCrouching == false && isGrounded == true) || (isFacingRight == false && Input.x > 0 && isCrouching == false && isGrounded == true))
    {
      StandBlock = true;
      CrouchBlock = false;
    }
    else if((isFacingRight == true && Input.x < 0 && isCrouching == true) || (isFacingRight == false && Input.x > 0 && isCrouching == true))
    {
      CrouchBlock = true;
      StandBlock = false;
    }
    else
    {
      StandBlock = false;
      CrouchBlock = false; 
    }

  }

//////////////////////////////////////////////////  CHARACTER FLIP FUNCTION   ////////////////////////////////////////////////////
  public void characterFlipFunction()
  {
     float distance = 2f; // Small distance to check for collisions
      // Check for collisions on the right side
      
      RaycastHit2D rightSideDetector = Physics2D.BoxCast(rb.position + new Vector2(2, 0), boxSize, 0, Vector2.right, distance, opsLayer);
      
      // Check for collisions on the left side
      
      RaycastHit2D leftSideDetector = Physics2D.BoxCast(rb.position + new Vector2(-2, 0), boxSize, 0, Vector2.left, distance, opsLayer);
      
      // Flip the GameObject based on the collision

      var playerLocalScale = transform.localScale;

      if ((rightSideDetector.collider != null) && (rightSideDetector.collider.gameObject != this.gameObject) &&  this.transform.gameObject.layer != yourLayer && (!rightSideDetector.collider.gameObject.CompareTag("Hitbox")))
    {
        isFacingRight = true;
        
        if(playerLocalScale.x < 0)
        {
          playerLocalScale.x = -playerLocalScale.x; // Flip to face right
        }
        
        transform.localScale = playerLocalScale;
        
    }
    
    else if ((leftSideDetector.collider != null) && (leftSideDetector.collider.gameObject != this.gameObject) && (!leftSideDetector.collider.gameObject.CompareTag("Hitbox")) && this.transform.gameObject.layer != yourLayer)
      {
        isFacingRight = false;
        
        if(playerLocalScale.x > 0)
        {
          playerLocalScale.x = -playerLocalScale.x; // Flip to face left
        }
        
        transform.localScale = playerLocalScale;
        
      }
  }

}

