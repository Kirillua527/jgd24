using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute
{
    // This is an empty class, simply used as a marker for the attribute
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;  // Disable editing the field
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;   // Enable editing for other fields
    }
}