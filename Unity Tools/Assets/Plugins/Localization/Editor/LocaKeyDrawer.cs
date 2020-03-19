using UnityEditor;
using UnityEngine;

namespace Plugins.Localization
{
    [CustomPropertyDrawer(typeof(LocaKeyAttribute))]
    public class LocaKeyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                var labelText = label.text;

                var internalPosition =
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                var currentString = property.stringValue;
                var list = LocalizationEditorManager.GetKeys(currentString);
                var index = 0;
                if (!string.IsNullOrWhiteSpace(currentString))
                {
                    index = list.IndexOf(currentString);
                    if (index < 0)
                    {
                        index = 0;
                    }
                }

                label = EditorGUI.BeginProperty(position, label, property);
                EditorGUI.BeginChangeCheck();
                var newIndex = EditorGUI.Popup(internalPosition, label.text, index, list.ToArray());
                if (EditorGUI.EndChangeCheck())
                {
                    property.stringValue = (newIndex == 0) ? string.Empty : list[newIndex];
                }

                EditorGUI.EndProperty();

                internalPosition.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.BeginChangeCheck();
                var newFilter = EditorGUI.TextField(internalPosition, "Filter", LocalizationEditorManager.GetFilter());
                if (EditorGUI.EndChangeCheck())
                {
                    LocalizationEditorManager.UpdateFilter(newFilter);
                }

                internalPosition.y += EditorGUIUtility.singleLineHeight;

                if (GUI.Button(internalPosition, "Reimport Keys"))
                {
                    LocalizationEditorManager.ReimportKeys();
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Please use LocaKey attribute only with string values.");
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
}