using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitProperties : MonoBehaviour
{
   PlayerMovement Player;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
