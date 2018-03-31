using System;

namespace AuraSDK
{
    public class GPU : AuraDevice
    {
        public GPU(SDK sdk, IntPtr handle) : base(sdk, handle)
        {
            ledCount = sdk.GetGpuLedCount(handle);
        }

        public override void SetMode(DeviceMode mode)
        {
            sdk.SetGpuMode(handle, (int)mode);
        }

        public override void SetColors(Color[] colors)
        {
            if (colors.Length != LedCount)
                throw new ArgumentException(String.Format("Argument colors must have a length of {0}, got {1}", LedCount, colors.Length));

            byte[] array = new byte[colors.Length * 3];

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == null)
                    throw new ArgumentNullException("Colors array contains null value at position " + i);

                array[i * 3] = colors[i].R;
                array[i * 3 + 1] = colors[i].B;
                array[i * 3 + 2] = colors[i].G;
            }

            sdk.SetGpuColor(handle, array, array.Length);
        }

        public override void SetColors(byte[] colors)
        {
            if (colors.Length != LedCount * 3)
                throw new ArgumentException(String.Format("Argument colors must have a length of {0}, got {1}", LedCount * 3, colors.Length));

            sdk.SetGpuColor(handle, colors, colors.Length);
        }
    }
}
