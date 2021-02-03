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
                    colors[i] = new Color(0, 255, 0);
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

        [TestMethod]
        public void TestKeyboards()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Keyboards.Length == 0)
                Assert.Inconclusive();

            foreach (Keyboard keyboard in sdk.Keyboards)
            {
                keyboard.SetMode(DeviceMode.Software);

                Color[] colors = new Color[keyboard.LedCount];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = testColors[i % testColors.Length];
                }

                keyboard.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestKeyboardByteColors()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Keyboards.Length == 0)
                Assert.Inconclusive();

            foreach (Keyboard keyboard in sdk.Keyboards)
            {
                keyboard.SetMode(DeviceMode.Software);

                byte[] colors = new byte[keyboard.LedCount * 3];

                for (int i = 0; i < keyboard.LedCount; i++)
                {
                    Color color = testColors[i % testColors.Length];
                    colors[i * 3] = color.R;
                    colors[i * 3 + 1] = color.B;
                    colors[i * 3 + 2] = color.G;
                }

                keyboard.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestKeyboardFailsIfNotEnoughColors()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Keyboards.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.Keyboards[0].LedCount + 1];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = testColors[i % testColors.Length];
            }

            Assert.ThrowsException<ArgumentException>(() => sdk.Keyboards[0].SetColors(colors));

            sdk.Unload();
        }

        [TestMethod]
        public void TestMice()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Mice.Length == 0)
                Assert.Inconclusive();

            foreach (Mouse mouse in sdk.Mice)
            {
                mouse.SetMode(DeviceMode.Software);


                Color[] colors = new Color[mouse.LedCount];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = testColors[i % testColors.Length];
                }

                mouse.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestMouseByteColors()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Mice.Length == 0)
                Assert.Inconclusive();

            foreach (Mouse mouse in sdk.Mice)
            {
                mouse.SetMode(DeviceMode.Software);

                byte[] colors = new byte[mouse.LedCount * 3];

                for (int i = 0; i < mouse.LedCount; i++)
                {
                    Color color = testColors[i % testColors.Length];
                    colors[i * 3] = color.R;
                    colors[i * 3 + 1] = color.B;
                    colors[i * 3 + 2] = color.G;
                }

                mouse.SetColors(colors);
            }

            sdk.Unload();
        }

        [TestMethod]
        public void TestMouseFailsIfNotEnoughColors()
        {
            AuraSDK sdk = new AuraSDK();

            if (sdk.Mice.Length == 0)
                Assert.Inconclusive();

            Color[] colors = new Color[sdk.Mice[0].LedCount + 1];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = testColors[i % testColors.Length];
            }

            Assert.ThrowsException<ArgumentException>(() => sdk.Mice[0].SetColors(colors));

            sdk.Unload();
        }
    }
}
