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

    public bool isRunning = true; //I will turn this into a bool later
    public int rounds; 
    private int Point_;

    public GameObject p1Health;
    public GameObject p1Meter; 
    public GameObject P1WinScreen;
    public GameObject P1Icon;
    public Image P1PlayerIcon;
    public int P1Score;
    public Image p1HealthBar; 

    public GameObject p2Health; 
    public GameObject p2Meter; 
    public GameObject P2WinScreen;
    public GameObject P2Icon;
    public Image P2PlayerIcon;
    public int P2Score;
    public Image p2HealthBar; 
    
    public Sprite pointIcon;
    public GameObject KoScreen;
    
    public GameObject MenuScreen;
    public GameObject[] P1Points;
    public GameObject[] P2Points; 

    public TextMeshProUGUI timerText;

    public Color winColor; 
    
    
    
    void Awake()
    {   
        //sets the time to the max time and start timer loop 
        currentTime = MaxTime;
        timerText.text = MaxTime.ToString();
        StartCoroutine(gameTimer());
        getPoints();
        
        p1HealthBar = p1Health.GetComponent<Image>();
        p2HealthBar = p2Health.GetComponent<Image>();

        
     }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRunning == true)
        {
        basehp = (int)p1HealthBar.rectTransform.rect.width;
        basehp2 = (int)p2HealthBar.rectTransform.rect.width;
        }
    
        if(basehp <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen, P2Points, P2Score));
            StopCoroutine(gameTimer());
            isRunning = false;
        }
        else if(basehp2 <= 0)
        {
            StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen, P1Points, P1Score));
            StopCoroutine(gameTimer());
            isRunning = false;
        }


    }

     private void getPoints()
     {
        int points = rounds--;

        

        for(int x = 0; x < points; x++)
        {
            P1Points[x].SetActive(true);
            P2Points[x].SetActive(true);
        }

        Point_ = points;
     }

     private void RestartRound()
     {
        basehp = 689;
        basehp2 = 689;

        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        if(players[0].yourLayer.value == 64)
        {
            players[0].transform.position = new Vector3(-11,1,0);
            players[1].transform.position = new Vector3(-1,1,0);
            
            players[0].isFacingRight = true;
            players[1].isFacingRight = false;
        }
        else
        {
            players[0].transform.position = new Vector3(-1,1,0);
            players[1].transform.position = new Vector3(-11,1,0);

            players[0].isFacingRight = false;
            players[1].isFacingRight = true;
        }
        

            
        p1HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, basehp);
        p2HealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, basehp2);

   
        players[0].Animator.SetBool("is Hurt", false);
        players[1].Animator.SetBool("is Hurt", false);
        
     }

     private IEnumerator startRound() // add 2 GameObject coroutines in here
     {
        currentTime = MaxTime;
        timerText.text = currentTime.ToString();
        yield return new WaitForSeconds(2);
        StartCoroutine(gameTimer());
        isRunning = true;
     }

     
     private IEnumerator gameCondition(GameObject Ko, GameObject WinScreen, GameObject Menu, GameObject[] Point, int Score)
    {
       
       
     if(isRunning == true)
     {
       Ko.SetActive(true);

       yield return new WaitForSeconds(2);

       Ko.SetActive(false);
       WinScreen.SetActive(true);

       yield return new WaitForSeconds(2);

       Image Pimage = Point[Score].GetComponent<Image>();


        WinScreen.SetActive(false);
        
            if(Score < Point_--)
            {
            
            Pimage.color = winColor; 
            
            yield return new WaitForSeconds(2);

                if(basehp <= 0)
                {
                P2Score++;
                }
                else if(basehp2 <= 0)
                {
                P1Score++;
                }

                RestartRound();

                StartCoroutine(startRound());
            }
            else
            {
            
                Pimage.color = winColor; 
                Menu.SetActive(true);
            }
            
     }

    }

    private IEnumerator gameTimer()
    {
        while(currentTime >= 0 && ( basehp >= 0 && basehp2 >= 0))
        {
            
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.text = currentTime.ToString();
            
        }

        if(currentTime < 1)
        {
            if(basehp > basehp2) // if player one wins 
            {
                StartCoroutine(gameCondition(KoScreen, P1WinScreen, MenuScreen, P1Points, P1Score));
                isRunning = false;
                Debug.Log("Player one wins ");
            }
            else if(basehp2 > basehp) // if player 2 wins 
            {
                StartCoroutine(gameCondition(KoScreen, P2WinScreen, MenuScreen, P2Points, P2Score));
                isRunning = false;
                Debug.Log("Player 2 wins");
            }

            StopCoroutine(gameTimer());
            Debug.Log("Times up");

        }

    }
    
}
