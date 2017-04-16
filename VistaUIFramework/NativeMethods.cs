using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    internal class NativeMethods {

        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0x0000;
        public const int MF_ENABLED = 0x0000;
        public const int MF_GRAYED = 0x0001;
        public const int CS_NOCLOSE = 0x0200;
        public const int IMAGE_BITMAP = 0;
        public const int IMAGE_ICON = 1;
        public const string WC_IPADDRESS = "SysIPAddress32";

        /* WM AND WS VARIABLES */
        public const int WM_USER = 0x0400;
        public const int WM_PAINT = 0x000F;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_LAYOUTRTL = 0x00400000;
        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;

        /* EDIT VARIABLES */
        public const int ECM_FIRST = 0x1500;
        public const int EM_SETCUEBANNER = ECM_FIRST + 1;

        /* CLASS VARIABLES */
        public const int CS_VREDRAW = 0x0001;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_GLOBALCLASS = 0x4000;

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
        public const int BS_ICON = 0x00000040;
        public const int BM_SETIMAGE = 0x00F7;

        /* PROGRESS BAR VARIABLES */
        public const int PBM_SETSTATE = WM_USER + 16;
        public const int PBS_SMOOTHREVERSE = 0x10;
        public const int PBST_NORMAL = 0x0001;
        public const int PBST_ERROR = 0x0002;
        public const int PBST_PAUSED = 0x0003;

        /* LISTVIEW VARIABLES */
        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        public const int LVS_EX_DOUBLEBUFFER = 0x00010000;

        /* TREEVIEW VARIABLES */
        public const int TV_FIRST = 0x1100;
        public const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        public const int TVS_EX_AUTOHSCROLL = 0x0020;
        public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        public const int TVS_EX_DOUBLEBUFFER = 0x0004;

        /* MENU VARIABLES */
        public const int MIM_STYLE = 0x00000010;
        public const int MNS_CHECKORBMP = 0x04000000;
        public const int MIIM_BITMAP = 0x00000080;


        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        public enum THUMBBUTTONFLAGS {
            THBF_ENABLED = 0,
            THBF_DISABLED = 0x1,
            THBF_DISMISSONCLICK = 0x2,
            THBF_NOBACKGROUND = 0x4,
            THBF_HIDDEN = 0x8,
            THBF_NONINTERACTIVE = 0x10
        }

        public enum THUMBBUTTONMASK {
            THB_BITMAP = 0x1,
            THB_ICON = 0x2,
            THB_TOOLTIP = 0x4,
            THB_FLAGS = 0x8
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
        public struct THUMBBUTTON {
            public THUMBBUTTONMASK dwMask;
            public int iId;
            public int iBitmap;
            public IntPtr hIcon;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 259)]
            public string szTip;

            public THUMBBUTTONFLAGS dwFlags;
        }

        /// <summary>
        /// Extends ITaskbarList2 by exposing methods that support the unified launching and switching taskbar button
        /// functionality added in Windows 7. This functionality includes thumbnail representations and switch
        /// targets based on individual tabs in a tabbed application, thumbnail toolbars, notification and
        /// status overlays, and progress indicators.
        /// </summary>
        [ComImport,
        Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITaskbarList3 {
            // ITaskbarList

            /// <summary>
            /// Initializes the taskbar list object. This method must be called before any other ITaskbarList methods can be called.
            /// </summary>
            void HrInit();

            /// <summary>
            /// Adds an item to the taskbar.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be added to the taskbar.</param>
            void AddTab(IntPtr hWnd);

            /// <summary>
            /// Deletes an item from the taskbar.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be deleted from the taskbar.</param>
            void DeleteTab(IntPtr hWnd);

            /// <summary>
            /// Activates an item on the taskbar. The window is not actually activated; the window's item on the taskbar is merely displayed as active.
            /// </summary>
            /// <param name="hWnd">A handle to the window on the taskbar to be displayed as active.</param>
            void ActivateTab(IntPtr hWnd);

            /// <summary>
            /// Marks a taskbar item as active but does not visually activate it.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be marked as active.</param>
            void SetActiveAlt(IntPtr hWnd);

            // ITaskbarList2

            /// <summary>
            /// Marks a window as full-screen
            /// </summary>
            /// <param name="hWnd"></param>
            /// <param name="fFullscreen"></param>
            void MarkFullscreenWindow(IntPtr hWnd, int fFullscreen);

            /// <summary>
            /// Displays or updates a progress bar hosted in a taskbar button to show the specific percentage
            /// completed of the full operation.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose associated taskbar button is being used as
            /// a progress indicator.</param>
            /// <param name="ullCompleted">An application-defined value that indicates the proportion of the
            /// operation that has been completed at the time the method is called.</param>
            /// <param name="ullTotal">An application-defined value that specifies the value ullCompleted will
            /// have when the operation is complete.</param>
            void SetProgressValue(IntPtr hWnd, ulong ullCompleted, ulong ullTotal);

            /// <summary>
            /// Sets the type and state of the progress indicator displayed on a taskbar button.
            /// </summary>
            /// <param name="hWnd">The handle of the window in which the progress of an operation is being
            /// shown. This window's associated taskbar button will display the progress bar.</param>
            /// <param name="tbpFlags">Flags that control the current state of the progress button. Specify
            /// only one of the following flags; all states are mutually exclusive of all others.</param>
            void SetProgressState(IntPtr hWnd, TBPFLAG tbpFlags);

            /// <summary>
            /// Informs the taskbar that a new tab or document thumbnail has been provided for display in an
            /// application's taskbar group flyout.
            /// </summary>
            /// <param name="hWndTab">Handle of the tab or document window. This value is required and cannot
            /// be NULL.</param>
            /// <param name="hWndMDI">Handle of the application's main window. This value tells the taskbar
            /// which application's preview group to attach the new thumbnail to. This value is required and
            /// cannot be NULL.</param>
            void RegisterTab(IntPtr hWndTab, IntPtr hWndMDI);

            /// <summary>
            /// Removes a thumbnail from an application's preview group when that tab or document is closed in the application.
            /// </summary>
            /// <param name="hWndTab">The handle of the tab window whose thumbnail is being removed. This is the same
            /// value with which the thumbnail was registered as part the group through ITaskbarList3::RegisterTab.
            /// This value is required and cannot be NULL.</param>
            void UnregisterTab(IntPtr hWndTab);

            /// <summary>
            /// Inserts a new thumbnail into a tabbed-document interface (TDI) or multiple-document interface
            /// (MDI) application's group flyout or moves an existing thumbnail to a new position in the
            /// application's group.
            /// </summary>
            /// <param name="hWndTab">The handle of the tab window whose thumbnail is being placed. This value
            /// is required, must already be registered through ITaskbarList3::RegisterTab, and cannot be NULL.</param>
            /// <param name="hWndInsertBefore">The handle of the tab window whose thumbnail that hwndTab is
            /// inserted to the left of. This handle must already be registered through ITaskbarList3::RegisterTab.
            /// If this value is NULL, the new thumbnail is added to the end of the list.</param>
            void SetTabOrder(IntPtr hWndTab, IntPtr hWndInsertBefore);

            /// <summary>
            /// Informs the taskbar that a tab or document window has been made the active window.
            /// </summary>
            /// <param name="hWndTab">Handle of the active tab window. This handle must already be registered
            /// through ITaskbarList3::RegisterTab. This value can be NULL if no tab is active.</param>
            /// <param name="hWndMDI">Handle of the application's main window. This value tells the taskbar
            /// which group the thumbnail is a member of. This value is required and cannot be NULL.</param>
            /// <param name="tbatFlags">None, one, or both of the following values that specify a thumbnail
            /// and peek view to use in place of a representation of the specific tab or document.</param>
            void SetTabActive(IntPtr hWndTab, IntPtr hWndMDI, int tbatFlags);

            /// <summary>
            /// Adds a thumbnail toolbar with a specified set of buttons to the thumbnail image of a window in a
            /// taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose thumbnail representation will receive the toolbar.
            /// This handle must belong to the calling process.</param>
            /// <param name="cButtons">The number of buttons defined in the array pointed to by pButton. The maximum
            /// number of buttons allowed is 7.</param>
            /// <param name="pButton">A pointer to an array of THUMBBUTTON structures. Each THUMBBUTTON defines an
            /// individual button to be added to the toolbar. Buttons cannot be added or deleted later, so this must
            /// be the full defined set. Buttons also cannot be reordered, so their order in the array, which is the
            /// order in which they are displayed left to right, will be their permanent order.</param>
            void ThumbBarAddButtons(
            IntPtr hWnd,
            int cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButton);

            void ThumbBarUpdateButtons(
            IntPtr hWnd,
            int cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButton);

            /// <summary>
            /// Specifies an image list that contains button images for a toolbar embedded in a thumbnail image of a
            /// window in a taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose thumbnail representation contains the toolbar to be
            /// updated. This handle must belong to the calling process.</param>
            /// <param name="himl">The handle of the image list that contains all button images to be used in the toolbar.</param>
            void ThumbBarSetImageList(IntPtr hWnd, IntPtr himl);

            /// <summary>
            /// Applies an overlay to a taskbar button to indicate application status or a notification to the user.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose associated taskbar button receives the overlay.
            /// This handle must belong to a calling process associated with the button's application and must be
            /// a valid HWND or the call is ignored.</param>
            /// <param name="hIcon">The handle of an icon to use as the overlay. This should be a small icon,
            /// measuring 16x16 pixels at 96 dots per inch (dpi). If an overlay icon is already applied to the
            /// taskbar button, that existing overlay is replaced.</param>
            /// <param name="pszDescription">A pointer to a string that provides an alt text version of the
            /// information conveyed by the overlay, for accessibility purposes.</param>
            void SetOverlayIcon(IntPtr hWnd, IntPtr hIcon, string pszDescription);

            /// <summary>
            /// Specifies or updates the text of the tooltip that is displayed when the mouse pointer rests on an
            /// individual preview thumbnail in a taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle to the window whose thumbnail displays the tooltip. This handle must
            /// belong to the calling process.</param>
            /// <param name="pszTip">The pointer to the text to be displayed in the tooltip. This value can be NULL,
            /// in which case the title of the window specified by hwnd is used as the tooltip.</param>
            void SetThumbnailTooltip(IntPtr hWnd, string pszTip);

            /// <summary>
            /// Selects a portion of a window's client area to display as that window's thumbnail in the taskbar.
            /// </summary>
            /// <param name="hWnd">The handle to a window represented in the taskbar.</param>
            /// <param name="prcClip">A pointer to a RECT structure that specifies a selection within the window's
            /// client area, relative to the upper-left corner of that client area. To clear a clip that is already
            /// in place and return to the default display of the thumbnail, set this parameter to NULL.</param>
            void SetThumbnailClip(IntPtr hWnd, IntPtr prcClip);
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

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, int pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, int pszSubIdList);
    }
}
