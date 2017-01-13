using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    public class CommandLink : Button {

        /// <summary>
        /// CommandLink is a button with green arrow and a description (<code>Note</code> property)
        /// </summary>
        public CommandLink() : base() {}

        /// <summary>
        /// The CommandLink summary
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The CommandLink summary")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", "System.Drawing.Design.UITypeEditor")]
        public string Note {
            get {
                StringBuilder title = new StringBuilder();
                int size = NativeMethods.SendMessage(Handle, NativeMethods.BCM_GETNOTELENGTH, 0, 0).ToInt32();
                if (size > 0) {
                    title = new StringBuilder(size + 1);
                    NativeMethods.SendMessage(Handle, NativeMethods.BCM_GETNOTE, new IntPtr(title.Capacity), title);
                }
                return title.ToString();
            }
            set {
                NativeMethods.SendMessage(Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, value);
            }
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style |= NativeMethods.BS_COMMANDLINK;
                if (IsDefault) {
                    cp.Style |= NativeMethods.BS_DEFCOMMANDLINK;
                }
                return cp;
            }
        }

    }
}
