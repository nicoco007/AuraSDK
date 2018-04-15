using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuraSDKDotNet;
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
            AuraSDK sdk = new AuraSDK();

            sdk.Unload();
        }

        [TestMethod]
        public void TestLoadUnloadRelativeDirectory()
        {
            AuraSDK sdk = new AuraSDK(@"lib\AURA_SDK_lib.dll");

            sdk.Unload();
        }

        [TestMethod]
        public void TestLoadUnloadEmptyPath()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new AuraSDK(""), "Path cannot be null or empty");
        }

        [TestMethod]
        public void TestLoadUnloadInvalidPath()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new AuraSDK("src/hello.dll"), "src/hello.dll not found");
        }

        [TestMethod]
        public void TestMotherboards()
        {
            AuraSDK sdk = new AuraSDK();

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
            AuraSDK sdk = new AuraSDK();

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
            AuraSDK sdk = new AuraSDK();

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
            AuraSDK sdk = new AuraSDK();

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
            AuraSDK sdk = new AuraSDK();

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
            AuraSDK sdk = new AuraSDK();

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
