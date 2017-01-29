using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework.Taskbar {
    public class TaskbarHelper {

        private NativeMethods.ITaskbarList3 taskbar;

        public enum TBPFLAG {
            TBPF_NOPROGRESS = 0,
            TBPF_INDETERMINATE = 0x1,
            TBPF_NORMAL = 0x2,
            TBPF_ERROR = 0x4,
            TBPF_PAUSED = 0x8
        }

        private TaskbarHelper() {
            taskbar = (NativeMethods.ITaskbarList3)new TaskbarInstance();
        }

        public void SetProgressValue(IntPtr Handle, double value, double max) {
            taskbar.SetProgressValue(Handle, (ulong)value, (ulong)max);
        }

        public void SetProgressState(IntPtr Handle, TBPFLAG state) {
            taskbar.SetProgressState(Handle, (NativeMethods.TBPFLAG)state);
        }

        [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
        [ClassInterface(ClassInterfaceType.None)]
        [ComImport()]
        private class TaskbarInstance {}

        /// <summary>
        /// Get the instance of the taskbar, if Windows version is earlier than Windows 7, an exception will be throwed. Call <code>isSupported</code> property to check if version is Windows 7 or later
        /// </summary>
        public TaskbarHelper Instance {
            get {
                if (!isSupported) {
                    throw new UnsupportedWindowsException("Windows 7");
                }
                return new TaskbarHelper();
            }
        }

        /// <summary>
        /// Check if Windows version supports taskbar methods.
        /// </summary>
        public bool isSupported {
            get {
                return Environment.OSVersion.Version >= new Version(6, 1);
            }
        }

    }
}