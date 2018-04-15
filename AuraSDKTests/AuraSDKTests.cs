using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuraSDK;
using System;
using System.IO;
using System.Diagnostics;

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
        public void TestLoadUnloadRelativeDirectory()
        {
            SDK sdk = new SDK(@"lib\AURA_SDK_lib.dll");

            sdk.Unload();
        }

        [TestMethod]
        public void TestLoadUnloadEmptyPath()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SDK(""), "Path cannot be null or empty");
        }

        [TestMethod]
        public void TestLoadUnloadInvalidPath()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new SDK("src/hello.dll"), "src/hello.dll not found");
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
        public void TestMotherboardByteColors()
        {
            SDK sdk = new SDK();

            if (sdk.Motherboards.Length == 0)
                Assert.Inconclusive();

            foreach (Motherboard motherboard in sdk.Motherboards)
            {
                motherboard.SetMode(DeviceMode.Software);

                byte[] colors = new byte[motherboard.LedCount * 3];

                for (int i = 0; i < motherboard.LedCount; i++)
                {
                    Color color = testColors[i % testColors.Length];
                    colors[i * 3] = color.R;
                    colors[i * 3 + 1] = color.B;
                    colors[i * 3 + 2] = color.G;
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
        public void TestMotherboardGpuColors()
        {
            SDK sdk = new SDK();

            if (sdk.GPUs.Length == 0)
                Assert.Inconclusive();

            foreach (GPU gpu in sdk.GPUs)
            {
                gpu.SetMode(DeviceMode.Software);

                byte[] colors = new byte[gpu.LedCount * 3];

                for (int i = 0; i < gpu.LedCount; i++)
                {
                    Color color = testColors[i % testColors.Length];
                    colors[i * 3] = color.R;
                    colors[i * 3 + 1] = color.B;
                    colors[i * 3 + 2] = color.G;
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
    }
}
