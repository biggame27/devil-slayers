using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] 
    Transform player;
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime);
    }
}
