using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(HeaderAttribute))]
public class GroupDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        HeaderAttribute Header = attribute as HeaderAttribute;

        //Debug.Log(label.text);
        CollapsibleEditor.IncrementGroup(position, property, label, Header.header);
    }
}