using System;

namespace AuraSDK
{
    public abstract class AuraDevice
    {
        public int LedCount { get => ledCount; }

        protected SDK sdk;
        protected IntPtr handle;
        protected int ledCount;

        internal AuraDevice(SDK sdk, IntPtr handle)
        {
            this.sdk = sdk;
            this.handle = handle;
        }

        public abstract void SetMode(DeviceMode mode);
        public abstract void SetColors(Color[] colors);
    }
}
