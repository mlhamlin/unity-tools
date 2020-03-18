using Plugins.Localization;
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
                //Todo: Some fancy stuff to make a drop down
                var value = EditorGUI.TextField(position, label.text, property.stringValue);

                if (property.stringValue != value)
                {
                    property.stringValue = value;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Please use LocaKey attribute only with string values.");
            }
        }
    }
}