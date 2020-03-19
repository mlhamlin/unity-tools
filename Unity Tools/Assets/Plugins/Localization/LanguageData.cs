using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Localization
{
    [Serializable]
    public class LanguageData
    {
        [SerializeField] private string language = "";
        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<string> values = new List<string>();

        public string Localize(string key)
        {
            var index = keys.FindIndex(x => x == key);
            if (index >= 0 && index < values.Count)
            {
                return values[index];
            }

            Debug.Log("Failed to find localized text for key " + key + " in language " + language);
            return key;
        }
    }
}