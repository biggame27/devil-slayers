using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class TestPage : MonoBehaviour
{
    public Text japchar;
    public Text timing;
    public Canvas deathUI;
    public Canvas UI;
    Gold gold;
    
    int maxTime = 20;
    float deltaTime = 0;
    int timer;
    bool runDown = false;
    bool showingAnswers = false;

    int playerGuess;
    int correctChoice;
    public GameObject[] choices;
    private string input;
    Dictionary<string, string> myMap = new Dictionary<string, string>();

    void Start()
    {
        gold = GameObject.Find("Player").transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Gold>();
        GetComponent<Canvas>().enabled = false;
        timer = maxTime;
        myMap["あ"] = "a";
        myMap["い"] = "i";
        myMap["う"] = "u";
        myMap["え"] = "e";
        myMap["お"] = "o";

        myMap["か"] = "ka";
        myMap["き"] = "ki";
        myMap["く"] = "ku";
        myMap["け"] = "ke";
        myMap["こ"] = "ko";
        
        myMap["さ"] = "sa";
        myMap["し"] = "shi";
        myMap["す"] = "su";
        myMap["せ"] = "se";
        myMap["そ"] = "so";

        myMap["た"] = "ta";
        myMap["ち"] = "chi";
        myMap["つ"] = "tsu";
        myMap["て"] = "te";
        myMap["と"] = "to";

        myMap["な"] = "na";
        myMap["に"] = "ni";
        myMap["ぬ"] = "nu";
        myMap["ね"] = "ne";
        myMap["の"] = "no";

        myMap["は"] = "ha";
        myMap["ひ"] = "hi";
        myMap["ふ"] = "hu";
        myMap["へ"] = "he";
        myMap["ほ"] = "ho";

        myMap["ま"] = "ma";
        myMap["み"] = "mi";
        myMap["む"] = "mu";
        myMap["め"] = "me";
        myMap["も"] = "mo";

        myMap["ら"] = "ra";
        myMap["り"] = "ri";
        myMap["る"] = "ru";
        myMap["れ"] = "re";
        myMap["ろ"] = "ro";

        myMap["わ"] = "wa";
        myMap["を"] = "wo";
        myMap["ん"] = "n";
        myMap["や"] = "ya";
        myMap["ゆ"] = "yu";
        myMap["よ"] = "yo";
        /*
        myMap["が"] = "ga";
        myMap["ぎ"] = "gi";
        myMap["ぐ"] = "gu";
        myMap["げ"] = "ge";
        myMap["ご"] = "go";
        */
        //ChangeText();
    }

    public void ChangeText()
    {
        runDown = true;
        Time.timeScale = 0;
        UI.enabled = false;
        GetComponent<Canvas>().enabled = true;
        timer = maxTime;
        //StartCoroutine(ClockTick());
        List<string> val = myMap.Keys.ToList();
        int num = Random.Range(0,45);
        japchar.text = val[num];
        correctChoice = Random.Range(0,3);
        choices[correctChoice].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = myMap[val[num]];
        for(int i = 0; i< 4; i++)
        {
            if(i != correctChoice)
            {
                bool check = false;
                int randomChoice = Random.Range(0,45);
                while(!check)
                {
                    check = true;
                    randomChoice = Random.Range(0,45);
                    for(int j = i; j >= 0; j--)
                    {
                        if(myMap[val[randomChoice]] == choices[j].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text || randomChoice == num)
                            check = false;
                    }
                }
                choices[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = myMap[val[randomChoice]];
            }
        }
    }
    /*
    private IEnumerator ClockTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            timing.text = ""+timer; 
            if(timer <= 0)
            {
                GetComponent<Canvas>().enabled = false;
            }
        }
    }
    */
    private void Update()
    {
        if(runDown)
        {
            deltaTime += Time.unscaledDeltaTime;
            if(deltaTime >= 1)
            {
                deltaTime = 0;
                timer--;
                timing.text = ""+timer; 
            }
            if(timer <= 0)
            {
                CheckCorrect(5);
            }
        }

        if(showingAnswers)
        {
            deltaTime += Time.unscaledDeltaTime;
            if(deltaTime >= 1.5f)
                HideAnswers();
        }
        
    }
    /*
    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }
    */
    public void CheckCorrect(int ans)
    {
        runDown =false;
        if(ans != 5)
        {
            playerGuess =ans;
            //StopCoroutine(ClockTick());
            if(ans == correctChoice)
            {
                gold.AddGold(5);
            }
            else
            {
                choices[ans].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = true;
            }
        }
        
        choices[correctChoice].transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
        showingAnswers = true;
        //StartCoroutine(WaitOneSecond());
        

    }

    public void HideAnswers()
    {
        choices[playerGuess].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        choices[correctChoice].transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
        GetComponent<Canvas>().enabled = false;
        UI.enabled = true;
        Time.timeScale = 1;
    }
}
