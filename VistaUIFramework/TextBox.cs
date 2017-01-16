using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class TextBox : System.Windows.Forms.TextBox {

        private string _Hint;

        public TextBox() : base() {}

        /// <summary>
        /// Set the TextBox's gray text when TextBox is empty
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Set the TextBox's gray text when TextBox is empty")]
        public string Hint {
            get {
                return _Hint;
            }
            set {
                _Hint = value;
                NativeMethods.SendMessageW(Handle, NativeMethods.EM_SETCUEBANNER, IntPtr.Zero, value);
            }
        }

    }
}
