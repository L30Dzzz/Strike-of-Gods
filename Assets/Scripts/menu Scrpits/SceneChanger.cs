using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{

   public GameManager gameController; 
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoader(string scenechanger)
    {
        if(gameController != null)
        {
            if((gameController.gameIsPaused == true))
            {
                gameController.gameIsPaused = false;
            }
        }
        SceneManager.LoadScene(scenechanger);
    }

    public void ExitGame()
    {
      Application.Quit();
    }
}
