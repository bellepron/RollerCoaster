using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator : MonoBehaviour
{
    public static Calculator Instance;
    Day_Info day_Info;

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
    public int currentRideCapacity;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartNew(Day_Info day_Info)
    {
        day_Info = FindObjectOfType<GameSceneController>().day_Info;

        L = day_Info.L;
        C = day_Info.C;
        N = day_Info.N;
        Pi = day_Info._pi;

        for (int i = 0; i < N; i++)
        {
            P_List.Add(day_Info.Pi[i]);
        }

        Creator.Instance.Init_FromCalculator(P_List, N);
    }

    #region LOGIC

    public int Calculate() // IMPORTANT TO DO: bir grup birden çok binebiliyor. Örn: L:100,N:1,Pi:[2];
    {
        howManyGroupList = new List<int>(); // Bunları en üstte tanımlayıp burda .Clear() kullan.
        dailyRideEarnings = new List<int>();
        howManyGroupsInRide = new List<int>();


        while (C > 0)
        {
            bool available = true;
            temporaryDirham = 0;
            currentRideCapacity = 0;
            int countt = 0;

            List<int> holder = new List<int>();


            for (; available;)
            {
                if (P_List.Count > 0)
                    firstGroupSize = P_List[0];

                if (L - temporaryDirham - firstGroupSize >= 0 && P_List.Count > 0)
                {
                    P_List.RemoveAt(0);
                    temporaryDirham += firstGroupSize;
                    dirham += firstGroupSize;
                    // P_List.Add(firstGroupSize);
                    holder.Add(firstGroupSize);

                    countt++;
                }
                else
                {
                    howManyGroupsInRide.Add(countt);
                    currentRideCapacity = temporaryDirham;
                    dailyRideEarnings.Add(currentRideCapacity);
                    temporaryDirham = 0;
                    C--;
                    available = false;

                    foreach (int fgs in holder)
                    {
                        P_List.Add(fgs);
                    }
                }
            }
        }

        return dirham;
    }

    #endregion

    public void StartSimulation()
    {
        Calculate();
        Creator.Instance.ReadyToGetIn(dailyRideEarnings);
    }
}