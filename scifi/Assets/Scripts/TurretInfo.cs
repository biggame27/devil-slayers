using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretInfo : MonoBehaviour
{
    Image image;
    TurretLevelController controller;
    [SerializeField]
    private Image turretImage;
    [SerializeField]
    private Text cost;
    [SerializeField]
    private TMP_Text damage;
    [SerializeField]
    private TMP_Text potentialDamage;
    void Start()
    {
        gameObject.SetActive(false);
        //get the turret (add turret upgradeable stats)
        //make upgrade edits
    }

    public bool CheckEnabled()
    {
        return gameObject.activeSelf;
    }

    public void SetController(TurretLevelController _controller)
    {
        controller = _controller;
        if(controller.GetCost() == 69)
        {
            cost.text = "MAX";
            potentialDamage.enabled = false;
            damage.text = "Damage: " + controller.GetDamage();
        }
        else
        {
            cost.text = "Upgrade: "+controller.GetCost();
            string futureDamageText = "";
            if(controller.GetFutureDamage() != 69)
            {
                potentialDamage.enabled = true;
                futureDamageText = "+"+(controller.GetFutureDamage()-controller.GetDamage());
                potentialDamage.text = futureDamageText;
            }
            damage.text = "Damage: " + controller.GetDamage();
        }
        GetSprite();
    }

    public void LevelUp()
    {
        controller.LevelUp();
        if(controller.GetCost() == 69)
        {
            cost.text = "MAX";
            potentialDamage.enabled = false;
            damage.text = "Damage: " + controller.GetDamage();
        }
        else
        {
            cost.text = "Upgrade: "+controller.GetCost();
            string futureDamageText = "";
            if(controller.GetFutureDamage() != 69)
            {
                potentialDamage.enabled = true;
                futureDamageText = "+"+(controller.GetFutureDamage()-controller.GetDamage());
                potentialDamage.text = futureDamageText;
            }
            damage.text = "Damage: " + controller.GetDamage();
        }
        GetSprite();
    }

    public void Sell()
    {
        gameObject.SetActive(false);
        controller.Sell();
    }

    void GetSprite()
    {
        turretImage.sprite = controller.gameObject.transform.GetChild(controller.GetLevel()-1).GetComponent<SpriteRenderer>().sprite;
    }
}
