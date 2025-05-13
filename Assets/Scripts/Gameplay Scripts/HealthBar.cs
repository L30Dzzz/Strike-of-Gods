using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public int basehp;
    public int basehp2;

    public int currentTime;
    public int MaxTime;

    public int isRunning = 1; //I will turn this into a bool later
    public int rounds; 

    public GameObject p1Health;
    public GameObject p1Meter; 
    public GameObject P1WinScreen;
    public GameObject P1Icon;
    public Image P1PlayerIcon;
    private int P1Score;
    
    public GameObject p2Health; 
    public GameObject p2Meter; 
    public GameObject P2WinScreen;
    public GameObject P2Icon;
    public Image P2PlayerIcon;
    private int P2Score;
    
    public Sprite pointIcon;
    public GameObject KoScreen;
    
    public GameObject MenuScreen;
    public GameObject[] P1Points;
    public GameObject[] P2Points; 

    public TextMeshProUGUI timerText;
    
    
    
    void Awake()
    {   
        //sets the time to the max time and start timer loop 
        currentTime = MaxTime;
        timerText.text = MaxTime.ToString();
        StartCoroutine(gameTimer());

        
    }

    // Update is called once per frame
    void Update()
    {
        basehp = (int)p1Health.GetComponent<Image>().rectTransform.rect.width;
        basehp2 = (int)p2Health.GetComponent<Image>().rectTransform.rect.width;
    
    
        if(basehp <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen));
            StopCoroutine(gameTimer());
            isRunning--;
        }
        else if(basehp2 <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen));
            StopCoroutine(gameTimer());
            isRunning--;
        }


    }

     private void getPoints(int rounds)
     {
        int points = rounds--;
        P1Points = new GameObject[points];
        P2Points = new GameObject[points];
        P1Score = points;
        P2Score = points;

        
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

    private IEnumerator gameTimer()
    {
        while(currentTime >= 0 && ( basehp >= 0 && basehp2 >= 0))
        {
            
            yield return new WaitForSeconds(1);
            timerText.text = currentTime.ToString();
            currentTime--;
        }

        if(currentTime < 1)
        {
            if(basehp > basehp2) // if player one wins 
            {
                StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen));
                Debug.Log("Player one wins ");
            }
            else if(basehp2 > basehp) // if player 2 wins 
            {
                StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen));
                Debug.Log("Player 2 wins");
            }

            Debug.Log("Times up");

        }

    }
}
