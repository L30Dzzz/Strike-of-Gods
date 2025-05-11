using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
  
    // Start is called before the first frame update
    void Awake()
    {
        Player = GetComponent<PlayerMovement>();
        if(Player != null)
        {
         StartCoroutine(FindHp());
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IconChanger();
    }

    IEnumerator FindHp()
    {
        yield return new WaitForSeconds(1/60); 
        
        if(Player.yourLayer.value == 64)
        {
            HealthBar = Player.hp.p1Health.GetComponent<Image>();
            Icon = Player.hp.P1PlayerIcon;
            IconBg = Player.hp.P1Icon.GetComponent<Image>();
            Debug.Log("Found Player 1 hp");
        }
        else
        {
            HealthBar = Player.hp.p2Health.GetComponent<Image>();
            Icon = Player.hp.P2PlayerIcon;
            IconBg = Player.hp.P2Icon.GetComponent<Image>();
            Debug.Log("Find Player 2 hp");
        }
        HealthBar.color = HpColor;
        IconBg.color = IconColor; 

        Debug.Log("Loop is done");

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
          Debug.Log("Winning");
        }
    }
    
}
