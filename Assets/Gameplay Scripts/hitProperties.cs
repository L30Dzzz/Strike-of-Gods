using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitProperties : MonoBehaviour
{
   PlayerMovement Player;
   private LayerMask yourLayer_;
   private LayerMask opsLayer_;

   
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponentInParent<PlayerMovement>();
        yourLayer_ = Player.yourLayer;
        opsLayer_ = Player.opsLayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("I hit something");
    }
    
}
