using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Creator : MonoBehaviour, IEndOfRideObserver
{
    [SerializeField] Calculator calculator;
    RollerCoasterController rollerCoasterController;

    [SerializeField] GameObject characterPrefab;
    public List<GameObject> group;
    // int dailyRideCount = 0;

    void Start()
    {
        GameManager.Instance.AddEndOfRideObserver(this);
    }

    public void Init_FromCalculator(List<int> Pi_list, int N)
    {
        rollerCoasterController = FindObjectOfType<RollerCoasterController>();
        group = new List<GameObject>();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < Pi_list[i]; j++)
                AddToList(Instantiate(characterPrefab, new Vector3(j * 1, 0, -i * 2f), Quaternion.identity));
        }

        // group = group.OrderBy(gameObject => gameObject.transform.position.z).ToList();
    }
    void AddToList(GameObject go)
    {
        group.Add(go);
    }


    #region Ride

    public void ReadyToGetIn(List<int> dailyRideEarnings)
    {
        List<GameObject> seats = rollerCoasterController.GetSeats();

        // Debug.Log(dailyRideEarnings[0]);
        GetIn(dailyRideEarnings, seats, Globals.dailyWorkCount);
    }

    public void GetIn(List<int> dailyRideEarnings, List<GameObject> seats, int queue)
    {
        List<GameObject> passengers = new List<GameObject>(); // Remove from all, add back when queue up.

        for (int i = 0; i < dailyRideEarnings[queue]; i++)
        {
            group[i].transform.DOMove(seats[i].transform.position, 1.2f + i * 0.05f);
            group[i].transform.parent = seats[i].transform;

            passengers.Add(group[i]);
        }
        for (int i = dailyRideEarnings[queue]; i < group.Count; i++)
        {
            group[i].transform.DOMoveZ(group[i].transform.position.z + 2 * Calculator.Instance.howManyGroupsInRide[queue], 2);
        }
        for (int i = 0; i < dailyRideEarnings[queue]; i++)
        {
            group.Remove(group[0]);                       // Remove from all.
        }
        StartCoroutine(WaitForCustomersToGetOn(2f + (queue - 1) * 0.05f, passengers));
    }

    IEnumerator WaitForCustomersToGetOn(float delay, List<GameObject> passengers)
    {
        yield return new WaitForSeconds(delay);

        rollerCoasterController.Ride(passengers);
    }

    public void GoNextRide_IfExists()
    {
        // group = group.OrderBy(gameObject => gameObject.transform.position.z).ToList();

        Globals.dailyWorkCount++;
        if (Globals.dailyWorkCount < calculator.dailyRideEarnings.Count)
        {
            GetIn(calculator.dailyRideEarnings, rollerCoasterController.GetSeats(), Globals.dailyWorkCount);
        }
    }

    #endregion
}