using System;
using System.Collections.Generic;
using System.Linq;
using Backoffice.Abstractions.Bo;
using Backoffice.Services.Platforms;

namespace Backoffice.Services
{
    public static class PlatformManager
    {
        private static readonly DeviceInfo UnknownPlatform = new DeviceInfo
        {
            FormFactor = FormFactor.Unknown,
            PlatformType = PlatformType.Unknown,
            AppHost = ApplicationHost.Unknown
        };
        
        public enum AndroidPlatformName
        {
            Unknown,
            Cupcake,
            Donut,
            Eclair,
            Froyo,
            Gingerbread,
            Honeycomb,
            IceCreamSandwich,
            JellyBean,
            KitKat,
            Lollipop,
            Marshmallow,
            Nougat,
            Oreo,
            Pie,
            AndroidQ,
            AndroidR
        }
        
        public static class WindowsVersions
        {
            private static readonly Dictionary<string, string> Versions = new Dictionary<string, string>
            {
                {"10.0","10"},
                {"6.3","8.1"},
                {"6.2","8"},
                {"6.1","7"},
                {"6.0","Vista"},
                {"5.2","XP Pro x64"},
                {"5.1","XP"},
                {"4.0","NT"},

            };

            public static string GetExtendedInfo(string version)
            {
                return Versions.ContainsKey(version) ? Versions[version] : version;
            }
        }
        
        private static string GetProductLine(string userAgent)
        {
            var index = userAgent.IndexOf('/');
            return index < 0 ? null : userAgent.Substring(0, index);
        }
        
        private static string WindowsOsNumberToName(string version)
        {
            return WindowsVersions.GetExtendedInfo(version);
        }
        
        private static DeviceInfo ParseMozillaWindows(string userAgent, string[] platformInfo)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Windows,
                Architecture = platformInfo.Any(itm => itm == "WOW64") ? ArchitectureType.X64 : ArchitectureType.X86,
                FormFactor = FormFactor.Desktop,
                OsVersion = WindowsOsNumberToName(platformInfo.First(itm => itm.StartsWith("Windows NT"))
                    .SubstringFromString("Windows NT "))
            };


            if (platformInfo.Any(itm => itm.StartsWith("Trident")) || platformInfo.Any(itm => itm.StartsWith(".NET")) ||
                platformInfo.Any(itm => itm.StartsWith("MSIE")))
            {
                var ieData = platformInfo.FirstOrDefault(itm => itm.StartsWith("MSIE"));
                if (ieData != null)
                {
                    result.AppHost = ApplicationHost.Ie;
                    result.AppVersion = ieData.SubstringFromString("MSIE ");
                    return result;
                }

                ieData = platformInfo.FirstOrDefault(itm => itm.StartsWith("rv:"));

                if (ieData != null)
                {
                    result.AppHost = ApplicationHost.Ie;
                    result.AppVersion = ieData.SubstringFromString("rv:");
                    return result;
                }
            }


            var browserDataString = userAgent.SubstringFromChar(')', 1);

            if (browserDataString == null)
            {
                browserDataString = userAgent.SubstringFromChar(')');

                var bd = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var bi = bd.FirstOrDefault(itm => itm.StartsWith("Firefox"));
                if (bi == null) return UnknownPlatform;
                
                result.AppHost = ApplicationHost.Firefox;
                result.AppVersion = bi.SubstringFromChar('/');
                return result;

            }


