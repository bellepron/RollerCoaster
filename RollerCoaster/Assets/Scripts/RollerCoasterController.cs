using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class RollerCoasterController : MonoBehaviour
{
    public enum States { Idle, Ride, Finish, Drop }
    public States currentState;

    public int length;
    [SerializeField] GameObject rollerCoasterModule;
    List<GameObject> modules;
    GameObject firstVagoon;
    Vector3 firstVagoonsFirstPosition;
    [SerializeField] int rollerCoaster_Speed = 60;

    #region States
    public void UpdateStates()
    {
        switch (currentState)
        {
            case States.Idle:
                Idle();
                break;
            case States.Ride:
                Ride();
                break;
            case States.Finish:
                Finish();
                break;
            case States.Drop:
                Drop();
                break;
        }
    }

    private void Idle()
    {

    }
    private void Ride()
    {
        firstVagoonsFirstPosition = firstVagoon.transform.position;

        Globals.rollerCoasterSpeed = rollerCoaster_Speed;

        // StartCoroutine(FinishTheRide(FindObjectOfType<Creator>().passengers));
        currentState = States.Finish;
        UpdateStates();
    }
    private void Finish()
    {
        StartCoroutine(FinishTheRide(FindObjectOfType<Creator>().passengers));
    }

    private void Drop()
    {
        StartCoroutine(GetOffTheRollerCoaster(FindObjectOfType<Creator>().passengers));
    }

    #endregion

    #region Create Roller Coaster Vagoons

    public void ArrangeSize()
    {
        transform.localScale = new Vector3(2, 0.5f, length);
        modules = new List<GameObject>();

        for (int i = 0; i < length; i++)
        {
            GameObject vagoon = Instantiate(rollerCoasterModule, transform.position + new Vector3(i * 1, 0, 0), Quaternion.identity) as GameObject;
            modules.Add(vagoon);

            vagoon.AddComponent<Follower_RollerCoaster>();
            vagoon.GetComponent<Follower_RollerCoaster>().distanceTravelled = -i * 1;

            if (i == 0)
                firstVagoon = vagoon;
        }

        firstVagoonsFirstPosition = firstVagoon.transform.position; // Init
    }
    public List<GameObject> GetSeats()
    {
        return modules;
    }

    #endregion

    public void StartRide()
    {
        currentState = RollerCoasterController.States.Ride;
        UpdateStates();
    }

    #region Finish State

    IEnumerator FinishTheRide(List<GameObject> passengers)
    {
        yield return new WaitForSeconds(1f); // Do not detect first second (for trigger)
        bool _continue = true;

        while (_continue)
        {
            if (Vector3.Distance(firstVagoonsFirstPosition, firstVagoon.transform.position) < 10f) // Slow down
                Globals.rollerCoasterSpeed = 10;                      // TO DO: do it gently

            if (Vector3.Distance(firstVagoonsFirstPosition, firstVagoon.transform.position) < 0.6f) // Stop (if frame can catch)
            {
                Globals.rollerCoasterSpeed = 0;

                currentState = States.Drop;
                UpdateStates();

                _continue = false;
            }

            yield return null; // Control every frame to catch the roller coaster aproximately same position and stop;
        }
    }

    #endregion

    #region Drop State

    IEnumerator GetOffTheRollerCoaster(List<GameObject> passengers)
    {
        yield return new WaitForSeconds(0.1f); // Passengers unbuckle their seat belts (not added)

        for (int j = 0; j < Calculator.Instance.dailyRideEarnings[Globals.dailyWorkCount]; j++) // send passengers to the back of the queue
        {
            Vector3 endOfTheQueue = new Vector3(j, 0, -2 * (Calculator.Instance.N - 1)); // TO DO: fix&arrange

            passengers[j].transform.DOMove(endOfTheQueue, 1);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.parent = null;

            Creator.Instance.group.Add(passenger);    // Add passengers to all.
        }

        GameManager.Instance.NotifyGameStartObservers();
    }

    #endregion
}