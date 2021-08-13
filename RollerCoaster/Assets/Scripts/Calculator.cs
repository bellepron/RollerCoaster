using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] Day_Info day_Info;

    int L;
    int C;
    int N;
    int[] Pi;
    List<int> P_List = new List<int>(4);

    public int dirham = 0;
    int firstGroupSize;

    void Start()
    {
        L = day_Info.L;
        C = day_Info.C;
        N = day_Info.N;
        Pi = day_Info.Pi;

        for (int i = 0; i < N; i++)
        {
            P_List.Add(day_Info.Pi[i]);
        }
    }

    public int Calculate()
    {
        while (C > 0)
        {
            int cycle = 0;
            int temporaryDirham = 0;
            firstGroupSize = P_List[0];
            P_List.RemoveAt(0);

            Debug.Log(firstGroupSize);

            do
            {
                if (L - temporaryDirham - firstGroupSize >= 0)
                {
                    temporaryDirham += firstGroupSize;
                    dirham += firstGroupSize;
                    P_List.Add(firstGroupSize);
                }
                else
                {
                    temporaryDirham = 0;
                    C--;
                    cycle = 1;
                }
            }
            while (cycle == 0);
        }

        return dirham;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && day_Info != null)
        {
            Debug.Log(Calculate());
        }
    }
}
