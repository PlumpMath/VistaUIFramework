using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    internal class NativeMethods {

        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0x0000;
        public const int MF_ENABLED = 0x0000;
        public const int MF_GRAYED = 0x0001;
        public const int IMAGE_BITMAP = 0;
        public const int IMAGE_ICON = 1;

        /* WM VARIABLES */
        public const int WM_USER = 0x0400;
        public const int WM_PAINT = 0x000F;

        /* BUTTON VARIABLES */
        public const int BCM_FIRST = 0x1600;
        public const int BCM_SETNOTE = BCM_FIRST + 0x0009;
        public const int BCM_SETDROPDOWNSTATE = BCM_FIRST + 0x0006;
        public const int BCM_SETSHIELD = BCM_FIRST + 0x000C;
        public const int BS_COMMANDLINK = 0x000E;
        public const int BS_DEFCOMMANDLINK = 0x000F;
        public const int BS_PUSHBUTTON = 0x0000;
        public const int BS_DEFPUSHBUTTON = 0x0001;
        public const int BS_SPLITBUTTON = 0x000C;
        public const int BS_DEFSPLITBUTTON = 0x000D;
        public const int BM_SETIMAGE = 0x00F7;

        /* PROGRESS BAR VARIABLES */
        public const int PBM_SETSTATE = WM_USER + 16;
        public const int PBS_SMOOTHREVERSE = 0x10;
        public const int PBST_NORMAL = 0x0001;
        public const int PBST_ERROR = 0x0002;
        public const int PBST_PAUSED = 0x0003;

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
