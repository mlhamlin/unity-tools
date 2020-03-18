using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Plugins.Localization
{
    public static class LocalizationEditorManager
    {
        private const string KEY_PATH = "Assets/Resources/Localization/keys.json";
        private static string _filter = "";
        private static string[] _filteredKeys;
        private static List<string> _keys = new List<string>();

        public static void UpdateFilter(string newFilter)
        {
            if (_filter == newFilter) return;

            _filter = newFilter;
            if (string.IsNullOrWhiteSpace(_filter))
            {
                _filteredKeys = _keys.ToArray();
            }
            else
            {
                _filteredKeys = _keys.FindAll(x =>
                        CultureInfo.InvariantCulture.CompareInfo.IndexOf(x, _filter, CompareOptions.IgnoreCase) >= 0)
                    .ToArray();
            }
        }

        public static void ReimportKeys()
        {
            var keysJson = EditorGUIUtility.Load(KEY_PATH) as TextAsset;
            var keysObj = JsonUtility.FromJson<KeyList>(keysJson.text);
            _keys = keysObj.Keys;
        }

        public static string[] GetKeys()
        {
            return _filteredKeys;
        }
    }
}