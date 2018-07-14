using System;
using System.IO;

namespace KEC.ECommerce.Web.UI.Helpers
{
    public static class WkHtmlToPdf
    {
        public static void Preload()
        {
            var wkHtmlToPdfContext = new CustomAssemblyLoadContext();
            var architectureFolder = (IntPtr.Size == 8) ? "64bit" : "32bit";
            var wkHtmlToPdfPath = Path.Combine(AppContext.BaseDirectory, $"wkhtmltox\\{architectureFolder}\\libwkhtmltox");

            wkHtmlToPdfContext.LoadUnmanagedLibrary(wkHtmlToPdfPath);
        }
    }
}
