using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSender : MonoBehaviour
{
    public Image hiragana;
    public Image katakana;
    public Image easyMode;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Hiragana", 1);
        hiragana.enabled = true;
        PlayerPrefs.SetInt("Katakana", 0);
        katakana.enabled = false;
        PlayerPrefs.SetInt("EasyMode", 1);
        easyMode.enabled = true;
    }

    public void HiraganaClick()
    {
        if(PlayerPrefs.GetInt("Hiragana") == 1)
        {
            PlayerPrefs.SetInt("Hiragana", 0);
            hiragana.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("Hiragana", 1);
            hiragana.enabled = true;
        }
    }

    public void KatakanaClick()
    {
        if(PlayerPrefs.GetInt("Katakana") == 1)
        {
            PlayerPrefs.SetInt("Katakana", 0);
            katakana.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("Katakana", 1);
            katakana.enabled = true;
        }
    }

    public void EasyModeClick()
    {
        if(PlayerPrefs.GetInt("EasyMode") == 1)
        {
            PlayerPrefs.SetInt("EasyMode", 0);
            easyMode.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("EasyMode", 1);
            easyMode.enabled = true;
        }
    }

}
