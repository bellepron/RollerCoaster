using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Day/New Day Info")]
public class Day_Info : ScriptableObject
{
    [Header("Roller Coaster Capacity")]
    [Space]
    [Range(1, 101)] public int L;

    [Header("How Many Times Roller Coaster Work")]
    [Space]
    [Range(1, 101)] public int C;

    [Header("How Many Group Exist")]
    [Space]
    [Range(1, 101)] public int N;

    [Header("Group Sizes")] // N times Pi input ************
    [Space]
    [Range(1, 10)] public int[] Pi;

    public void OnAfterDeserialize()
    {
        Pi = new int[N];
    }

}