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
            if((isHigh == true && P2.StandBlock == false) || (isLow == true && P2.CrouchBlock == false) || (isMid == true && (P2.StandBlock == false || P2.CrouchBlock == false)))
            {  
              if(P2.basehp >= 0)
              {
              P2.basehp -= dmg;
              Player.basehp += dmg; 
              }
             
             
             
             if(Player.Meter != 302 && P2.basehp >= 0)
             {
               Player.Meter += meterGain;
             }
             
            }
             
             /*
              Debug.Log(Player.basehp);
              Debug.Log(P2.basehp);
              */
         }


       }
    }
    
}
