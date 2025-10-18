using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace P2FixAnAppDotNetCode.Resources.Models.ViewModels
{
    public static class Order
    {
        private static ResourceManager resourceManager = new ResourceManager("P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order", Assembly.GetExecutingAssembly());
        // Correction : Utilise CultureInfo.CurrentUICulture à chaque accès
        public static string ErrorMissingName
        {
            get
            {
                return resourceManager.GetString("ErrorMissingName", CultureInfo.CurrentUICulture);
            }
        }
        public static string ErrorMissingAddress
        {
            get
            {
                return resourceManager.GetString("ErrorMissingAddress", CultureInfo.CurrentUICulture);
            }
        }
        public static string ErrorMissingCity
        {
            get
            {
                return resourceManager.GetString("ErrorMissingCity", CultureInfo.CurrentUICulture);
            }
        }
        public static string ErrorMissingCountry
        {
            get
            {
                return resourceManager.GetString("ErrorMissingCountry", CultureInfo.CurrentUICulture);
            }
        }
        public static string ErrorMissingZip
        {
            get
            {
                return resourceManager.GetString("ErrorMissingZip", CultureInfo.CurrentUICulture);
            }
        }
    }

    public class OrderViewModel
    {
        [Required(
           ErrorMessageResourceType = typeof(P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order),
           ErrorMessageResourceName = "ErrorMissingName"
       )]
        public string Name { get; set; }
        [Required(
            ErrorMessageResourceType = typeof(P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order),
            ErrorMessageResourceName = "ErrorMissingAddress"
        )]
        public string Address { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order),
            ErrorMessageResourceName = "ErrorMissingCity"
        )]
        public string City { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order),
            ErrorMessageResourceName = "ErrorMissingCountry"
        )]
        public string Country { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(P2FixAnAppDotNetCode.Resources.Models.ViewModels.Order),
            ErrorMessageResourceName = "ErrorMissingZip"
        )]
        public string Zip { get; set; }
    }
}

