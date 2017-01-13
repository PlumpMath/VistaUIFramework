using System;
using System.ComponentModel;

namespace MyAPKapp.VistaUIFramework {
    public class Form : System.Windows.Forms.Form {
        private bool _CloseBox = true;
        private bool _Aero;

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

        [Category("WindowStyle")]
        [DefaultValue(false)]
        [Description("Set if close button is enabled")]
        public bool Aero {
            get {
                return _Aero;
            }
            set {
                _Aero = value;
                RecreateHandle();
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (Aero && NativeMethods.DwmIsCompositionEnabled()) {
                NativeMethods.MARGINS margins = new NativeMethods.MARGINS();
                margins.topHeight = 20;
                margins.leftWidth = 20;
                NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
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
