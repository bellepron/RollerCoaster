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
    GameObject firstVagoon;
    Vector3 firstVagoonsFirstPosition;

    public int queue = -1;

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
            {
                // vagoon.AddComponent<Rigidbody>();
                // vagoon.AddComponent<FirstVagoon>();
                firstVagoon = vagoon;
                firstVagoonsFirstPosition = firstVagoon.transform.position;
            }
        }

        firstVagoonsFirstPosition = firstVagoon.transform.position; // Init
    }
    public List<GameObject> GetSeats()
    {
        return modules;
    }

    #region Ride

    public void Ride(List<GameObject> passengers)
    {
        firstVagoonsFirstPosition = firstVagoon.transform.position;

        foreach (GameObject module in modules)
        {
            Vector3 startPos = module.transform.position;

            // module.transform.DOMove(new Vector3(-10, 1, 20), 1).OnComplete(() =>
            // module.transform.DOMove(new Vector3(10, 1, 20), 1).OnComplete(() =>
            // module.transform.DOMove(startPos, 1)
            // ));
            module.GetComponent<Follower_RollerCoaster>().speed = 60;
        }

        // StartCoroutine(GetOffTheRollerCoaster(passengers));

        StartCoroutine(FinishTheRide(passengers));
    }
    IEnumerator FinishTheRide(List<GameObject> passengers)
    {
        yield return new WaitForSeconds(1f);
        bool contin = true;
        while (contin)
        {
            if (Vector3.Distance(firstVagoonsFirstPosition, firstVagoon.transform.position) < 0.5f)
            {
                foreach (GameObject module in modules)
                    module.GetComponent<Follower_RollerCoaster>().speed = 0;

                StartCoroutine(GetOffTheRollerCoaster(passengers));

                contin = false;
            }
            yield return null;
        }
    }

    IEnumerator GetOffTheRollerCoaster(List<GameObject> passengers)
    {
        yield return new WaitForSeconds(0.1f);
        queue = 0;

        int index = Calculator.Instance.N - Calculator.Instance.howManyGroupsInRide[queue];

        for (int i = 0; i < Calculator.Instance.howManyGroupsInRide[queue]; i++)
        {
            // for (int j = 0; j < Calculator.Instance.P_List[index + i]; j++)

            for (int j = 0; j < Calculator.Instance.temporaryDirham; j++)
            {
                Vector3 endOfTheQueue = new Vector3(j, 0, -2 * (index + i));

                if (i == 0)
                    passengers[j].transform.DOMove(endOfTheQueue, 2);

            }
        }

        yield return new WaitForSeconds(1);
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.parent = null;
            FindObjectOfType<Creator>().group.Add(passenger);    // Add passengers to all.
        }

        GameManager.Instance.NotifyGameStartObservers();
    }

    #endregion
}