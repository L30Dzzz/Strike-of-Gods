using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{

    private const int WinsRequired = 2;
    
    static public RoundTracker Instance { get; private set; }

    bool player1Wins;
    bool Player2Wins;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
