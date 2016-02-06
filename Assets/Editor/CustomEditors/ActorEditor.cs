using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Actor), true)]
[CanEditMultipleObjects]
public class ActorEditor : DynamicObjectEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
        
        Properties.AddProperty("m_MovementState", 0);
        Properties.AddProperty("m_CanJump", 0);
        Properties.AddProperty("m_JumpTimer", 0);

        Properties.AddGroup(1, "Horizontal Movement", 1);
        Properties.AddProperty("m_Decay", 1);
        Properties.AddProperty("m_Speed", 1);
        Properties.AddProperty("m_MaxSpeed", 1);

        Properties.AddGroup(2, "Vertical Movement", 1);
        Properties.AddProperty("m_IncrementalJumpForce", 2);
        Properties.AddProperty("m_InitialJumpForce", 2);
        Properties.AddProperty("m_MaxJumpTime", 2);

        Properties.AddGroup(3, "Objects", 1);
        Properties.AddProperty("m_Animator", 3);
        Properties.AddProperty("m_Rigidbody", 3);
    }
}
