using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Localization.LocalizeComponents
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextMeshProUGUI : ComponentLocalizerBase
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void UpdateComponent(string localizedText)
        {
            _text.text = localizedText;
        }
    }
}