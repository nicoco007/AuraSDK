using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AuraSDK
{
    public class SDK
    {
        public Motherboard[] Motherboards { get => motherboards; }
        public GPU[] GPUs { get => gpus; }

        private Motherboard[] motherboards;
        private GPU[] gpus;

        private IntPtr dllHandle = IntPtr.Zero;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int EnumerateMbControllerPointer(IntPtr handles, int size);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetMbModePointer(IntPtr handle, int mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetMbLedCountPointer(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetMbColorPointer(IntPtr handle, byte[] colors, int size);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int EnumerateGpuControllerPointer(IntPtr handles, int size);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetGpuModePointer(IntPtr handle, int mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetGpuLedCountPointer(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetGpuColorPointer(IntPtr handle, byte[] colors, int size);

        private EnumerateMbControllerPointer enumerateMbControllerPointer;
        private SetMbModePointer setMbModePointer;
        private GetMbLedCountPointer getMbLedCountPointer;
        private SetMbColorPointer setMbColorPointer;

        private EnumerateGpuControllerPointer enumerateGpuControllerPointer;
        private SetGpuModePointer setGpuModePointer;
        private GetGpuLedCountPointer getGpuLedCountPointer;
        private SetGpuColorPointer setGpuColorPointer;

        public SDK()
        {
            Load();
        }

        public void Reload()
        {
            Unload();
            Load();
        }

        private void Load()
        {
            if (!File.Exists("AURA_SDK.dll"))
                throw new FileNotFoundException("AURA_SDK.dll not found");

            dllHandle = NativeMethods.LoadLibrary("AURA_SDK.dll");

            enumerateMbControllerPointer = (EnumerateMbControllerPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "EnumerateMbController"), typeof(EnumerateMbControllerPointer));
            setMbModePointer = (SetMbModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetMbMode"), typeof(SetMbModePointer));
            getMbLedCountPointer = (GetMbLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "GetMbLedCount"), typeof(GetMbLedCountPointer));
            setMbColorPointer = (SetMbColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetMbColor"), typeof(SetMbColorPointer));

            enumerateGpuControllerPointer = (EnumerateGpuControllerPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "EnumerateGPU"), typeof(EnumerateGpuControllerPointer));
            setGpuModePointer = (SetGpuModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetGPUMode"), typeof(SetGpuModePointer));
            getGpuLedCountPointer = (GetGpuLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "GetGPULedCount"), typeof(GetGpuLedCountPointer));
            setGpuColorPointer = (SetGpuColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetGPUColor"), typeof(SetGpuColorPointer));

            LoadMotherboards();
            LoadGpus();
        }

        private void LoadMotherboards()
        {
            int controllerCount = EnumerateMbController(IntPtr.Zero, 0);

            IntPtr[] handles = Util.ArrayFromPointer(controllerCount, (pointer) => EnumerateMbController(pointer, controllerCount));

            motherboards = new Motherboard[controllerCount];

            for (int i = 0; i < controllerCount; i++)
            {
                motherboards[i] = new Motherboard(this, handles[i]);
            }
        }

        private void LoadGpus()
        {
            int controllerCount = EnumerateGpuController(IntPtr.Zero, 0);

            IntPtr[] handles = Util.ArrayFromPointer(controllerCount, (pointer) => EnumerateGpuController(pointer, controllerCount));

            gpus = new GPU[controllerCount];

            for (int i = 0; i < controllerCount; i++)
            {
                gpus[i] = new GPU(this, handles[i]);
            }
        }

        public void Unload()
        {
            if (dllHandle == IntPtr.Zero)
                return;

            while (NativeMethods.FreeLibrary(dllHandle)) ;
            dllHandle = IntPtr.Zero;

            motherboards = new Motherboard[0];
            gpus = new GPU[0];
        }

        public int EnumerateMbController(IntPtr handles, int size) => enumerateMbControllerPointer(handles, size);
        public void SetMbMode(IntPtr handle, int mode) => setMbModePointer(handle, mode);
        public int GetMbLedCount(IntPtr handle) => getMbLedCountPointer(handle);
        public void SetMbColor(IntPtr handle, byte[] colors, int size) => setMbColorPointer(handle, colors, size);

        public int EnumerateGpuController(IntPtr handles, int size) => enumerateGpuControllerPointer(handles, size);
        public void SetGpuMode(IntPtr handle, int mode) => setGpuModePointer(handle, mode);
        public int GetGpuLedCount(IntPtr handle) => getGpuLedCountPointer(handle);
        public void SetGpuColor(IntPtr handle, byte[] colors, int size) => setGpuColorPointer(handle, colors, size);
    }
}
