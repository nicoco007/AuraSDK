using System;

namespace AuraSDK
{
    public class Motherboard : AuraDevice
    {
        public Motherboard(SDK sdk, IntPtr handle) : base(sdk, handle)
        {
            ledCount = sdk.GetMbLedCount(handle);
        }

        public override void SetMode(DeviceMode mode)
        {
            sdk.SetMbMode(handle, (int) mode);
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

            sdk.SetMbColor(handle, array, array.Length);
        }
    }
}
