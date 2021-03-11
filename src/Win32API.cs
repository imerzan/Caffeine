using System;
using System.Runtime.InteropServices;

namespace Caffeine
{
    internal static class Win32API
    {
        // Constants
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        private const uint KEYEVENTF_SCANCODE = 0x0008;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint INPUT_MOUSE = 0;
        private const uint INPUT_KEYBOARD = 1;
        private const uint INPUT_HARDWARE = 2;

        // Pinvokes
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint esFlags);
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [DllImport("user32.dll")]
        private static extern uint SendInput (uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(SystemMetric smIndex);

        // Private Wrapper Methods
        private static int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        private static int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        // Public Wrapper Methods
        public static void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED); // Prevent system sleep
        }
        public static void SendKey(byte key)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = INPUT_KEYBOARD;
            input[0].U.ki.wVk = key;
            SendInput((uint)input.Length, input, Marshal.SizeOf(input.GetType().GetElementType()) * input.Length);
        }
        public static void SetCursorPos(int xCoord, int yCoord)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = INPUT_MOUSE;
            input[0].U.mi.dx = CalculateAbsoluteCoordinateX(xCoord);
            input[0].U.mi.dy = CalculateAbsoluteCoordinateY(yCoord);
            input[0].U.mi.mouseData = 0;
            input[0].U.mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
            SendInput((uint)input.Length, input, Marshal.SizeOf(input.GetType().GetElementType()) * input.Length);
        }
    }
    internal struct LASTINPUTINFO
    {
        public uint cbSize;

        public uint dwTime;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct INPUT
    {
        public uint type;
        public InputUnion U;
    }
    [StructLayout(LayoutKind.Explicit)]
    internal struct InputUnion
    {
        [FieldOffset(0)]
        internal MOUSEINPUT mi;
        [FieldOffset(0)]
        internal KEYBDINPUT ki;
        [FieldOffset(0)]
        internal HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public ScanCodeShort wScan;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public uint uMsg;
        public short wParamL;
        public short wParamH;
    }
    public enum SystemMetric
    {
        SM_CXSCREEN = 0,
        SM_CYSCREEN = 1,
    }
    public enum ScanCodeShort : short
    {
        KEY_A = 30,
        KEY_B = 48,
        KEY_C = 46,
        KEY_D = 32,
        KEY_E = 18,
        KEY_F = 33,
        KEY_G = 34,
        KEY_H = 35,
        KEY_I = 23,
        KEY_J = 36,
        KEY_K = 37,
        KEY_L = 38,
        KEY_M = 50,
        KEY_N = 49,
        KEY_O = 24,
        KEY_P = 25,
        KEY_Q = 16,
        KEY_R = 19,
        KEY_S = 31,
        KEY_T = 20,
        KEY_U = 22,
        KEY_V = 47,
        KEY_W = 17,
        KEY_X = 45,
        KEY_Y = 21,
        KEY_Z = 44,
    }
}
