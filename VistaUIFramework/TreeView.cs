using System;
using System.Drawing;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ListView))]
    public class TreeView : System.Windows.Forms.TreeView {

        public TreeView() : base() {}

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            NativeMethods.SetWindowTheme(base.Handle, "explorer", null);
            int extended = NativeMethods.SendMessage(base.Handle, NativeMethods.TVM_GETEXTENDEDSTYLE, 0, 0).ToInt32();
            extended |= (NativeMethods.TVS_EX_AUTOHSCROLL | NativeMethods.TVS_EX_FADEINOUTEXPANDOS | NativeMethods.TVS_EX_DOUBLEBUFFER);
            NativeMethods.SendMessage(base.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, extended);
        }

    }
}