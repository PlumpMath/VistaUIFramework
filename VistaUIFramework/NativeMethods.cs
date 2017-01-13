using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    internal class NativeMethods {

        /// <summary>
        /// <code>BCM_FIRST</code> is
        /// </summary>
        public const int BCM_FIRST = 0x1600;
        public const int BCM_GETNOTE = BCM_FIRST + 0x000A;
        public const int BCM_GETNOTELENGTH = BCM_FIRST + 0x000B;
        public const int BCM_SETNOTE = BCM_FIRST + 0x0009;
        public const int BS_COMMANDLINK = 0x000E;
        public const int BS_DEFCOMMANDLINK = 0x000F;
        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0x0000;
        public const int MF_ENABLED = 0x0000;
        public const int MF_GRAYED = 0x0001;

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
    }
}
