using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator : MonoBehaviour
{
    public static Calculator Instance;
    [SerializeField] Day_Info day_Info;

    public int L;
    int C;
    public int N;
    int[] Pi;
    public List<int> P_List;

    public int dirham = 0;
    public int temporaryDirham;
    int firstGroupSize;


    List<int> howManyGroupList;
    public List<int> dailyRideEarnings;

    public List<int> howManyGroupsInRide;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        Time.timeScale = 3f;
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

    public int Calculate() // IMPORTANT TO DO: bir grup birden çok binebiliyor. Örn: L:100,N:1,Pi:[2];
    {
        howManyGroupList = new List<int>();
        dailyRideEarnings = new List<int>();
        howManyGroupsInRide = new List<int>();

        while (C > 0)
        {
            bool available = true;
            temporaryDirham = 0;
            int currentRideCapacity = 0;

            int countt = 0;


            for (; available;)
            {
                firstGroupSize = P_List[0];

                if (L - temporaryDirham - firstGroupSize >= 0)
                {
                    P_List.RemoveAt(0);
                    temporaryDirham += firstGroupSize;
                    dirham += firstGroupSize;
                    P_List.Add(firstGroupSize);

                    countt++;
                }
                else
                {
                    howManyGroupsInRide.Add(countt);

                    currentRideCapacity = temporaryDirham; // sürüşten gelen para (bunu listeye ekle)(listedeki sayıların toplamı günlük kazanç olacak)
                    dailyRideEarnings.Add(currentRideCapacity);

                    temporaryDirham = 0;
                    C--;
                    available = false;
                }
            }
        }

        return dirham;
    }

    public void StartSimulation()
    {
        Calculate();
        FindObjectOfType<Creator>().ReadyToGetIn(dailyRideEarnings);
        // Debug.Log(Calculator.Instance.P_List[Calculator.Instance.P_List.Count - Calculator.Instance.howManyGroupsInRide[0] + 1]);
        // Debug.Log(howManyGroupsInRide[0]);
        // Debug.Log(howManyGroupsInRide[1]);
        // Debug.Log(howManyGroupsInRide[2]);
        // Debug.Log(howManyGroupsInRide[3]);
    }
}