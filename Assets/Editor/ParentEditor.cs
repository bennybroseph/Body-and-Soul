using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CanEditMultipleObjects]
public class ParentEditor : Editor
{
    //protected Properties m_Properties = new Properties();

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Properties.Draw();

        DrawPropertiesExcluding(serializedObject, Properties.PropertiesByName);

        serializedObject.ApplyModifiedProperties();
    }
}


static public class Properties
{
    static private SerializedObject m_Object;
    static private List<Group> m_GroupList = new List<Group>();

    static public string[] PropertiesByName
    {
        get
        {
            List<string> Names = new List<string>();

            for (int i = 0; i < m_GroupList.Count; ++i)
                for (int j = 0; j < m_GroupList[i].PropertyList.Count; ++j)
                    Names.Add(m_GroupList[i].PropertyList[j].name);

            return Names.ToArray();
        }
    }

    static public void SetObject(SerializedObject a_Object)
    {
        m_Object = a_Object;
        for (int i = 0; i < m_GroupList.Count; ++i)
            m_GroupList[i].ClearPropertyList();
    }

    static public void Draw()
    {
        int OldIndent = EditorGUI.indentLevel;

        for (int i = 0; i < m_GroupList.Count; ++i)
        {
            if (m_GroupList[i].Indent > 0)
            {
                EditorGUI.indentLevel = OldIndent + m_GroupList[i].Indent;

                m_GroupList[i].IsExpanded = EditorGUILayout.Foldout(m_GroupList[i].IsExpanded, m_GroupList[i].Name);

                if (m_GroupList[i].IsExpanded)
                {
                    EditorGUI.indentLevel += 1;

                    m_GroupList[i].Draw();

                    EditorGUI.indentLevel -= 1;
                }
                EditorGUI.indentLevel = OldIndent;
            }
            else
                m_GroupList[i].Draw();
        }

        EditorGUI.indentLevel = OldIndent;
    }

    static public void AddGroup(int a_Index, string a_Name)
    {
        if (m_GroupList.Count <= a_Index)
            m_GroupList.Add(new Group(a_Name));
        else
            m_GroupList[a_Index] = new Group(a_Name, m_GroupList[a_Index].Indent, m_GroupList[a_Index].IsExpanded);
    }
    static public void AddGroup(int a_Index, string a_Name, int a_Indent)
    {
        if (m_GroupList.Count <= a_Index)
            m_GroupList.Add(new Group(a_Name, a_Indent));
        else
            m_GroupList[a_Index] = new Group(a_Name, a_Indent, m_GroupList[a_Index].IsExpanded);
    }

    static public void AddProperty(string a_Name, int a_GroupIndex)
    {
        while (m_GroupList.Count <= a_GroupIndex)
        {
            Debug.LogWarning("Group Index Out of Range...Avoiding Catastrophe");
            m_GroupList.Add(new Group("Group " + m_GroupList.Count.ToString()));
        }

        m_GroupList[a_GroupIndex].PropertyList.Add(m_Object.FindProperty(a_Name));
    }
    static public void InsertProperty(int a_Index, string a_Name, int a_GroupIndex)
    {
        m_GroupList[a_GroupIndex].PropertyList.Insert(a_Index, m_Object.FindProperty(a_Name));
    }

    private class Group
    {
        private string m_Name;
        private int m_Indent;
        private bool m_IsExpanded;

        private List<SerializedProperty> m_PropertyList;

        public string Name { get { return m_Name; } }
        public int Indent { get { return m_Indent; } }
        public bool IsExpanded { get { return m_IsExpanded; } set { m_IsExpanded = value; } }

        public List<SerializedProperty> PropertyList { get { return m_PropertyList; } }

        public void Draw()
        {
            for (int i = 0; i < m_PropertyList.Count; ++i)
                EditorGUILayout.PropertyField(m_PropertyList[i]);
        }

        public void ClearPropertyList()
        {
            m_PropertyList = new List<SerializedProperty>();
        }

        public Group(string a_Name)
        {
            m_Name = a_Name;
            m_IsExpanded = false;
            m_Indent = 0;

            m_PropertyList = new List<SerializedProperty>();
        }
        public Group(string a_Name, int a_Indent)
        {
            m_Name = a_Name;
            m_IsExpanded = false;
            m_Indent = a_Indent;

            m_PropertyList = new List<SerializedProperty>();
        }
        public Group(string a_Name, int a_Indent, bool a_IsExpanded)
        {
            m_Name = a_Name;
            m_Indent = a_Indent;
            m_IsExpanded = a_IsExpanded;

            m_PropertyList = new List<SerializedProperty>();
        }
    }
}
//[CustomEditor(typeof(Player), true)]
//[CanEditMultipleObjects]
//public class HumanCustomEditor : Editor
//{
//    SerializedProperty p_Decay;
//    SerializedProperty p_Speed;
//    SerializedProperty p_MaxSpeed;

//    SerializedProperty p_CanJump;

//    SerializedObject m_ObjectIterator;
//    SerializedProperty m_PropertyIterator;

