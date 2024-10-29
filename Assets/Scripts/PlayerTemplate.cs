using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerTemplate", order = 1)]
public class PlayerTemplate : ScriptableObject
{

    private Animator animation;
    public LayerMask yourLayer;
    public LayerMask opsLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
