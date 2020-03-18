using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Localization
{
    [Serializable]
    public class LanguageData
    {
        [SerializeField] private string language;
        [SerializeField] private Dictionary<string, string> data = new Dictionary<string, string>();

        public string Localize(string key)
        {
            if (data.TryGetValue(key, out var text))
            {
                return text;
            }

            Debug.Log("Failed to find localized text for key " + key + " in language " + language);
            return key;
        }
    }
}