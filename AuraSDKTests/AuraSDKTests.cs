using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuraSDK;
using System.Diagnostics;
using System;

namespace AuraSDKTests
{
    [TestClass]
    public class AuraSDKTests
    {
        private Color[] testColors = new Color[] 
        {
            new Color(255, 0, 0),
            new Color(255, 127, 0),
            new Color(255, 255, 0),
            new Color(127, 255, 0),
            new Color(0, 255, 0),
            new Color(0, 255, 127),
            new Color(0, 255, 255),
            new Color(0, 127, 255),
            new Color(0, 0, 255),
            new Color(127, 0, 255),
            new Color(255, 0, 255),
            new Color(255, 0, 127)
        };

        [TestMethod]
        public void TestLoadUnload()
        {
            SDK sdk = new SDK();

            sdk.Unload();
        }

        [TestMethod]
        public void TestMotherboards()
        {
            SDK sdk = new SDK();

            if (sdk.Motherboards.Length == 0)
                Assert.Inconclusive();

            foreach (Motherboard motherboard in sdk.Motherboards)
            {
                motherboard.SetMode(DeviceMode.Software);

                Color[] colors = new Color[motherboard.LedCount];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = testColors[i % testColors.Length];
                }

                motherboard.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestMotherboardFailsIfNotEnoughColors()
        {
            SDK sdk = new SDK();

            if (sdk.Motherboards.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.Motherboards[0].LedCount + 1];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = testColors[i % testColors.Length];
            }

            Assert.ThrowsException<ArgumentException>(() => sdk.Motherboards[0].SetColors(colors));

            sdk.Unload();
        }

        [TestMethod]
        public void TestMotherboardFailsIfNullColor()
        {
            SDK sdk = new SDK();

            if (sdk.Motherboards.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.Motherboards[0].LedCount];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = null;
            }

            Assert.ThrowsException<ArgumentNullException>(() => sdk.Motherboards[0].SetColors(colors));

            sdk.Unload();
        }

        [TestMethod]
        public void TestGpus()
        {
            SDK sdk = new SDK();

            foreach (GPU gpu in sdk.GPUs)
            {
                gpu.SetMode(DeviceMode.Software);

                Color[] colors = new Color[gpu.LedCount];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = testColors[i % testColors.Length];
                }

                gpu.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestGpuFailsIfNotEnoughColors()
        {
            SDK sdk = new SDK();

            if (sdk.GPUs.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.GPUs[0].LedCount + 1];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = testColors[i % testColors.Length];
            }

            Assert.ThrowsException<ArgumentException>(() => sdk.GPUs[0].SetColors(colors));

            sdk.Unload();
        }

        [TestMethod]
        public void TestGpuFailsIfNullColor()
        {
            SDK sdk = new SDK();

            if (sdk.GPUs.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.GPUs[0].LedCount];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = null;
            }

            Assert.ThrowsException<ArgumentNullException>(() => sdk.GPUs[0].SetColors(colors));

            sdk.Unload();
        }
    }
}
