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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        layerAsLayerMask = (1 << other.gameObject.layer);
       
      
       if(layerAsLayerMask == opsLayer_.value)
       {
         PlayerMovement P2 = other.gameObject.GetComponent<PlayerMovement>();   
         if(P2 != null)
         {
            //Checks if the player is blocking high or low and it fits the attack properties of the attack
            if((isHigh == true && P2.StandBlock == false) || (isLow == true && P2.CrouchBlock == false) || (isMid == true && (P2.StandBlock == false && P2.CrouchBlock == false)))
            {  
              // If the player is not blocking 
              
              if(P2.basehp >= 0)
              {
                P2.basehp -= dmg;
                Player.basehp += dmg; 
                StartCoroutine(hitstun(stunTime, P2));
                  

              }
            
             if(Player.Meter != 302 && P2.basehp >= 0)
             {
               Player.Meter += meterGain;
             }
             
            }
             else // of the character is blocking
             {
              // chip damage
              float reducedDmg = dmg * (1/4);


              if(P2.Meter >= 0)
              {
                

                 P2.Meter -= reducedDmg;
              }
              else
              {
                P2.basehp -= reducedDmg;
              }



             }
             /*
              Debug.Log(Player.basehp);
              Debug.Log(P2.basehp);
              */
         }


       }
    }

    public IEnumerator hitstun(int stun, PlayerMovement p2)
    {
      float stuned = stun/60;

      p2.canRespond = false;
      
      yield return new WaitForSeconds(stuned);

      p2.canRespond = true;
      Debug.Log("You can attack now");
    }
    
}
