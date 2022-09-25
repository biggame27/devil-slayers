using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowTarget : MonoBehaviour
{
    public PlayerController player;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if(player.GetCanBuild())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            float timesX = Mathf.Round(pos.x/0.16f);
            float timesY = Mathf.Round(pos.y/0.16f);
            Vector2 newCoords = new Vector2(0.16f * timesX, 0.16f * timesY);
            transform.position = new Vector3(newCoords.x, newCoords.y, 0);
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
        
    }
}
