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

    public int queue = -1;

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
        // int queue = 0;
        queue = 0;
        //        Debug.Log(queue);
        // int index = FindObjectOfType<Creator>().group.Count - Calculator.Instance.howManyGroupsInRide[queue];
        int index = Calculator.Instance.N - Calculator.Instance.howManyGroupsInRide[queue]; // Toplam Grup sayısından ilk sürüşteki grup sayısını çıkart (Yanlış olmuş)
        // L=10, N=5, ilk grup 10 => index 4 (yani son index)
        // howManyGroupsInRide[queue]=1 olmazsa sıkıntı. 2 olduğunda index=3
        // index=3 iken
        for (int i = 0; i < Calculator.Instance.howManyGroupsInRide[queue]; i++)
        {
            // for (int j = 0; j < Calculator.Instance.P_List[index + i]; j++)
            Debug.Log(Calculator.Instance.temporaryDirham);
            for (int j = 0; j < Calculator.Instance.temporaryDirham; j++)
            {
                Vector3 endOfTheQueue = new Vector3(j, 0, -2 * (index + i));

                // Debug.Log("10 kere");

                // int sum = 0;
                // for (int v = 0; v < i; v++)
                // {
                //     sum += Calculator.Instance.P_List[index + v];
                //     Debug.Log(sum);
                // }
                // Debug.Log(j + Calculator.Instance.P_List[index] + Calculator.Instance.P_List[index + 1] + Calculator.Instance.P_List[index + 2]);
                if (i == 0)
                    passengers[j].transform.DOMove(endOfTheQueue, 2);
                // if (i == 1)
                //     passengers[j + Calculator.Instance.P_List[index] + 1].transform.DOMove(endOfTheQueue, 2);
                // if (i == 2)
                //     passengers[j + Calculator.Instance.P_List[index] + Calculator.Instance.P_List[index + 1]].transform.DOMove(endOfTheQueue, 2);
                // if (i == 3)
                //     passengers[j + Calculator.Instance.P_List[index] + Calculator.Instance.P_List[index + 1] + Calculator.Instance.P_List[index + 2]].transform.DOMove(endOfTheQueue, 2);
                // if (i == 4)
                //     passengers[j + Calculator.Instance.P_List[index] + Calculator.Instance.P_List[index + 1] + Calculator.Instance.P_List[index + 2] + Calculator.Instance.P_List[index + 3]].transform.DOMove(endOfTheQueue, 2);
            }
        }

        yield return new WaitForSeconds(2);
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.parent = null;
            FindObjectOfType<Creator>().group.Add(passenger);    // Add passengers to all.
        }

        // Notify an observer or change an event
        GameManager.Instance.NotifyGameStartObservers();
    }

    #endregion
}