using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpLib.Win32
{
    static public partial class Function
    {
        /// Return Type: LRESULT->LONG_PTR->int
        ///hWnd: HWND->HWND__*
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAsAttribute(UnmanagedType.SysInt)]
        public static extern int SendMessage([InAttribute()] IntPtr hWnd, uint Msg, [MarshalAsAttribute(UnmanagedType.SysUInt)] uint wParam, [MarshalAsAttribute(UnmanagedType.SysInt)] int lParam);


    }
}
