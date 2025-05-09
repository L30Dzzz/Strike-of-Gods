using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WuKongMoveSet : MonoBehaviour
{
   PlayerMovement Player;

   public Color wuColor; 

   Image HealthBar; 
   
    // Start is called before the first frame update
    void Awake()
    {
        Player = GetComponent<PlayerMovement>();
        if(Player != null)
        {
         if(Player.hp == null)
         {
            StartCoroutine(FindHp());
         }
         

         
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FindHp()
    {
        yield return new WaitForSeconds(1/60);
        HealthBar = Player.hp.p1Health.GetComponent<Image>();
        HealthBar.color = wuColor;
    }
}
