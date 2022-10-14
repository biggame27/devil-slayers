using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingButton : MonoBehaviour
{
    public Color targetColor = new Color(1, 1, 1, 0);
    public Color targetColor2 = new Color(1, 1, 1, 1);
    bool fading = true;
    Image spriteToFade;
    void Start()
    {
        spriteToFade = gameObject.GetComponent<Image>();
        StartCoroutine(LerpFunction(targetColor, 2));
        
        
    }
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        while(true)
        {
            float time = 0;
            if(fading)
            {
                Color startValue = spriteToFade.color;
                while (time < duration)
                {
                    spriteToFade.color = Color.Lerp(startValue, targetColor, time / duration);
                    time += Time.deltaTime;
                    yield return null;
                }
                spriteToFade.color = endValue;
                fading = false;
            }
            else
            {
                Color startValue = spriteToFade.color;
                while (time < duration)
                {
                    spriteToFade.color = Color.Lerp(startValue, targetColor2, time / duration);
                    time += Time.deltaTime;
                    yield return null;
                }
                spriteToFade.color = targetColor2;
                fading = true;
            }
        }
        
        
    }
}
