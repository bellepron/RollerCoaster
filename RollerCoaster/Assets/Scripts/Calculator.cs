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



    int howManyGroup;
    List<int> howManyGroupList;

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

        FindObjectOfType<Creator>().Init_FromCalculator(P_List, N);
    }

    public int Calculate()
    {
        howManyGroupList = new List<int>();

        while (C > 0)
        {
            bool available = true;
            int temporaryDirham = 0;
            howManyGroup = 0;

            do
            {
                firstGroupSize = P_List[0];
                if (L - temporaryDirham - firstGroupSize >= 0)
                {
                    P_List.RemoveAt(0);
                    // Debug.Log(firstGroupSize);
                    temporaryDirham += firstGroupSize;
                    dirham += firstGroupSize;
                    P_List.Add(firstGroupSize);
                    howManyGroup++;
                }
                if (L - temporaryDirham - firstGroupSize < 0)
                {
                    temporaryDirham = 0;
                    C--;
                    available = false;
                    howManyGroupList.Add(howManyGroup);
                }
            }
            while (available);
        }

        return dirham;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Calculate();
            Debug.Log(howManyGroupList.Count);
            //FindObjectOfType<Creator>().GetIn(howManyGroupList);
        }
    }
}
