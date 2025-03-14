using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int basehp;
    private int basehp2;

    public int isRunning = 1; //I will turn this into a bool later

    public GameObject p1Health;
    public GameObject p2Health; 
    public GameObject p1Meter; 
    public GameObject p2Meter; 
    public GameObject KoScreen;
    public GameObject P1WinScreen;
    public GameObject P2WinScreen;
    public GameObject MenuScreen;
    
    
    
    void Awake()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        basehp = (int)p1Health.GetComponent<Image>().rectTransform.rect.width;
        basehp2 = (int)p2Health.GetComponent<Image>().rectTransform.rect.width;
    
    
        if(basehp <= 0)
        {
           StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen));
           isRunning--;
        }
        else if(basehp2 <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen));
            isRunning--;
        }


    }

    
     private IEnumerator gameCondition(GameObject Ko, GameObject WinScreen, GameObject Menu)
    {
       
       
     if(isRunning >= 1)
     {
       Ko.SetActive(true);

       yield return new WaitForSeconds(2);

       Ko.SetActive(false);
       WinScreen.SetActive(true);

       yield return new WaitForSeconds(2);

       WinScreen.SetActive(false);
       Menu.SetActive(true);

        
       }
    }
}
