using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo : MonoBehaviour
{
    Image image;
    TurretLevelController controller;
    void Start()
    {
        gameObject.SetActive(false);
        //get the turret (add turret upgradeable stats)
        //make upgrade edits
    }

    public void OpenInfo()
    {
        gameObject.SetActive(true);
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }

    public void SetController(TurretLevelController _controller)
    {
        controller = _controller;
    }

    public void LevelUp()
    {
        controller.LevelUp();
    }

    public void Sell()
    {
        controller.Sell();
    }
}