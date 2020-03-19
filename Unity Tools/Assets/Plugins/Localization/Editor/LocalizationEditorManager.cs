using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Plugins.Localization
{
    public class LocalizationEditorManager
    {
        private static LocalizationEditorManager _instance;

        private bool _init;
        private const string KEY_PATH = "Assets/Resources/Localization/keys.json";
        private string _filter = "";
        private List<string> _keys = new List<string>();

        public static string GetFilter()
        {
            return Instance._filter;
        }

        public static List<string> GetKeys(string currentKey)
        {
            return Instance.FilterKeysInternal(currentKey);
        }

        public static void UpdateFilter(string newFilter)
        {
            Instance.UpdateFilterInternal(newFilter);
        }

        public static void ReimportKeys()
        {
            Instance.ImportKeysInternal();
        }

        private LocalizationEditorManager()
        {
            ImportKeysInternal();
        }

        private static LocalizationEditorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalizationEditorManager();
                }

                return _instance;
            }
        }

        private void UpdateFilterInternal(string newFilter)
        {
            _filter = newFilter;
        }

        private void ImportKeysInternal()
        {
            var keysJson = EditorGUIUtility.Load(KEY_PATH) as TextAsset;
            if (keysJson == null) return;
            var keysObj = JsonUtility.FromJson<KeyList>(keysJson.text);
            _keys = keysObj.Keys;
            _keys.Sort(string.CompareOrdinal);
        }

        private List<string> FilterKeysInternal(string currentKey)
        {
            List<string> tempList;
            if (string.IsNullOrWhiteSpace(_filter))
            {
                tempList = new List<string>(_keys);
            }
            else
            {
                tempList = _keys.FindAll(x => KeepInList(x, _filter, currentKey));
            }

            tempList.Insert(0, "None");

            return tempList;
        }

        private bool KeepInList(string key, string filter, string currentSelectedKey)
        {
            return !string.IsNullOrWhiteSpace(key) &&
                   (key == currentSelectedKey ||
                    CultureInfo.InvariantCulture.CompareInfo.IndexOf(key, filter, CompareOptions.IgnoreCase) >= 0);
        }
    }
}