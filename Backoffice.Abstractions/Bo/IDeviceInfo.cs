namespace Backoffice.Abstractions.Bo
{
    public enum PlatformType
    {
        Unknown,
        Apple,
        Android,
        Windows,
        Symbian,
        BlackBerry
    }

    public enum FormFactor
    {
        Desktop,
        Mobile,
        Tablet,
        Unknown
    }

    public enum ApplicationHost
    {
        Unknown,
        Firefox,
        Opera,
        Chrome,
        Safari,
        UcBrowser,
        WeChat,
        Android,
        Ie,
        Native
    }


    public enum ArchitectureType
    {
        X64,
        X86,
        Unknown
    }

    public interface IDeviceInfo
    {
        PlatformType PlatformType { get; }
        FormFactor FormFactor { get; }
        ApplicationHost AppHost { get; }
        ArchitectureType Architecture { get; }
        string OsVersion { get; }
        string AppVersion { get; }
    }
    
    public class DeviceInfo : IDeviceInfo
    {
        public PlatformType PlatformType { get; set;}
        public FormFactor FormFactor { get; set;}
        public ApplicationHost AppHost { get; set;}
        public ArchitectureType Architecture { get; set;}
        public string OsVersion { get; set;}
        public string AppVersion { get; set;}
    }
}