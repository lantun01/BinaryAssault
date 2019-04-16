using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorList
{
    public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
    {
        bool
            showListLabel = (options & EditorListOption.ListLabel) != 0,
            showListSize = (options & EditorListOption.ListSize) != 0;


        Debug.Log(list.isArray);

        if (showListLabel)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
        }
        if (!showListLabel || list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            ShowElements(list, options);
        }
        if (showListLabel)
        {
            EditorGUI.indentLevel -= 1;
        }
    }

    private static void ShowElements(SerializedProperty list, EditorListOption options)
    {
        bool showElementLabels = (options & EditorListOption.ElementLabels) != 0;

        for (int i = 0; i < list.arraySize; i++)
        {
            SerializedProperty li = list.GetArrayElementAtIndex(i);
                if (li.isArray)
                {
                    Debug.Log("Es array");
                    Show(list.GetArrayElementAtIndex(i), options);
                }
                else
                {
                    Debug.Log("No es array");
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
                }
           
        }
    }
}
