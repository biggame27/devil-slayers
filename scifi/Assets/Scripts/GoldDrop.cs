using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    TestPage testPage;
    public Collider2D playerCollider;

    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col == playerCollider)
        {
            GetComponent<Collider2D>().enabled = false;
            GameObject.Find("Player").transform.GetChild(5).GetComponent<TestPage>().ChangeText();
            Destroy(gameObject);
        }
        
    }
}
