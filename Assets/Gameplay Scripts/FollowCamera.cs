using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;
    public float smoothTime = .5f;

    private Vector3 velocity;

    void LateUpdate()
    {

        if (players.Count ==0)
        return;

        Vector3 centerPoint = GetCenterPoint();
        
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++) 
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    }

    


}
