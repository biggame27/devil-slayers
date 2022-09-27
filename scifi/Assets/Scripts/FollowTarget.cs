using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class FollowTarget : MonoBehaviour
{
    public PlayerController player;
    SpriteRenderer spriteRenderer;
    public Vector2 pos;
    public Vector2 newCoords;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if(player.GetCanBuild())
        {
            
            if(IsPointerOverUIObject())
            {
                spriteRenderer.enabled = false;
                return;
            }
            /*
            if(Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue() != pos))
                player.IsTouchingMouse();
            */
            pos = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            float timesX = Mathf.Round(pos.x/0.16f);
            float timesY = Mathf.Round(pos.y/0.16f);
            newCoords = new Vector2(0.16f * timesX, 0.16f * timesY);
            transform.position = new Vector3(newCoords.x, newCoords.y, 0);
            if(!player.IsTouchingMouse())
                spriteRenderer.enabled = true;
            else
                spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
        
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Touchscreen.current.primaryTouch.position.ReadValue().x, Touchscreen.current.primaryTouch.position.ReadValue().y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    
}
