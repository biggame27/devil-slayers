using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo : MonoBehaviour
{
    Image image;
    TurretLevelController controller;
    [SerializeField]
    private Image turretImage;
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
        GetSprite();
    }

    public void LevelUp()
    {
        controller.LevelUp();
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
