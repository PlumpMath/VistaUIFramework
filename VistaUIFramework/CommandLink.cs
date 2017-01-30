using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class CommandLink : Button {

        private string _Note;

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
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Note {
            get {
                return _Note;
            }
            set {
                _Note = value;
                NativeMethods.SendMessageW(Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, value);
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

        protected override Size DefaultSize {
            get {
                return new Size(200, 74);
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (_Note != null) Note = _Note;
        }

    }
}
