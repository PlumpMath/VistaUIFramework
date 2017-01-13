using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    public class Button : System.Windows.Forms.Button {
        
        public Button() : base() {
            base.FlatStyle = FlatStyle.System;
        }

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
