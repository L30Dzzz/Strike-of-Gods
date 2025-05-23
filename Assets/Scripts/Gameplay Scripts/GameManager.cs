using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 


public class GameManager : MonoBehaviour
{

    public bool gameIsPaused;
    public GameObject PauseSystem;
    HealthBar hp; 
   // public TextMeshProGUI roundEndText;

    // Start is called before the first frame update
    void Awake()
    {
        hp = GameObject.Find("Canvas").GetComponent<HealthBar>();
    }

     void Update()
    {
            PauseGame();
    }

    public void Paused(InputAction.CallbackContext context)
    {
       if(hp.basehp >= 0 && hp.basehp2 >= 0)
       {
        gameIsPaused = !gameIsPaused; 
       }

       
    }

    public void UnPause()
    {
        gameIsPaused = !gameIsPaused;
    }
    
    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            PauseSystem.gameObject.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            PauseSystem.gameObject.SetActive(false);
        }
    }

  //  void RoundEnd()
   // {
        
   // }
}
