using Backoffice.Abstractions.Bo;

namespace Backoffice.Services.Platforms
{
    public static class DetectOpera
    {
        public static DeviceInfo ParseOpera(this string userAgent)
        {
            userAgent.DetectPlatform(out var formFactor, out var platformType, out var osVersion);

            var result = new DeviceInfo
            {
                Architecture = ArchitectureType.Unknown,
                AppHost = ApplicationHost.Opera,
                OsVersion = osVersion,
                AppVersion = userAgent.SubstringFromString("Version/"),
                FormFactor = formFactor,
                PlatformType = platformType
            };

            return result;
        }

        private static void DetectPlatform(this string userAgent, out FormFactor formFactor,
            out PlatformType platformType, out string osVersion)
        {
            var theData = userAgent.ToLower().SubstringBetween('(', ')').Split(';');


            foreach (var s in theData)
            {
                if (s.StartsWith("series 60"))
                {
                    platformType = PlatformType.Symbian;
                    formFactor = FormFactor.Mobile;
                    osVersion = null;
                    return;
                }


                if (s.StartsWith("android"))
                {
                    platformType = PlatformType.Android;
                    formFactor = FormFactor.Mobile;
                    osVersion = null;
                    return;
                }

                if (s.StartsWith("blackberry"))
                {
                    platformType = PlatformType.BlackBerry;
                    formFactor = FormFactor.Mobile;
                    osVersion = null;
                    return;
                }

                if (s.StartsWith("iphone"))
                {
                    platformType = PlatformType.Apple;
                    formFactor = FormFactor.Mobile;
                    osVersion = null;
                    return;
                }

                if (s.StartsWith("ipad"))
                {
                    platformType = PlatformType.Apple;
                    formFactor = FormFactor.Tablet;
                    osVersion = null;
                    return;
                }

                if (s.StartsWith("windows nt"))
                {
                    platformType = PlatformType.Windows;
                    formFactor = FormFactor.Desktop;
                    osVersion = PlatformManager.WindowsVersions.GetExtendedInfo(s.SubstringFromString("windows nt")
                        .Trim());
                    return;
                }


                if (s.StartsWith("windows mobile"))
                {
                    platformType = PlatformType.Windows;
                    formFactor = FormFactor.Mobile;
                    osVersion = null;
                    return;
                }
            }

            platformType = PlatformType.Unknown;
            formFactor = FormFactor.Unknown;
            osVersion = null;
        }
    }
}