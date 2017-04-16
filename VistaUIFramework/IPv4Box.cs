using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class IPv4Box : System.Windows.Forms.TextBox {

        public IPv4Box() : base() {}

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassName = NativeMethods.WC_IPADDRESS;
                cp.ClassStyle = NativeMethods.CS_VREDRAW | NativeMethods.CS_HREDRAW | NativeMethods.CS_DBLCLKS | NativeMethods.CS_GLOBALCLASS;
                cp.ExStyle = NativeMethods.WS_EX_NOPARENTNOTIFY | NativeMethods.WS_EX_CLIENTEDGE;
                cp.Style = NativeMethods.WS_VISIBLE | NativeMethods.WS_CHILD;
                if (RightToLeft == RightToLeft.Yes) {
                    cp.ExStyle |= NativeMethods.WS_EX_LAYOUTRTL;
                    cp.ExStyle &= ~(NativeMethods.WS_EX_RIGHT | NativeMethods.WS_EX_RTLREADING | NativeMethods.WS_EX_LEFTSCROLLBAR);
                }
                return cp;
            }
        }

        [Browsable(true)]
        public new virtual ContextMenu ContextMenu {
            get {
                return base.ContextMenu;
            }
            set {
                base.ContextMenu = value;
            }
        }

    }
}
