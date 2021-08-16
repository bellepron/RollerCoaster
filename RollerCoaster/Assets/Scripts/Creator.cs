using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Creator : MonoBehaviour, IEndOfRideObserver
{
    public static Creator Instance;
    [SerializeField] Calculator calculator;
    RollerCoasterController rollerCoasterController;

    [SerializeField] GameObject characterPrefab;
    public List<GameObject> group;
    public List<GameObject> passengers;

    [SerializeField] Material[] materials;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        GameManager.Instance.AddEndOfRideObserver(this);
    }

    #region Creation

    public void Init_FromCalculator(List<int> Pi_list, int N)
    {
        rollerCoasterController = FindObjectOfType<RollerCoasterController>();
        group = new List<GameObject>();

        for (int i = 0; i < N; i++) // Create customers
        {
            for (int j = 0; j < Pi_list[i]; j++)
            {
                GameObject go = Instantiate(characterPrefab, new Vector3(j * 1, 0, -i * 2f), Quaternion.identity) as GameObject;
                SetCustomerMaterial(go, i);
                AddToList(go);
            }
        }
    }
    void SetCustomerMaterial(GameObject go, int i)
    {
        go.GetComponent<MeshRenderer>().material = materials[i % materials.Length];
    }
    void AddToList(GameObject go)
    {
        group.Add(go);
    }

    #endregion

    #region Ride

    public void ReadyToGetIn(List<int> dailyRideEarnings)
    {
        List<GameObject> seats = rollerCoasterController.GetSeats();

        // Debug.Log(dailyRideEarnings[0]);
        GetIn(dailyRideEarnings, seats, Globals.dailyWorkCount);
    }

    public void GetIn(List<int> dailyRideEarnings, List<GameObject> seats, int queue)
    {
        passengers = new List<GameObject>(); // Remove from all, add back when queue up.

        for (int i = 0; i < dailyRideEarnings[queue]; i++) // Go to the seats & adding to new list (passengers)
        {
            group[i].transform.DOMove(seats[i].transform.position, 1.2f + i * 0.05f);
            group[i].transform.parent = seats[i].transform;

            passengers.Add(group[i]);
        }
        for (int i = dailyRideEarnings[queue]; i < group.Count; i++) // Go forward (others)
        {
            // group[i].transform.DOMoveZ(group[i].transform.position.z + 2 * Calculator.Instance.howManyGroupsInRide[queue], 2);
            group[i].transform.DOMoveZ(group[i].transform.position.z + 2, 2); // TO DO: fix&arrange
        }
        for (int i = 0; i < dailyRideEarnings[queue]; i++) // Remove from group (passengers)
        {
            group.Remove(group[0]);
        }
        StartCoroutine(WaitForCustomersToGetOn(2f + (queue - 1) * 0.02f));
    }

    IEnumerator WaitForCustomersToGetOn(float delay)
    {
        yield return new WaitForSeconds(delay);

        rollerCoasterController.StartRide();
    }

    #endregion
    public void GoNextRide_IfExists()
    {
        Globals.dailyWorkCount++;

        if (Globals.dailyWorkCount < calculator.dailyRideEarnings.Count)
        {
            GetIn(calculator.dailyRideEarnings, rollerCoasterController.GetSeats(), Globals.dailyWorkCount);
        }
    }
}