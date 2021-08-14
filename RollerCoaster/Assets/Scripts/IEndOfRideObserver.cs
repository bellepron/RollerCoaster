using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEndOfRideObserver
{
    void GoNextRide_IfExists();
}