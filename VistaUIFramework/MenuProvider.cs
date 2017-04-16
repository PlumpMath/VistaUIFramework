using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    internal class Properties {
        public Image Image;
        public IntPtr renderBmpHbitmap = IntPtr.Zero;
    }

    [ToolboxBitmap(typeof(MenuItem))]
    [ProvideProperty("Image", typeof(MenuItem))]
    public partial class MenuProvider : Component, IExtenderProvider, ISupportInitialize {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, MENUITEMINFO_T_RW lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetMenuInfo(HandleRef hMenu, MENUINFO lpcmi);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        Container components;
        readonly Hashtable properties = new Hashtable();
        readonly Hashtable menuParents = new Hashtable();

        bool formHasBeenIntialized;
        readonly bool isVistaOrLater;

        public MenuProvider() {
            isVistaOrLater = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;
            InitializeComponent();
        }

        public MenuProvider(IContainer container) : this() {
            container.Add(this);
        }
        void InitializeComponent() {
            components = new Container();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                //release all the HBitmap handles created
                foreach (DictionaryEntry de in properties) {
                    if (((Properties)de.Value).renderBmpHbitmap != IntPtr.Zero)
                        DeleteObject(((Properties)de.Value).renderBmpHbitmap);
                }


                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        bool IExtenderProvider.CanExtend(object o) {
            if (o is MenuItem) {
                // reject the menuitem if it's a top level element on a MainMenu bar
                if (((MenuItem)o).Parent != null)
                    return ((MenuItem)o).Parent.GetType() != typeof(MainMenu);

                // parent is null - meaning it's a context menu
                return true;
            }

            if (o is Form)
                return true;

            return false;
        }

        Properties EnsurePropertiesExists(MenuItem key) {
            Properties p = (Properties)properties[key];
            if (p == null) {
                p = new Properties();
                properties[key] = p;
            }
            return p;
        }

        [DefaultValue(null)]
        [Description("The Image for the MenuItem")]
        [Category("Appearance")]
        public Image GetImage(MenuItem mnuItem) {
            return EnsurePropertiesExists(mnuItem).Image;
        }

        [DefaultValue(null)]
        public void SetImage(MenuItem mnuItem, Image value) {
            Properties prop = EnsurePropertiesExists(mnuItem);
            prop.Image = value;
            if (!DesignMode && isVistaOrLater) {
                if (prop.renderBmpHbitmap != IntPtr.Zero) {
                    DeleteObject(prop.renderBmpHbitmap);
                    prop.renderBmpHbitmap = IntPtr.Zero;
                }
                if (value == null)
                    return;
                using (Bitmap renderBmp = new Bitmap(value.Width, value.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)) {
                    using (Graphics g = Graphics.FromImage(renderBmp))
                        g.DrawImage(value, 0, 0, value.Width, value.Height);
                    prop.renderBmpHbitmap = renderBmp.GetHbitmap(Color.FromArgb(0, 0, 0, 0));
                }
                if (formHasBeenIntialized)
                    AddMenuProviderItem(mnuItem);
            }
            if (!DesignMode && !isVistaOrLater && formHasBeenIntialized) {
                AddPreMenuProviderItem(mnuItem);
            }
        }

        void ISupportInitialize.BeginInit() { }

        MENUINFO mnuInfo = new MENUINFO();

        void AddMenuProviderItem(MenuItem mnuItem) {
            if (menuParents[mnuItem.Parent] == null) {
                if (mnuItem.Parent.GetType() == typeof(ContextMenu))
                    ((ContextMenu)mnuItem.Parent).Popup += MenuItem_Popup;
                else
                    ((MenuItem)mnuItem.Parent).Popup += MenuItem_Popup;
                SetMenuInfo(new HandleRef(null, mnuItem.Parent.Handle), mnuInfo);
                menuParents[mnuItem.Parent] = true;
            }
        }

        void AddPreMenuProviderItem(MenuItem mnuItem) {
            if (menuParents[mnuItem.Parent] == null) {
                menuParents[mnuItem.Parent] = true;

                if (formHasBeenIntialized) {
                    //add all the menu items with custom paint events
                    foreach (MenuItem menu in mnuItem.Parent.MenuItems) {
                        menu.DrawItem += MenuItem_DrawItem;
                        menu.MeasureItem += MenuItem_MeasureItem;
                        menu.OwnerDraw = true;
                    }
                }
            }
        }

        void ISupportInitialize.EndInit() {
            if (!DesignMode) {
                if (isVistaOrLater) {
                    foreach (DictionaryEntry de in properties) {
                        AddMenuProviderItem((MenuItem)de.Key);
                    }
                } else {
                    menuBoldFont = new Font(SystemFonts.MenuFont, FontStyle.Bold);


                    if (ownerForm != null)
                        ownerForm.ChangeUICues += ownerForm_ChangeUICues;

                    foreach (DictionaryEntry de in properties) {
                        AddPreMenuProviderItem((MenuItem)de.Key);
                    }

                    //add event handle for each menu item's measure & draw routines
                    foreach (DictionaryEntry parent in menuParents) {
                        foreach (MenuItem mnuItem in ((Menu)parent.Key).MenuItems) {
                            mnuItem.DrawItem += MenuItem_DrawItem;
                            mnuItem.MeasureItem += MenuItem_MeasureItem;
                            mnuItem.OwnerDraw = true;
                        }
                    }
                }

                formHasBeenIntialized = true;
            }
        }

        void MenuItem_Popup(object sender, EventArgs e) {
            MENUITEMINFO_T_RW menuItemInfo = new MENUITEMINFO_T_RW();
            Menu.MenuItemCollection mi = sender.GetType() == typeof(ContextMenu) ? ((ContextMenu)sender).MenuItems : ((MenuItem)sender).MenuItems;
            int miOn = 0;
            for (int i = 0; i < mi.Count; i++) {
                if (mi[i].Visible) {
                    Properties p = ((Properties)properties[mi[i]]);
                    if (p != null) {
                        menuItemInfo.hbmpItem = p.renderBmpHbitmap;
                        SetMenuItemInfo(new HandleRef(null, ((Menu)sender).Handle), miOn, true, menuItemInfo);
                    }
                    miOn++;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO_T_RW {
            public int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T_RW));
            public int fMask = 0x00000080; //MIIM_BITMAP = 0x00000080
            public int fType;
            public int fState;
            public int wID;
            public IntPtr hSubMenu = IntPtr.Zero;
            public IntPtr hbmpChecked = IntPtr.Zero;
            public IntPtr hbmpUnchecked = IntPtr.Zero;
            public IntPtr dwItemData = IntPtr.Zero;
            public IntPtr dwTypeData = IntPtr.Zero;
            public int cch;
            public IntPtr hbmpItem = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUINFO {
            public int cbSize = Marshal.SizeOf(typeof(MENUINFO));
            public int fMask = 0x00000010; //MIM_STYLE;
            public int dwStyle = 0x04000000; //MNS_CHECKORBMP;
            public uint cyMax;
            public IntPtr hbrBack = IntPtr.Zero;
            public int dwContextHelpID;
            public IntPtr dwMenuData = IntPtr.Zero;
        }

    }

}
