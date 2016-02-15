using UnityEngine;
using System.Collections;

public class LockedPlatform : MovingPlat
{
    [SerializeField]
    protected string m_OriginalDirection;

    [SerializeField]
    protected Button m_ConnectedButton;

    protected override void Start()
    {
        base.Start();
        
        m_OriginalDirection = m_Direction;
    }
    protected override void Update()
    {
        base.Update();

        if (m_ConnectedButton.IsPressed)
            m_Direction = m_OriginalDirection;        
        else
            m_Direction = "";
    }
}
