using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyIntEvent : UnityEvent<int, GameObject[]>
{

}

public class Events : MonoBehaviour
{
    public MyIntEvent m_GetInEvent;

    void Start()
    {
        if (m_GetInEvent == null)
            m_GetInEvent = new MyIntEvent();
    }
}