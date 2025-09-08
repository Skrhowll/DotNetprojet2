using System;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace P2FixAnAppDotNetCode.Resources.Models.ViewModels
{
    public static class Order
    {
        private static ResourceManager resourceManager = new ResourceManager("P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order", Assembly.GetExecutingAssembly());
        // Changement : la variable resourceCulture est initialisée avec la culture utilisateur courante (CultureInfo.CurrentUICulture).
        // Cela permet de récupérer les ressources localisées selon la langue de l'utilisateur actif.
        private static CultureInfo resourceCulture = CultureInfo.CurrentUICulture;

        public static string ErrorMissingName
        {
            get
            {
                return resourceManager.GetString("ErrorMissingName", resourceCulture);
            }
        }
        public static string ErrorMissingAddress
        {
            get
            {
                return resourceManager.GetString("ErrorMissingAddress", resourceCulture);
            }
        }
        public static string ErrorMissingCity
        {
            get
            {
                return resourceManager.GetString("ErrorMissingCity", resourceCulture);
            }
        }
        public static string ErrorMissingCountry
        {
            get
            {
                return resourceManager.GetString("ErrorMissingCountry", resourceCulture);
            }
        }
    }
}

