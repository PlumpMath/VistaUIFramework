using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// It's a splitted button with an arrow
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class SplitButton : Button {

        public SplitButton() : base() {}

        /// <summary>
        /// It's triggered when split arrow is clicked
        /// </summary>
        public event SplitClickEventHandler SplitClick;

        /// <summary>
        /// Set the split button's menu, set the SplitClick e.cancel to true to prevent the menu from showing
        /// </summary>
        [Category("Behavior")]
        [DefaultValue((ContextMenu) null)]
        [Description("Set the split button's menu, set the SplitClick e.cancel to true to prevent the menu from showing")]
        public ContextMenu Menu {get; set;}

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~NativeMethods.BS_PUSHBUTTON;
                cp.Style |= NativeMethods.BS_SPLITBUTTON;
                if (IsDefault) {
                    cp.Style &= ~NativeMethods.BS_DEFPUSHBUTTON;
                    cp.Style |= NativeMethods.BS_DEFSPLITBUTTON;
                }
                return cp;
            }
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case (NativeMethods.BCM_SETDROPDOWNSTATE):
                    if (m.HWnd==Handle && m.WParam.ToInt32()==1) {
                        if (SplitClick != null) {
                            SplitClickEventArgs e = new SplitClickEventArgs(Menu);
                            SplitClick(this, e);
                            if (!e.Cancel && Menu != null) {
                                Menu.Show(this, new Point(0, Height));
                            }
                        } else {
                            Menu.Show(this, new Point(0, Height));
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        protected override Size DefaultSize {
            get {
                return new Size(108, base.DefaultSize.Height);
            }
        }



    }
}
