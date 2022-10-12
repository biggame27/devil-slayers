using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDrop : MonoBehaviour
{
    TestPage testPage;
    public Collider2D playerCollider;
    private bool pickUpAllowed;
    private int time;
    public int maxTime;

    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
        StartCoroutine(TimeLimit());
    }

    void Picking()
    {
        if(pickUpAllowed)
            PickUp();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col == playerCollider)
        {
            pickUpAllowed = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col == playerCollider)
        {
            pickUpAllowed = false;
        }
        
    }

    public void PickUp()
    {
        GetComponent<Collider2D>().enabled = false;
        GameObject.Find("Player").transform.GetChild(5).GetComponent<TestPage>().ChangeText();
        Destroy(gameObject);
    }

    public bool GetAllowed()
    {
        return pickUpAllowed;
    }

    IEnumerator TimeLimit()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            time++;
            if(time >= maxTime)
                Destroy(gameObject);
        }

    }
}
