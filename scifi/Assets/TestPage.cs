using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class TestPage : MonoBehaviour
{
    public Text japchar;
    public TMP_InputField inputUser;
    public Button btnClick;
    private string input;
    Dictionary<string, string> myMap = new Dictionary<string, string>();

    void Start()
    {
        btnClick.onClick.AddListener(GetInputOnClickHandler);
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
    }

    void ChangeText()
    {
        List<string> val = myMap.Keys.ToList();
        int num = Random.Range(0,45);
        japchar.text = val[num];
    }

    public void GetInputOnClickHandler()
    {
        input = inputUser.text;
    }
}
