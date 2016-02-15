using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicObject), true)]
[CanEditMultipleObjects]
public class DynamicObjectEditor : ParentEditor
{
    protected virtual void OnEnable()
    {
        Properties.SetObject(serializedObject);

        Properties.AddGroup(0, "Loose Variables");
        Properties.AddProperty("m_IsActive", 0);
        Properties.AddProperty("m_Velocity", 0);
        Properties.AddProperty("m_TotalVelocity", 0);
    }
}