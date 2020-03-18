using UnityEngine;

namespace Plugins.Localization.LocalizeComponents
{
    public abstract class ComponentLocalizerBase : MonoBehaviour
    {
        [SerializeField, LocaKey] protected string locaKey;

        protected abstract void UpdateComponent(string localizedText);

        protected virtual void Localize()
        {
            Debug.Log("Re localize");
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