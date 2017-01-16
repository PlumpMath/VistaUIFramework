using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
    public class ComboBox : System.Windows.Forms.ComboBox {

        public ComboBox() : base() {
            base.FlatStyle = FlatStyle.System;
        }

        /// <summary>
        /// This property can alter the native purpose of VistaUI
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("This property can alter the native purpose of VistaUI")]
        [DefaultValue(typeof(FlatStyle), "System")]
        public new FlatStyle FlatStyle {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

    }
}