            var browserData = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("OPR"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Opera;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Chrome"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Chrome;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Version"));
            if (browserInfo == null) return result;
            
            result.AppHost = ApplicationHost.Safari;
            result.AppVersion = browserInfo.SubstringFromChar('/');
            return result;


        }

        private static DeviceInfo ParseIphoneIPad(string userAgent, string[] platformInfo, FormFactor formFactor)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Apple,
                FormFactor = formFactor,
                OsVersion = null
            };

            var browserData = userAgent.SubstringFromChar(')', 1)
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("CriOS"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Chrome;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("OPiOS"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Opera;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }


            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("UCBrowser"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.UcBrowser;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("MicroMessenger"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.WeChat;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Version"));
            if (browserInfo == null) return result;
            
            result.AppHost = ApplicationHost.Safari;
            result.AppVersion = browserInfo.SubstringFromChar('/');
            return result;


        }
        
        private static DeviceInfo ParseAndroid(string userAgent, string[] platformInfo)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Android,
                FormFactor = FormFactor.Mobile,
                OsVersion = WindowsOsNumberToName(platformInfo.First(itm => itm.StartsWith("Android"))
                    .SubstringFromString("Android "))
            };

            var browserDataString = userAgent.SubstringFromChar(')', 1);


            if (browserDataString == null)
            {
                browserDataString = userAgent.SubstringFromChar(')');

                var bd = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var bi = bd.FirstOrDefault(itm => itm.StartsWith("Firefox"));
                if (bi == null) return UnknownPlatform;
                
                result.AppHost = ApplicationHost.Firefox;
                result.AppVersion = bi.SubstringFromChar('/');
                return result;
            }


            var browserData = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Chrome"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Chrome;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Version"));
            if (browserInfo == null) return result;
            
            result.AppHost = ApplicationHost.Android;
            result.AppVersion = browserInfo.SubstringFromChar('/');
            return result;

        }
        
        private static DeviceInfo ParseWindowsPhone(string userAgent, string[] platformInfo)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Windows,
                FormFactor = FormFactor.Mobile,
                OsVersion = WindowsOsNumberToName(platformInfo.First(itm => itm.StartsWith("Windows Phone"))
                    .SubstringFromString("Windows Phone "))
            };

            var ieData = platformInfo.FirstOrDefault(itm => itm.StartsWith("IEMobile"));
            if (ieData == null) return result;
            
            result.AppHost = ApplicationHost.Ie;
            result.AppVersion = ieData.SubstringFromString("IEMobile/");
            return result;


        }
        
        private static DeviceInfo ParseAndroidTablet(string userAgent, string[] platformInfo)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Android,
                FormFactor = FormFactor.Tablet,
                OsVersion = null
            };

            var browserDataString = userAgent.SubstringFromChar(')', 1);


            if (browserDataString == null)
            {
                browserDataString = userAgent.SubstringFromChar(')');

                var bd = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var bi = bd.FirstOrDefault(itm => itm.StartsWith("Firefox"));
                if (bi == null) return UnknownPlatform;
                
                result.AppHost = ApplicationHost.Firefox;
                result.AppVersion = bi.SubstringFromChar('/');
                return result;

            }


            var browserData = browserDataString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Chrome"));
            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Chrome;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Version"));
            if (browserInfo == null) return result;
            
            result.AppHost = ApplicationHost.Android;
            result.AppVersion = browserInfo.SubstringFromChar('/');
            return result;

        }
        
        private static DeviceInfo ParseMacintosh(string userAgent, IEnumerable<string> platformInfo)
        {
            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Apple,
                FormFactor = FormFactor.Desktop,
                OsVersion = WindowsOsNumberToName(platformInfo.FirstOrDefault(itm => itm.Contains("Mac OS X")))
            };


            var browserData = userAgent.SubstringFromChar(')', 1)
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Chrome"));

            if (browserInfo != null)
            {
                result.AppHost = ApplicationHost.Chrome;
                result.AppVersion = browserInfo.SubstringFromChar('/');
                return result;
            }

            browserInfo = browserData.FirstOrDefault(itm => itm.StartsWith("Version"));

            if (browserInfo == null) return result;
            
            result.AppHost = ApplicationHost.Safari;
            result.AppVersion = browserInfo.SubstringFromChar('/');
            return result;

        }
        
        private static DeviceInfo ParseMozilla(string userAgent)
        {
            try
            {
                var platformInfo = userAgent.SubstringBetween('(', ')').Split(';').Select(itm => itm.Trim()).ToArray();

                if (platformInfo.Any(itm => itm.StartsWith("Windows NT")))
                    return ParseMozillaWindows(userAgent, platformInfo);

                if (platformInfo.Any(itm => itm.StartsWith("iPhone")))
                    return ParseIphoneIPad(userAgent, platformInfo, FormFactor.Mobile);

                if (platformInfo.Any(itm => itm.StartsWith("iPad")))
                    return ParseIphoneIPad(userAgent, platformInfo, FormFactor.Tablet);

                if (platformInfo.Any(itm => itm.StartsWith("Windows Phone")))
                    return ParseWindowsPhone(userAgent, platformInfo);

                if (platformInfo.Any(itm => itm.StartsWith("Android")) &&
                    platformInfo.Any(itm => itm.StartsWith("Linux")))
                    return ParseAndroid(userAgent, platformInfo);

                if (platformInfo.Any(itm => itm.StartsWith("Android")) &&
                    platformInfo.Any(itm => itm.StartsWith("Tablet")))
                    return ParseAndroidTablet(userAgent, platformInfo);

                if (platformInfo.Any(itm => itm.StartsWith("Macintosh")))
                    return ParseMacintosh(userAgent, platformInfo);
            }
            catch (Exception)
            {
                return UnknownPlatform;
            }

            return UnknownPlatform;
        }
        
        private static DeviceInfo ParseNative(string userAgent)
        {
            var platformInfo = userAgent.SubstringBetween('(', ')').Split(';').Select(itm => itm.Trim()).ToArray();

            var result = new DeviceInfo
            {
                FormFactor = FormFactor.Mobile,
                AppHost = ApplicationHost.Native
            };


            if (platformInfo.Any(itm => itm.StartsWith("WinPhone")))
            {
                result.PlatformType = PlatformType.Windows;
            }
            else if (platformInfo.Any(itm => itm.StartsWith("Android")))
            {
                result.PlatformType = PlatformType.Android;
            }

            return result;
        }
        
        private static DeviceInfo ParseLG_P503(string userAgent)
        {
            var data = userAgent.Split(' ');

            var result = new DeviceInfo
            {
                PlatformType = PlatformType.Android,
                FormFactor = FormFactor.Mobile,
                AppHost = ApplicationHost.Android,
            };


            var androidData = data.FirstOrDefault(itm => itm.StartsWith("Android"));
            if (androidData != null)
            {
                result.OsVersion = androidData.SubstringFromString("Android/");
            }

            return result;
        }
        
        public static DeviceInfo DetectPlatformByUserAgent(this string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return UnknownPlatform;

            var productLine = GetProductLine(userAgent);

            return productLine switch
            {
                null => UnknownPlatform,
                "Mozilla" => ParseMozilla(userAgent),
                "Opera" => userAgent.ParseOpera(),
                "ZebraFx.Mobile" => ParseNative(userAgent),
                "LG_P503" => ParseLG_P503(userAgent),
                _ => UnknownPlatform
            };
        }
        
        public static AndroidPlatformName GetAndroidVersionName(this string version)
        {
            if (version.StartsWith("1.5"))
                return AndroidPlatformName.Cupcake;

            if (version.StartsWith("1.6"))
                return AndroidPlatformName.Donut;

            if (version.StartsWith("2.0") || version.StartsWith("2.1"))
                return AndroidPlatformName.Eclair;

            if (version.StartsWith("2.2"))
                return AndroidPlatformName.Froyo;

            if (version.StartsWith("2.3"))
                return AndroidPlatformName.Gingerbread;

            if (version.StartsWith("3"))
                return AndroidPlatformName.Honeycomb;

            if (version.StartsWith("4.0"))
                return AndroidPlatformName.IceCreamSandwich;

            if (version.StartsWith("4.1") || version.StartsWith("4.2") || version.StartsWith("4.3"))
                return AndroidPlatformName.JellyBean;

            if (version.StartsWith("4.4"))
                return AndroidPlatformName.KitKat;

            if (version.StartsWith("5"))
                return AndroidPlatformName.Lollipop;

            if (version.StartsWith("6"))
                return AndroidPlatformName.Marshmallow;

            if (version.StartsWith("7"))
                return AndroidPlatformName.Nougat;

            if (version.StartsWith("8"))
                return AndroidPlatformName.Oreo;

            if (version.StartsWith("9"))
                return AndroidPlatformName.Pie;

            if (version.StartsWith("10"))
                return AndroidPlatformName.AndroidQ;
            
            if (version.StartsWith("11"))
                return AndroidPlatformName.AndroidR;

            return AndroidPlatformName.Unknown;
        }
    }
}