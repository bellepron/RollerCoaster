using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Creator : MonoBehaviour
{
    [SerializeField] Calculator calculator;

    [SerializeField] GameObject characterPrefab;
    List<GameObject> group;

    public void Init_FromCalculator(List<int> Pi_list, int N)
    {
        // for (int i = 0; i < N; i++)
        // {
        //     for (int j = 0; j < Pi_list[i]; j++)
        //         group[i] = Instantiate(characterPrefab, new Vector3(j * 1, 0, -i * 2), Quaternion.identity);
        // }
    }

    public void GetIn(List<int> howManyGroupList)
    {
        // int a = howManyGroupList.Count;
        // while (a > 0)
        // {
        //     a--;
        //     for (int i = 0; i < howManyGroupList[0]; i++)
        //     {
        //         // group[i].transform.DOMove(new Vector3(0, 0, 20), 1 + i * 0.1f);
        //     }
        // }
    }
}
