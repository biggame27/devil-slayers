using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;
    private Image picture;

    public void Start()
    {
        GetComponent<Image>().sprite = images[0];
    }

    public void SwapFire()
    {
        Debug.Log("bruh");
        GetComponent<Image>().sprite = images[0];   
    }

    public void SwapBuild()
    {
        GetComponent<Image>().sprite = images[1];
    }
}
