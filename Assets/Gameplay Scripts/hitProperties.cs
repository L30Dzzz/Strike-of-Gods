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
              P2.basehp -= dmg;
              Player.basehp += dmg; 

              Debug.Log(Player.basehp);
              Debug.Log(P2.basehp);
         }


       }
    }
    
}
