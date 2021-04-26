using System;
using Microsoft.Extensions.Options;

namespace PromoCodesAPI.Helpers
{
    public class AppSettings
    {
        public string TokenSecret { get; set; }
    }

    public class SettingsHelper
    {
        private AppSettings _appSettings { get; set; }

        public SettingsHelper(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public AppSettings GetAppSettings()
        {
            return _appSettings;
        }
    }
}