//    ReadOnlyDrawer test;

//    protected bool m_IsExpanded;

//    void OnEnable()
//    {
//        p_Decay = serializedObject.FindProperty("m_Decay");
//        p_Speed = serializedObject.FindProperty("m_Speed");
//        p_MaxSpeed = serializedObject.FindProperty("m_MaxSpeed");

//        p_CanJump = serializedObject.FindProperty("m_CanJump");

//        m_ObjectIterator = new SerializedObject(target);

//        CollapsibleEditor.ReInit();
//    }

//    public override void OnInspectorGUI()
//    {
//        if (CollapsibleEditor.IsCached)
//        {
//            m_ObjectIterator.Update();

//            CollapsibleEditor.Draw();

//            m_ObjectIterator.ApplyModifiedProperties();
//        }
//        else
//        {
//            m_PropertyIterator = m_ObjectIterator.GetIterator();

//            while (m_PropertyIterator.NextVisible(true))
//            {
//                EditorGUILayout.PropertyField(m_PropertyIterator);
//                CollapsibleEditor.CacheProperty(m_PropertyIterator);

//                if (m_PropertyIterator.type == "Vector3")
//                    for (int i = 0; i < 3; ++i)
//                        m_PropertyIterator.NextVisible(true);
//            }
//            CollapsibleEditor.Finish();
//        }
//    }
//}

//public static class CollapsibleEditor
//{
//    private struct Group
//    {
//        public Rect Position;
//        public string Text;

//        public bool IsExpanded;
//        public int IndentLevel;

//        public List<SerializedProperty> Property;

//        public Group(Rect a_Position, string a_Text, bool a_IsExpanded, int a_IndentLevel, SerializedProperty a_Property)
//        {
//            Position = a_Position;
//            Text = a_Text;
//            IsExpanded = a_IsExpanded;
//            IndentLevel = a_IndentLevel;
//            Property = new List<SerializedProperty>();

//            Property.Add(a_Property);
//        }
//        public Group(Rect a_Position, string a_Text, bool a_IsExpanded, int a_IndentLevel, List<SerializedProperty> a_Property)
//        {
//            Position = a_Position;
//            Text = a_Text;
//            IsExpanded = a_IsExpanded;
//            IndentLevel = a_IndentLevel;
//            Property = a_Property;
//        }
//    }

//    private static List<Group> m_Group = new List<Group>();
//    private static int m_CurrentGroup = 0;
//    private static int m_CurrentIndentLevel = 0;

//    private static bool m_IsCached = false;

//    public static bool IsCached
//    {
//        get
//        {
//            return m_IsCached;
//        }
//    }

//    public static void Finish()
//    {
//        m_IsCached = true;
//    }
//    public static void ReInit()
//    {
//        m_CurrentGroup = 0;
//        List<Group> m_Group = new List<Group>();
//        m_CurrentGroup = 0;

//        m_IsCached = false;
//    }

//    public static void CacheProperty(SerializedProperty property)
//    {
//        if (m_Group.Count == 0)
//            m_Group.Add(new Group(new Rect(2, 100, 50, 10), null, false, m_CurrentIndentLevel, property));
//        else //if (m_Group[m_CurrentGroup].Property[m_Group[m_CurrentGroup].Property.Count - 1] != property)
//        {
//            Debug.Log("Cached: " + property.displayName);
//            m_Group[m_Group.Count - 1].Property.Add(property);
//        }

//    }

//    public static void NewGroup(Rect position,
//                               SerializedProperty property,
//                               GUIContent label,
//                               string text)
//    {
//        m_CurrentGroup += 1;
//        m_CurrentIndentLevel += 1;

//        m_Group.Add(new Group(position, text, false, m_CurrentIndentLevel, property));
//    }

//    public static void EndGroup(Rect position,
//                               SerializedProperty property,
//                               GUIContent label)
//    {
//        m_CurrentGroup += 1;
//        m_CurrentIndentLevel -= 1;

//        m_Group.Add(new Group(position, null, false, m_CurrentIndentLevel, property));
//    }

//    public static void Draw()
//    {
//        for (int i = 0; i < m_Group.Count; ++i)
//        {
//            if (m_Group[i].Text != null)
//                Debug.Log("Group " + i.ToString() + " Start:" + m_Group[i].Text);
//            //m_Group[i] = new Group(m_Group[i].Position, m_Group[i].Text, EditorGUILayout.Foldout(m_Group[i].IsExpanded, m_Group[i].Text), m_Group[i].IndentLevel, m_Group[i].Property);

//            //if (m_Group[i].IsExpanded || m_Group[i].Text == null)
//            //{
//            //Debug.Log(m_Group[i].Property.Count);
//                for (int j = 0; j < m_Group[i].Property.Count; ++j)
//                {
//                //Debug.Log(j.ToString());
//                    Debug.Log("Index " + j.ToString() + " " + m_Group[i].Property[j].displayName);
//                    //EditorGUILayout.PropertyField(m_Group[i].Property[j]);
//                }
//            //}    
//        }
//    }
//}
