using System;
using UnityEditor;
using UnityEngine;

namespace Plugins.Localization
{
    public abstract class ComponentLocalizerBase : UnityEngine.MonoBehaviour
    {
        [SerializeField, LocaKey] protected string locaKey;

        protected abstract void UpdateComponent(string localizedText);

        protected virtual void Localize()
        {
            UpdateComponent(LocalizationManager.Localize(locaKey));
        }

        protected virtual void Start()
        {
            Localize();
            LocalizationManager.AddLanguageChangedListener(Localize);
        }

        protected void OnDestroy()
        {
            LocalizationManager.RemoveLanguageChangedListener(Localize);
        }
    }
}