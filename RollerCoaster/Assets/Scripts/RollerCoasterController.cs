using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RollerCoasterController : MonoBehaviour
{
    public int length;
    [SerializeField] GameObject rollerCoasterModule;
    List<GameObject> modules;

    public void ArrangeSize()
    {
        transform.localScale = new Vector3(2, 0.5f, length);
        modules = new List<GameObject>();

        for (int i = 0; i < length; i++)
        {
            modules.Add(Instantiate(rollerCoasterModule, transform.position + new Vector3(i * 1, 0, 0), Quaternion.identity));
        }
    }
    public List<GameObject> GetSeats()
    {
        return modules;
    }

    #region Ride

    public void Ride(List<GameObject> passengers)
    {
        foreach (GameObject module in modules)
        {
            Vector3 startPos = module.transform.position;

            module.transform.DOMove(new Vector3(-10, 1, 20), 1).OnComplete(() =>
            module.transform.DOMove(new Vector3(10, 1, 20), 1).OnComplete(() =>
            module.transform.DOMove(startPos, 1)
            ));
        }

        StartCoroutine(GetOffTheRollerCoaster(passengers));
    }

    IEnumerator GetOffTheRollerCoaster(List<GameObject> passengers)
    {
        yield return new WaitForSeconds(3);
        int index = Calculator.Instance.N - Calculator.Instance.howManyGroupsInRide[0];
        for (int i = 0; i < Calculator.Instance.howManyGroupsInRide[0]; i++)
        {
            for (int j = 0; j < Calculator.Instance.P_List[index + i]; j++)
            {
                Vector3 endOfTheQueue = new Vector3(j, 0, -20 - i);

                if (i == 0)
                    passengers[j].transform.DOMove(endOfTheQueue, 2);
                if (i == 1)
                    passengers[j + Calculator.Instance.P_List[index]].transform.DOMove(endOfTheQueue, 2);
                if (i == 2)
                    passengers[j + Calculator.Instance.P_List[index + 1] + Calculator.Instance.P_List[index]].transform.DOMove(endOfTheQueue, 2);
            }
        }
        // for (int j = 0; j < Calculator.Instance.P_List[Calculator.Instance.P_List.Count - Calculator.Instance.howManyGroupsInRide[0]]; j++)
        // {
        //     Vector3 endOfTheQueue = new Vector3(j, 0, -20);
        //     passengers[j].transform.DOMove(endOfTheQueue, 2);
        // }
        // for (int j = 0; j < Calculator.Instance.P_List[Calculator.Instance.P_List.Count - Calculator.Instance.howManyGroupsInRide[0] + 1]; j++)
        // {
        //     Vector3 endOfTheQueue = new Vector3(j, 0, -20 - 1);
        //     passengers[j + Calculator.Instance.P_List[Calculator.Instance.P_List.Count - Calculator.Instance.howManyGroupsInRide[0]]].transform.DOMove(endOfTheQueue, 2);
        // }

        yield return new WaitForSeconds(2);
        foreach (GameObject passenger in passengers)
        {
            FindObjectOfType<Creator>().group.Add(passenger);    // Add passengers to all.
        }

        // Notify an observer or change an event
        GameManager.Instance.NotifyGameStartObservers();
    }

    #endregion
}