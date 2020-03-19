using Plugins.Localization;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void SetLanguageToKeys()
    {
        LocalizationManager.ChangeLanguage(Language.Keys);
    }

    public void SetLanguageToEnglish()
    {
        LocalizationManager.ChangeLanguage(Language.EnglishUS);
    }

    public void SetLanguageToFrench()
    {
        LocalizationManager.ChangeLanguage(Language.FrenchTest);
    }
}