using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Localization
{
    [Serializable]
    public class KeyList
    {
        [SerializeField] private List<string> keys = new List<string>();

        public List<string> Keys => keys;
    }
}