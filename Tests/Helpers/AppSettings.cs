using System;
using Microsoft.Extensions.Options;
using Moq;
namespace Tests.Helpers
{
    public class AppSettings
    {
        public AppSettings()
        {
            //
        }

        public static IOptions<PromoCodesAPI.Helpers.AppSettings> GetAppSettings()
        {
            var appSetting = new PromoCodesAPI.Helpers.AppSettings { TokenSecret = "sample token secret" };
            var mock = new Mock<IOptions<PromoCodesAPI.Helpers.AppSettings>>();
            mock.Setup(p => p.Value).Returns(appSetting);
            return mock.Object;
        }
    }
}
