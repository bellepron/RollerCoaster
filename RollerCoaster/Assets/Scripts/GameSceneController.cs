using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public Day_Info day_Info;

    void Start()
    {
        CreateRollerCoaster();
        StartNew();
    }

    public void StartNew()
    {
        Calculator.Instance.StartNew(day_Info);
    }

    void CreateRollerCoaster()
    {
        RollerCoasterController rollerCoasterController = FindObjectOfType<RollerCoasterController>();
        rollerCoasterController.length = day_Info.L;
        rollerCoasterController.ArrangeSize();
    }
}
