using UnityEngine;
using System.Collections.Generic;

public class Morning_Assignment : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> m_Children;

    void Update()
    {
        m_Children = new List<Transform>();

        foreach (Transform child in transform)
            m_Children.Add(child);
    }
}
