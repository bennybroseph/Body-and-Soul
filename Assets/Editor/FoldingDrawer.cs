using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Human))]
[CanEditMultipleObjects]
public class HumanCustomEditor : Editor
{
    SerializedProperty p_Decay;
    SerializedProperty p_Speed;
    SerializedProperty p_MaxSpeed;

    SerializedProperty p_CanJump;

    SerializedObject m_ObjectIterator;
    SerializedProperty m_PropertyIterator;

    ReadOnlyDrawer test;

    protected bool m_IsExpanded;

    void OnEnable()
    {
        CollapsibleEditor.Init();

        p_Decay = serializedObject.FindProperty("m_Decay");
        p_Speed = serializedObject.FindProperty("m_Speed");
        p_MaxSpeed = serializedObject.FindProperty("m_MaxSpeed");

        p_CanJump = serializedObject.FindProperty("m_CanJump");

        m_ObjectIterator = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        //

        m_ObjectIterator.Update();

        CollapsibleEditor.ReInit();       

        m_PropertyIterator = m_ObjectIterator.GetIterator();

        while (m_PropertyIterator.NextVisible(true))
        {
            EditorGUILayout.PropertyField(m_PropertyIterator);
            
            if (m_PropertyIterator.type == "Vector3")
                for (int i = 0; i < 3; ++i)
                    m_PropertyIterator.NextVisible(true);          
        }

        m_ObjectIterator.ApplyModifiedProperties();
        //Repaint();
    }
}

public static class CollapsibleEditor
{
    private static int m_CurrentGroup;

    private static List<bool> m_IsExpanded = new List<bool>();

    public static bool IsExpanded
    {
        get
        {
            return m_IsExpanded[m_CurrentGroup];
        }
    }
    public static void Init()
    {
        m_IsExpanded.Add(true);        
    }
    public static void ReInit()
    {
        m_CurrentGroup = 0;
    }    

    public static void IncrementGroup(Rect position,
                               SerializedProperty property,
                               GUIContent label,
                               string text)
    {
        m_CurrentGroup += 1;

        if(m_IsExpanded.Count == m_CurrentGroup)
            m_IsExpanded.Add(true);

        m_IsExpanded[m_CurrentGroup] = EditorGUI.Foldout(position, m_IsExpanded[m_CurrentGroup], text);

        if (m_IsExpanded[m_CurrentGroup])
            EditorGUILayout.PropertyField(property);
    }
}
