using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    public class UnsupportedWindowsException : Exception {

        public UnsupportedWindowsException() : base() {}
        public UnsupportedWindowsException(string os) : base("It requires" + os + " or later") {}

    }
}
