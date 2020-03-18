using System;

namespace Plugins.Localization
{
    public static class LanguageHelper
    {
        private static string filePrefix = "Localization/";

        public static string LanguageToFilePath(Language language)
        {
            switch (language)
            {
                case Language.Keys:
                    return filePrefix + "keys";
                case Language.EnglishUS:
                    return filePrefix + "en_us";
                case Language.FrenchTest:
                    return filePrefix + "fr_test";
                case Language.SpanishTest:
                    return filePrefix + "fr_test";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        public static bool IsNoDataLanguage(Language language)
        {
            return language == Language.Keys || language == Language.None;
        }
    }
}