using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int basehp;
    private int basehp2;
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
        
        
        basehp = (int)p1Health.GetComponent<Image>().rectTransform.rect.width;
        basehp2 = (int)p2Health.GetComponent<Image>().rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(basehp <= 0)
        {
           StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen));
        }
        else if(basehp2 <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen));
        }


    }

    
     private IEnumerator gameCondition(GameObject Ko, GameObject WinScreen, GameObject Menu)
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
