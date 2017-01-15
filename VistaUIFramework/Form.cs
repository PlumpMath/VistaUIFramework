using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    public class Form : System.Windows.Forms.Form {
        private bool _CloseBox = true;
        private bool _Aero;
        private Padding _AeroMargin;

        public Form() : base() {}

        /// <summary>
        /// Set if close button is enabled
        /// </summary>
        [Category("WindowStyle")]
        [DefaultValue(true)]
        [Description("Set if close button is enabled")]
        public bool CloseBox {
            get {
                return _CloseBox;
            }
            set {
                _CloseBox = value;
                EnableCloseButton(value);
            }
        }

        /// <summary>
        /// Set the aero glass to the form
        /// </summary>
        [Category("WindowStyle")]
        [DefaultValue(false)]
        [Description("Set the aero glass to the form")]
        public bool Aero {
            get {
                return _Aero;
            }
            set {
                _Aero = value;
                RecreateHandle();
            }
        }

        [Category("Design")]
        [Description("The margins between form container and Aero glass")]
        public Padding AeroMargin {
            get {
                return _AeroMargin;
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (Aero && NativeMethods.DwmIsCompositionEnabled()) {
                NativeMethods.MARGINS margins = new NativeMethods.MARGINS();
                margins.topHeight = 20;
                margins.leftWidth = 20;
                NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        private void EnableCloseButton(bool enable) {
            IntPtr hMenu = NativeMethods.GetSystemMenu(Handle, false);
            if (hMenu != IntPtr.Zero) {
                NativeMethods.EnableMenuItem(hMenu, NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | (enable ? NativeMethods.MF_ENABLED : NativeMethods.MF_GRAYED));
            }
        }

    }
}
