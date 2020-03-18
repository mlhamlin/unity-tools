using System.Collections.Generic;
using SingletonComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.Localization
{
    public class LocalizationManager : GeneratablePersistentSingletonBase<LocalizationManager>
    {
        private readonly LanguageChangedEvent languageChanged = new LanguageChangedEvent();
        private Language _currentLanguage = Language.Keys;
        private LanguageData _currentLanguageData;
        private readonly Dictionary<Language, LanguageData> _loadedLanguages = new Dictionary<Language, LanguageData>();

        public static string Localize(string key)
        {
            return Instance.LocalizeInternal(key);
        }

        public static void ChangeLanguage(Language language)
        {
            Instance.ChangeLanguageInternal(language);
        }

        public static void AddLanguageChangedListener(UnityAction action)
        {
            Instance.languageChanged.AddListener(action);
        }

        public static void RemoveLanguageChangedListener(UnityAction action)
        {
            if (SafeToAccess && InstanceValueSet)
            {
                Instance.languageChanged.RemoveListener(action);
            }
        }

        private string LocalizeInternal(string key)
        {
            return LanguageHelper.IsNoDataLanguage(_currentLanguage) ? key : _currentLanguageData.Localize(key);
        }

        private void ChangeLanguageInternal(Language language)
        {
            if (language == _currentLanguage)
            {
                Debug.Log("New Language " + language + " is same as current Language " + _currentLanguage);
                return;
            }

            if (LanguageHelper.IsNoDataLanguage(language))
            {
                _currentLanguage = language;
                _currentLanguageData = null;
                languageChanged.Invoke();
                return;
            }

            if (_loadedLanguages.TryGetValue(language, out _currentLanguageData))
            {
                _currentLanguage = language;
                languageChanged.Invoke();
                return;
            }

            if (!TryLoadLanguage(language))
            {
                Debug.Log("Failed to load language file for " + language + " keeping old language loaded.");
                return;
            }

            if (_loadedLanguages.TryGetValue(language, out _currentLanguageData))
            {
                _currentLanguage = language;
                languageChanged.Invoke();
                return;
            }

            Debug.Log("Failed to retrieve language data for " + language);
        }

        private bool TryLoadLanguage(Language language)
        {
            var text = Resources.Load(LanguageHelper.LanguageToFilePath(language)) as TextAsset;
            if (text == null) return false;
            _loadedLanguages[language] = JsonUtility.FromJson<LanguageData>(text.text);
            return true;
        }
    }
}