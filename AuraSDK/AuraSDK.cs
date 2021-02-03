using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AuraSDKDotNet
{
    public class AuraSDK
    {
        /// <summary>
        /// Array of found motherboard controllers
        /// </summary>
        public Motherboard[] Motherboards { get => motherboards; }

        /// <summary>
        /// Array of found GPU controllers
        /// </summary>
        public GPU[] GPUs { get => gpus; }

        /// <summary>
        /// Array of found Keyboard controllers
        /// </summary>
        public Keyboard[] Keyboards { get => keyboards; }

        /// <summary>
        /// Array of found Mouse controllers
        /// </summary>
        public Mouse[] Mice { get => mice; }

        private Motherboard[] motherboards;
        private GPU[] gpus;
        private Keyboard[] keyboards;
        private Mouse[] mice;

        private IntPtr dllHandle = IntPtr.Zero;
        private string dllPath = "AURA_SDK.dll";

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

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int CreateClaymoreKeyboardPointer(out IntPtr handles);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetClaymoreKeyboardModePointer(IntPtr handle, int mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetClaymoreKeyboardLedCountPointer(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetClaymoreKeyboardColorPointer(IntPtr handle, byte[] colors, int size);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int CreateRogMousePointer(out IntPtr handles);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetRogMouseModePointer(IntPtr handle, int mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int RogMouseLedCountPointer(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetRogMouseColorPointer(IntPtr handle, byte[] colors, int size);


        private EnumerateMbControllerPointer enumerateMbControllerPointer;
        private SetMbModePointer setMbModePointer;
        private GetMbLedCountPointer getMbLedCountPointer;
        private SetMbColorPointer setMbColorPointer;

        private EnumerateGpuControllerPointer enumerateGpuControllerPointer;
        private SetGpuModePointer setGpuModePointer;
        private GetGpuLedCountPointer getGpuLedCountPointer;
        private SetGpuColorPointer setGpuColorPointer;

        private CreateClaymoreKeyboardPointer createClaymoreKeyboardPointer;
        private SetClaymoreKeyboardModePointer setClaymoreKeyboardModePointer;
        private GetClaymoreKeyboardLedCountPointer getClaymoreKeyboardLedCountPointer;
        private SetClaymoreKeyboardColorPointer setClaymoreKeyboardColorPointer;

        private CreateRogMousePointer createRogMousePointer;
        private SetRogMouseModePointer setRogMouseModePointer;
        private RogMouseLedCountPointer rogMouseLedCountPointer;
        private SetRogMouseColorPointer setRogMouseColorPointer;

        /// <summary>
        /// Creates a new instance of the SDK class.
        /// </summary>
        public AuraSDK()
        {
            Load("AURA_SDK.dll");
        }

        public AuraSDK(string path)
        {
            Load(path);
        }

        /// <summary>
        /// Reloads all controllers.
        /// </summary>
        public void Reload()
        {
            Unload();
            Load(dllPath);
        }

        private void Load(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("Path cannot be null or empty");

            string fileName = Path.GetFileName(path);
            string directory = Path.GetDirectoryName(path);

            if (!File.Exists(path))
                throw new FileNotFoundException(path + " not found");

            dllPath = path;

            if (!String.IsNullOrEmpty(directory))
                NativeMethods.SetDllDirectory(directory);
            else
                NativeMethods.SetDllDirectory(Directory.GetCurrentDirectory());

            dllHandle = NativeMethods.LoadLibrary(fileName);

            enumerateMbControllerPointer = (EnumerateMbControllerPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "EnumerateMbController"), typeof(EnumerateMbControllerPointer));
            setMbModePointer = (SetMbModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetMbMode"), typeof(SetMbModePointer));
            getMbLedCountPointer = (GetMbLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "GetMbLedCount"), typeof(GetMbLedCountPointer));
            setMbColorPointer = (SetMbColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetMbColor"), typeof(SetMbColorPointer));

            enumerateGpuControllerPointer = (EnumerateGpuControllerPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "EnumerateGPU"), typeof(EnumerateGpuControllerPointer));
            setGpuModePointer = (SetGpuModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetGPUMode"), typeof(SetGpuModePointer));
            getGpuLedCountPointer = (GetGpuLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "GetGPULedCount"), typeof(GetGpuLedCountPointer));
            setGpuColorPointer = (SetGpuColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetGPUColor"), typeof(SetGpuColorPointer));

            createClaymoreKeyboardPointer = (CreateClaymoreKeyboardPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "CreateClaymoreKeyboard"), typeof(CreateClaymoreKeyboardPointer));
            setClaymoreKeyboardModePointer = (SetClaymoreKeyboardModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetClaymoreKeyboardMode"), typeof(SetClaymoreKeyboardModePointer));
            getClaymoreKeyboardLedCountPointer = (GetClaymoreKeyboardLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "GetClaymoreKeyboardLedCount"), typeof(GetClaymoreKeyboardLedCountPointer));
            setClaymoreKeyboardColorPointer = (SetClaymoreKeyboardColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetClaymoreKeyboardColor"), typeof(SetClaymoreKeyboardColorPointer));

            createRogMousePointer = (CreateRogMousePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "CreateRogMouse"), typeof(CreateRogMousePointer));
            setRogMouseModePointer = (SetRogMouseModePointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetRogMouseMode"), typeof(SetRogMouseModePointer));
            rogMouseLedCountPointer = (RogMouseLedCountPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "RogMouseLedCount"), typeof(RogMouseLedCountPointer));
            setRogMouseColorPointer = (SetRogMouseColorPointer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(dllHandle, "SetRogMouseColor"), typeof(SetRogMouseColorPointer));


            LoadMotherboards();
            LoadGpus();
            LoadKeyboards();
            LoadMice();
        }

        private void LoadMice()
        {
            IntPtr handle = IntPtr.Zero;
            if(CreateRogMouse(out handle) > 0)
            {
                mice = new Mouse[1];
                mice[0] = new Mouse(this, handle);
            }
        }

        private void LoadKeyboards()
        {
            IntPtr handle = IntPtr.Zero;
            if(CreateClaymoreKeyboard(out handle) > 0)
            {
                keyboards = new Keyboard[1];
                keyboards[0] = new Keyboard(this, handle);
            }
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

        /// <summary>
        /// Unloads the SDK, removing all references to the DLL.
        /// </summary>
        public void Unload()
        {
            if (dllHandle == IntPtr.Zero)
                return;

            while (NativeMethods.FreeLibrary(dllHandle)) ;
            dllHandle = IntPtr.Zero;

            motherboards = new Motherboard[0];
            gpus = new GPU[0];
        }

        internal int EnumerateMbController(IntPtr handles, int size) => enumerateMbControllerPointer(handles, size);
        internal void SetMbMode(IntPtr handle, int mode) => setMbModePointer(handle, mode);
        internal int GetMbLedCount(IntPtr handle) => getMbLedCountPointer(handle);
        internal void SetMbColor(IntPtr handle, byte[] colors, int size) => setMbColorPointer(handle, colors, size);

        internal int EnumerateGpuController(IntPtr handles, int size) => enumerateGpuControllerPointer(handles, size);
        internal void SetGpuMode(IntPtr handle, int mode) => setGpuModePointer(handle, mode);
        internal int GetGpuLedCount(IntPtr handle) => getGpuLedCountPointer(handle);
        internal void SetGpuColor(IntPtr handle, byte[] colors, int size) => setGpuColorPointer(handle, colors, size);

        internal int CreateClaymoreKeyboard(out IntPtr handle) => createClaymoreKeyboardPointer(out handle);
        internal void SetClaymoreKeyboardMode(IntPtr handle, int mode) => setClaymoreKeyboardModePointer(handle, mode);
        internal int GetClaymoreKeyboardLedCount(IntPtr handle) => getClaymoreKeyboardLedCountPointer(handle);
        internal int SetClaymoreKeyboardColor(IntPtr handle, byte[] colors, int size) => setClaymoreKeyboardColorPointer(handle, colors, size);

        internal int CreateRogMouse(out IntPtr handle) => createRogMousePointer(out handle);
        internal void SetRogMouseMode(IntPtr handle, int mode) => setRogMouseModePointer(handle, mode);
        internal int RogMouseLedCount(IntPtr handle) => rogMouseLedCountPointer(handle);
        internal int SetRogMouseColor(IntPtr handle, byte[] colors, int size) => setRogMouseColorPointer(handle, colors, size);

    }
}
