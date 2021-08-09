using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int l;       // Roller coaster limit
    int c;       // Per day roller coaster work c times
    int n;       // Number of Groups
    List<int> p;  // The numbers of people the groups have
    int dirham = 0;
    int cycleOffset;
    List<string> queueStorage;

    void Start()
    {
        // Scriptabledan al L, C, N, P

        // p = new int[n];

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(WhatTheFuck());
        }
    }

    int WhatTheFuck()
    {
        List<int> groupValueStorage = new List<int>();
        List<string> queueStorage = new List<string>();
        for (int i = 0; i < p.Count; i++)
        {
            queueStorage.Add(p.ToString());
        }

        int firstGroupPeopleNumber = p[0];
        p.RemoveAt(0);

        do
        {
            int s = 0;

            // Generate a group
            for (int i = 0; i <= p.Count && s + firstGroupPeopleNumber < l; i++)
            {
                p.Add(firstGroupPeopleNumber);
                s += firstGroupPeopleNumber;

                firstGroupPeopleNumber = p[0]; // How many people are in the next group?
                p.RemoveAt(0);
            }

            cycleOffset = queueStorage.IndexOf(p.ToString());
            queueStorage.Add(p.ToString());
            groupValueStorage.Add(s);   // Found a new group
        }
        while (cycleOffset == -1);


        // Sum up the first elements occuring just once
        for (var i = 0; i < cycleOffset; i++)
        {
            dirham += groupValueStorage[i];
        }

        // Calculate the rest, which doesn't fit into a group
        var r = (c - cycleOffset) % (groupValueStorage.Count - cycleOffset);

        for (int i = 0; i < groupValueStorage.Count; i++)
        {
            //     if (r > 0)
            //         // Group value times the number of occurenij
            //         dirham += groupValueStorage[i] * (Mathf.Floor((c - cycleOffset) / (groupValueStorage.Count - cycleOffset)));
            r--; // Reduce rest

        }
        return dirham;
    }
}