﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuraSDKDotNet
{
    public class Mouse : AuraDevice
    {
        internal Mouse(AuraSDK sdk, IntPtr handle) : base(sdk, handle)
        {
            ledCount = sdk.RogMouseLedCount(handle);
        }

        /// <summary>
        /// Set the device's RGB mode (currently only default/automatic or software).
        /// </summary>
        /// <param name="mode"></param>
        public override void SetMode(DeviceMode mode)
        {
            sdk.SetRogMouseMode(handle, (int)mode);
        }

        /// <summary>
        /// Set the device's colors. There must be the same number of colors as there are zones on the device.
        /// </summary>
        /// <param name="colors">Colors of the different zones</param>
        public override void SetColors(Color[] colors)
        {
            if (colors.Length != LedCount)
                throw new ArgumentException(String.Format("Argument colors must have a length of {0}, got {1}", LedCount, colors.Length));

            byte[] array = new byte[colors.Length * 3];

            for (int i = 0; i < colors.Length; i++)
            {
                array[i * 3] = colors[i].R;
                array[i * 3 + 1] = colors[i].B;
                array[i * 3 + 2] = colors[i].G;
            }

            sdk.SetRogMouseColor(handle, array, array.Length);
        }

        /// <summary>
        /// Set the device's colors. There must be the same number of colors as there are zones on the device times 3 bytes.
        /// </summary>
        /// <param name="colors">Colors of the different zones as bytes in RBG order</param>
        public override void SetColors(byte[] colors)
        {
            if (colors.Length != LedCount * 3)
                throw new ArgumentException(String.Format("Argument colors must have a length of {0}, got {1}", LedCount * 3, colors.Length));

            sdk.SetRogMouseColor(handle, colors, colors.Length);
        }
    }
}
