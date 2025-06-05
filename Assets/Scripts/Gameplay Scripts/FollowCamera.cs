using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public PlayerMovement[] players;
    public Vector3 offset;
    public float smoothTime = .5f;

    private Vector3 velocity;

    void LateUpdate()
    {

        if (players.Length == 0)
        {
            findPlayers();
        }
        

        Vector3 centerPoint = GetCenterPoint();
        
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (players.Length == 1)
        {
            return players[0].transform.position;
        }

        var bounds = new Bounds(players[0].transform.position, Vector3.zero);
        for (int i = 0; i < players.Length; i++) 
        {
            bounds.Encapsulate(players[i].transform.position);
        }
        return bounds.center;
    }

    public void findPlayers()
    {
       players = FindObjectsOfType<PlayerMovement>();
    }

    


}
