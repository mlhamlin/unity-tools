using System;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizedText : ComponentLocalizerBase
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        protected override void UpdateComponent(string localizedText)
        {
            _text.text = localizedText;
        }
    }
}