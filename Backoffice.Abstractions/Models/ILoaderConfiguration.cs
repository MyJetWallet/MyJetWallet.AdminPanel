using System;

namespace Backoffice.Abstractions.Models
{
    public interface ILoaderConfiguration : IDisposable
    {
        public bool IsEnable { get; }

        public void Enable();

        public void Disable();
    }

    public class LoaderConfiguration : ILoaderConfiguration
    {
        public bool IsEnable { get; private set; }

        public void Enable()
        {
            SetProperty(true);
        }

        public void Disable()
        {
            SetProperty(false);
        }

        private void SetProperty(bool isEnable)
        {
            IsEnable = isEnable;
        }

        public void Dispose()
        {
            SetProperty(false);
        }
    }
}
