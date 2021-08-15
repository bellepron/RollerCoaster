using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Day/New Day Info")]
public class Day_Info : ScriptableObject
{
    [Header("Roller Coaster Capacity")]
    [Space]
    [Range(1, 10000000)] public int L;

    [Header("How Many Times Roller Coaster Work")]
    [Space]
    [Range(1, 10000000)] public int C;

    [Header("How Many Group Exist")]
    [Space]
    [Range(1, 1000)] public int N;

    

    [Header("Group Sizes")] // N times Pi input ************
    [Space]
    [Range(1, 10000000)] [SerializeField] public int[] Pi;
    public int[] _pi
    {
        get
        {
            for (int i = 0; i < Pi.Length; i++)
            {
                if (Pi[i] > L)
                    Pi[i] = L;
            }

            return Pi;
        }
    }

}