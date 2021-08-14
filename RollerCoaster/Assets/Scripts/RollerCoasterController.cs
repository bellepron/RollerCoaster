using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.parent = null;

            Vector3 endOfTheQueue = new Vector3(0, 0, -30); // Gruplara göre yolla
            passenger.transform.DOMove(endOfTheQueue, 2);
        }

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