using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitProperties : MonoBehaviour
{
   PlayerMovement Player;
   private LayerMask yourLayer_;
   private LayerMask opsLayer_;
   private int layerAsLayerMask;
   
   public int dmg = 0;
   public int meterGain = 0;
   public int stunTime = 0;
   public int maxHitCount;

   public int currentHitCount; 


   public Vector2 KBackDirect; //Knock Back Direction
   public float KBackForce;  // Knock Back Force

   public bool isHigh;
   public bool isLow;
   public bool isMid;


   //public int meterLost = 0; 
    // Start is called before the first frame update
    void Awake()
    {
        Player = GetComponentInParent<PlayerMovement>();
        yourLayer_ = Player.yourLayer;
        opsLayer_ = Player.opsLayer;
        currentHitCount = maxHitCount;

    }

    // Update is called once per frame
    void OnEnable()
    {
        currentHitCount = maxHitCount;
    }
  

    void OnTriggerStay2D(Collider2D other)
    {
      
        if(currentHitCount > 0)
        {
        hitProp(other);  
        Debug.Log("Stay works");
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(currentHitCount > 0)
        {
        hitProp(other); 
        Debug.Log("Enter Works"); 
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(currentHitCount > 0)
        {
        hitProp(other);  
        Debug.Log("Exit works");
        } 
    }

    public void hitProp(Collider2D other)
    {
        layerAsLayerMask = (1 << other.gameObject.layer);
       
      
       if(layerAsLayerMask == opsLayer_.value)
       {
         PlayerMovement P2 = other.gameObject.GetComponent<PlayerMovement>();
         if(P2 != null)
         {

            Rigidbody2D P2Rb = P2.rb;  

            //Checks if the player is blocking high or low and it fits the attack properties of the attack
            if((isHigh == true && P2.StandBlock == false) || (isLow == true && P2.CrouchBlock == false) || (isMid == true && (P2.StandBlock == false && P2.CrouchBlock == false)))
            {  
              // If the player is not blocking 
              
              

              if(P2.basehp > 0)
              {
                P2.basehp -= dmg;
                Player.basehp += dmg;
                
                if(P2.isFacingRight == true)
                {
                 P2Rb.AddForce(new Vector2(-KBackDirect.x,KBackDirect.y) * KBackForce,ForceMode2D.Impulse);
                }
                else
                {
                  P2Rb.AddForce(KBackDirect * KBackForce,ForceMode2D.Impulse);
                }
                
                StartCoroutine(hitstun(stunTime, P2));
                
                P2.Animator.SetBool("is Hurt", true);
              }
              else
              {
                  P2.Animator.SetBool("is Hurt", true);
              }
            
             if(Player.Meter != 302 && P2.basehp >= 0)
             {
               Player.Meter += meterGain;
             }

              // Makes the player able to can their attack into another attack and cancel thier attacks into other attacks 
              P2.canAttack = false;
              Player.canCancel = true;


             
            }
             else // of the character is blocking
             {
              // chip damage
              float reducedDmg = dmg * (1/4);

              if(P2.isFacingRight == true)
                {
                 P2Rb.AddForce(new Vector2(-KBackDirect.x,KBackDirect.y) * (KBackForce * 0.75f),ForceMode2D.Impulse);
                }
                else
                {
                  P2Rb.AddForce(KBackDirect * (KBackForce * 0.75f),ForceMode2D.Impulse);
                }

              if(P2.Meter >= 0)
              {
                 P2.Meter -= reducedDmg;
              }
              else
              {
                P2.basehp -= reducedDmg;
              }


                P2.Animator.SetTrigger("is blocking");
             }
            
         }


       }

        currentHitCount--;

    }

    public IEnumerator hitstun(int stun, PlayerMovement p2)
    {
      float stuned = stun/60;

      p2.canRespond = false;
      
      yield return new WaitForSeconds(stuned);

      p2.canRespond = true;
      Debug.Log("You can attack now");

      p2.Animator.SetBool("is Hurt", false);
    }
    
}
