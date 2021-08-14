using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<IEndOfRideObserver> endOfRideObservers;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        endOfRideObservers = new List<IEndOfRideObserver>();
    }

    #region Observer Funcs

    public void AddEndOfRideObserver(IEndOfRideObserver observer)
    {
        endOfRideObservers.Add(observer);
    }

    public void RemoveEndOfRideObserver(IEndOfRideObserver observer)
    {
        endOfRideObservers.Remove(observer);
    }

    public void NotifyGameStartObservers()
    {
        foreach (IEndOfRideObserver observer in endOfRideObservers.ToArray())
        {
            if (endOfRideObservers.Contains(observer))
                observer.GoNextRide_IfExists();
        }
    }

    #endregion
}
