using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour, IEndOfRideObserver
{
    public static GameManager Instance;
    private List<IEndOfRideObserver> endOfRideObservers;

    [SerializeField] GameObject startButton;
    [SerializeField] TextMeshProUGUI dirhamText;
    int dirham = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        endOfRideObservers = new List<IEndOfRideObserver>();
        AddEndOfRideObserver(this);

        // Time.timeScale = 3f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartButton();
    }

    public void StartButton()
    {
        startButton.SetActive(false);
        Calculator.Instance.StartSimulation();
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

    public void GoNextRide_IfExists()
    {
        dirham += Calculator.Instance.dailyRideEarnings[Globals.dailyWorkCount];
        dirhamText.text = dirham.ToString();
    }
}
