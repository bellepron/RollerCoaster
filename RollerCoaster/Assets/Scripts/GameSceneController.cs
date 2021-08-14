using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Day_Info day_Info;

    void Start()
    {
        CreateRollerCoaster();
    }

    void Update()
    {

    }

    void CreateRollerCoaster()
    {
        RollerCoasterController rollerCoasterController = FindObjectOfType<RollerCoasterController>();
        rollerCoasterController.length = day_Info.L;
        rollerCoasterController.ArrangeSize();
    }
}
