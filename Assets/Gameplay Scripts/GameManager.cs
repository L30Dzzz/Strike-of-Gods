using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class GameManager : MonoBehaviour
{

    public static bool gameIsPaused;
    public GameObject PauseSystem;
    public GameObject player1;
    public GameObject player2;
   // public TextMeshProGUI roundEndText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

     void Update()
    {
            PauseGame();
    }

    public void Paused(InputAction.CallbackContext context)
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
