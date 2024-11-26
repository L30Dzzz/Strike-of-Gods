using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
[CreateAssetMenu(fileName = "DummyMoveset", menuName = "ScriptableObjects/DummyMoveSet", order = 2)]
public class DummyMoveset : ScriptableObject
{
    
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.Input.y == 0 && player.Input.x == 0)
        {
            Debug.Log("PLayer is at netural");
        }
        else
        {
            Debug.Log("Player is moving");
        }


    }

    public void LightAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Light attack had been pressed");
    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Medium attack had been pressed");
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Heavy attack had been pressed");
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Special Attack had been pressed");
    }
}
