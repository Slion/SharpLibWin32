//
// Copyright (C) 2014-2015 St�phane Lenclud.
//
// This file is part of SharpLibWin32.
//
// SharpDisplayManager is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SharpDisplayManager is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with SharpDisplayManager.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Runtime.InteropServices;

namespace SharpLib.Win32
{

    static public partial class Function
    {
        [DllImport("User32.dll", SetLastError = true)]
        public extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll", SetLastError = true)]
        public extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("User32.dll", SetLastError = true)]
        public extern static int GetRawInputDeviceInfo(IntPtr hDevice, RawInputDeviceInfoType uiCommand, IntPtr pData, ref uint pcbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetRawInputDeviceList(
            [In, Out] RAWINPUTDEVICELIST[] InputdeviceList,
            [In, Out] ref uint puiNumDevices,
            [In] uint cbSize);

    }


    static public partial class Macro
    {
        /// <summary>
        /// Retrieves the input code from wParam in WM_INPUT.
        /// See RIM_INPUT and RIM_INPUTSINK.
        /// </summary>
        /// <param name="wParam"></param>
        /// <returns></returns>
        public static int GET_RAWINPUT_CODE_WPARAM(IntPtr wParam)
        {
            return (wParam.ToInt32() & 0xff);
        }

        public static int GET_DEVICE_LPARAM(IntPtr lParam)
        {
            return ((ushort)(HIWORD(lParam.ToInt32()) & Const.FAPPCOMMAND_MASK));
        }

        public static int HIWORD(int val)
        {
            return ((val >> 16) & 0xffff);
        }


        //#define HIWORD(l)           ((WORD)((((DWORD_PTR)(l)) >> 16) & 0xffff))
        //#define LOWORD(l)           ((WORD)(((DWORD_PTR)(l)) & 0xffff))        
        //#define LOBYTE(w)           ((BYTE)(((DWORD_PTR)(w)) & 0xff))
        //#define HIBYTE(w)           ((BYTE)((((DWORD_PTR)(w)) >> 8) & 0xff))

        //#define GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
        //#define GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
        //#define GET_MOUSEORKEY_LPARAM         GET_DEVICE_LPARAM
        //#define GET_FLAGS_LPARAM(lParam)      (LOWORD(lParam))
        //#define GET_KEYSTATE_LPARAM(lParam)   GET_FLAGS_LPARAM(lParam)

    }



    static public partial class Const
    {
        //
        public const int RID_INPUT = 0x10000003;
        public const int RID_HEADER = 0x10000005;

        /// <summary>
        /// Possible value taken by wParam for WM_INPUT.
        /// <para />
        /// Input occurred while the application was in the foreground. The application must call DefWindowProc so the system can perform cleanup.
        /// </summary>
        public const int RIM_INPUT = 0;
        /// <summary>
        /// Possible value taken by wParam for WM_INPUT.
        /// <para />
        /// Input occurred while the application was not in the foreground. The application must call DefWindowProc so the system can perform the cleanup.
        /// </summary>
        public const int RIM_INPUTSINK = 1;

        /// <summary>
        /// This value can be taken by RAWKEYBOARD.MakeCode.
        /// </summary>
        public const ushort KEYBOARD_OVERRUN_MAKE_CODE = 0x00FF;

        public const int APPCOMMAND_BROWSER_BACKWARD = 1;
        public const int APPCOMMAND_VOLUME_MUTE = 8;
        public const int APPCOMMAND_VOLUME_DOWN = 9;
        public const int APPCOMMAND_VOLUME_UP = 10;
        public const int APPCOMMAND_MEDIA_NEXTTRACK = 11;
        public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 12;
        public const int APPCOMMAND_MEDIA_STOP = 13;
        public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 14;
        public const int APPCOMMAND_MEDIA_PLAY = 46;
        public const int APPCOMMAND_MEDIA_PAUSE = 47;
        public const int APPCOMMAND_MEDIA_RECORD = 48;
        public const int APPCOMMAND_MEDIA_FAST_FORWARD = 49;
        public const int APPCOMMAND_MEDIA_REWIND = 50;
        public const int APPCOMMAND_MEDIA_CHANNEL_UP = 51;
        public const int APPCOMMAND_MEDIA_CHANNEL_DOWN = 52;

        public const int FAPPCOMMAND_MASK = 0xF000;
        public const int FAPPCOMMAND_MOUSE = 0x8000;
        public const int FAPPCOMMAND_KEY = 0;
        public const int FAPPCOMMAND_OEM = 0x1000;
    }


    /// <summary>
    /// Mode flag that specifies how to interpret the information provided by usUsagePage and usUsage.
    /// It can be zero (the default) or one of the following values.
    /// By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.
    /// </summary>
    [Flags]
    public enum RawInputDeviceFlags : uint
    {
        /// <summary>
        /// If set, the application command keys are handled. RIDEV_APPKEYS can be specified only if RIDEV_NOLEGACY is specified for a keyboard device.
        /// </summary>
        RIDEV_APPKEYS = 0x00000400,

        /// <summary>
        /// If set, the mouse button click does not activate the other window.
        /// </summary>
        RIDEV_CAPTUREMOUSE = 0x00000200,

        /// <summary>
        /// If set, this enables the caller to receive WM_INPUT_DEVICE_CHANGE notifications for device arrival and device removal.
        /// Windows XP:  This flag is not supported until Windows Vista
        /// </summary>
        RIDEV_DEVNOTIFY = 0x00002000,

        /// <summary>
        /// If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with RIDEV_PAGEONLY.
        /// </summary>
        RIDEV_EXCLUDE = 0x00000010,

        /// <summary>
        /// If set, this enables the caller to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.
        /// Windows XP:  This flag is not supported until Windows Vista
        /// </summary>
        RIDEV_EXINPUTSINK = 0x00001000,

        /// <summary>
        /// If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that hwndTarget must be specified.
        /// </summary>
        RIDEV_INPUTSINK = 0x00000100,

        /// <summary>
        /// If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. RIDEV_NOHOTKEYS can be specified even if RIDEV_NOLEGACY is not specified and hwndTarget is NULL.
        /// </summary>
        RIDEV_NOHOTKEYS = 0x00000200,

        /// <summary>
        /// If set, this prevents any devices specified by usUsagePage or usUsage from generating legacy messages. This is only for the mouse and keyboard. See Remarks.
        /// </summary>
        RIDEV_NOLEGACY = 0x00000030,

        /// <summary>
        /// If set, this specifies all devices whose top level collection is from the specified usUsagePage. Note that usUsage must be zero. To exclude a particular top level collection, use RIDEV_EXCLUDE.
        /// </summary>
        RIDEV_PAGEONLY = 0x00000020,

        /// <summary>
        /// If set, this removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection.
        /// </summary>
        RIDEV_REMOVE = 0x00000001
    }


    /// <summary>
    /// Introduced this enum for consistency and easy of use.
    /// Naming of the Win32 constants were preserved.
    /// </summary>
    public enum RawInputDeviceType : uint
    {
        /// <summary>
        /// Data comes from a mouse.
        /// </summary>
        RIM_TYPEMOUSE = 0,
        /// <summary>
        /// Data comes from a keyboard.
        /// </summary>
        RIM_TYPEKEYBOARD = 1,
        /// <summary>
        /// Data comes from an HID that is not a keyboard or a mouse.
        /// </summary>
        RIM_TYPEHID = 2
    }

    /// <summary>
    /// Introduced this enum for consistency and easy of use.
    /// Naming of the Win32 constants were preserved.
    /// </summary>
    public enum RawInputDeviceInfoType : uint
    {
        /// <summary>
        /// GetRawInputDeviceInfo pData points to a string that contains the device name.
        /// </summary>
        RIDI_DEVICENAME = 0x20000007,
        /// <summary>
        /// GetRawInputDeviceInfo For this uiCommand only, the value in pcbSize is the character count (not the byte count).
        /// </summary>
        RIDI_DEVICEINFO = 0x2000000b,
        /// <summary>
        /// GetRawInputDeviceInfo pData points to an RID_DEVICE_INFO structure.
        /// </summary>
        RIDI_PREPARSEDDATA = 0x20000005
    }

    /// <summary>
    /// See RAWKEYBOARD.Flags
    /// </summary>
    [Flags]
    public enum RawInputKeyFlags : ushort
    {
        /// <summary>
        /// The key is down.
        /// </summary>
        RI_KEY_MAKE = 0x0000,

        /// <summary>
        /// The key is up.
        /// </summary>
        RI_KEY_BREAK = 0x0001,

        /// <summary>
        /// The scan code has the E0 prefix.
        /// </summary>
        RI_KEY_E0 = 0x0002,

        /// <summary>
        /// The scan code has the E1 prefix.
        /// </summary>
        RI_KEY_E1 = 0x0004
    }


    /// <summary>
    /// See RAWMOUSE.usFlags.
    /// </summary>
    [Flags]
    public enum RawInputMouseFlags : ushort
    {
        /// <summary>
        /// Mouse movement data is relative to the last mouse position.
        /// </summary>
        MOUSE_MOVE_RELATIVE = 0,

        /// <summary>
        /// Mouse movement data is based on absolute position.
        /// </summary>
        MOUSE_MOVE_ABSOLUTE = 1,

        /// <summary>
        /// Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).
        /// </summary>
        MOUSE_VIRTUAL_DESKTOP = 0x02,

        /// <summary>
        /// Mouse attributes changed; application needs to query the mouse attributes.
        /// </summary>
        MOUSE_ATTRIBUTES_CHANGED = 0x04
    }


    /// <summary>
    /// Value taken by RAWMOUSEBUTTONS.usButtonFlags.
    /// </summary>
    [Flags]
    public enum RawInputMouseButtonFlags : ushort
    {
        /// <summary>
        /// Left button changed to down.
        /// </summary>
        RI_MOUSE_LEFT_BUTTON_DOWN = 0x0001,

        /// <summary>
        /// Left button changed to up.
        /// </summary>
        RI_MOUSE_LEFT_BUTTON_UP = 0x0002,

        /// <summary>
        /// Middle button changed to down.
        /// </summary>
        RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010,

        /// <summary>
        /// Middle button changed to up.
        /// </summary>
        RI_MOUSE_MIDDLE_BUTTON_UP = 0x0020,

        /// <summary>
        /// Right button changed to down.
        /// </summary>
        RI_MOUSE_RIGHT_BUTTON_DOWN = 0x0004,

        /// <summary>
        /// Right button changed to up.
        /// </summary>
        RI_MOUSE_RIGHT_BUTTON_UP = 0x0008,

        /// <summary>
        /// See RI_MOUSE_LEFT_BUTTON_DOWN
        /// </summary>
        RI_MOUSE_BUTTON_1_DOWN = 0x0001,

        /// <summary>
        /// See RI_MOUSE_LEFT_BUTTON_UP
        /// </summary>
        RI_MOUSE_BUTTON_1_UP = 0x0002,

        /// <summary>
        /// See RI_MOUSE_RIGHT_BUTTON_DOWN
        /// </summary>
        RI_MOUSE_BUTTON_2_DOWN = 0x0004,

        /// <summary>
        /// See RI_MOUSE_RIGHT_BUTTON_UP
        /// </summary>
        RI_MOUSE_BUTTON_2_UP = 0x0008,

        /// <summary>
        /// See RI_MOUSE_MIDDLE_BUTTON_DOWN
        /// </summary>
        RI_MOUSE_BUTTON_3_DOWN = 0x0010,

        /// <summary>
        /// See RI_MOUSE_MIDDLE_BUTTON_UP
        /// </summary>
        RI_MOUSE_BUTTON_3_UP = 0x0020,

        /// <summary>
        /// XBUTTON1 changed to down.
        /// </summary>        
        RI_MOUSE_BUTTON_4_DOWN = 0x0040,

        /// <summary>
        /// XBUTTON1 changed to up.
        /// </summary>
        RI_MOUSE_BUTTON_4_UP = 0x0080,

        /// <summary>
        /// XBUTTON2 changed to down.
        /// </summary>
        RI_MOUSE_BUTTON_5_DOWN = 0x100,

        /// <summary>
        /// XBUTTON2 changed to up.
        /// </summary>
        RI_MOUSE_BUTTON_5_UP = 0x0200,

        /// <summary>
        /// Raw input comes from a mouse wheel.The wheel delta is stored in usButtonData.
        /// </summary>
        RI_MOUSE_WHEEL = 0x0400
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWINPUTDEVICELIST
    {
        public IntPtr hDevice;
        public RawInputDeviceType dwType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWINPUTDEVICE
    {
        [MarshalAs(UnmanagedType.U2)]
        public ushort usUsagePage;
        [MarshalAs(UnmanagedType.U2)]
        public ushort usUsage;
        [MarshalAs(UnmanagedType.U4)]
        public RawInputDeviceFlags dwFlags;
        public IntPtr hwndTarget;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWINPUTHEADER
    {
        [MarshalAs(UnmanagedType.U4)]
        public RawInputDeviceType dwType;
        [MarshalAs(UnmanagedType.U4)]
        public int dwSize;
        public IntPtr hDevice;
        public IntPtr wParam;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct RAWINPUTDATA
    {
        [FieldOffset(0)]
        public RAWMOUSE mouse;
        [FieldOffset(0)]
        public RAWKEYBOARD keyboard;
        [FieldOffset(0)]
        public RAWHID hid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWHID
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwSizeHid;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwCount;
        //
        //BYTE  bRawData[1];
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWMOUSEBUTTONS
    {
        [MarshalAs(UnmanagedType.U2)]
        public RawInputMouseButtonFlags usButtonFlags;
        [MarshalAs(UnmanagedType.U2)]
        public ushort usButtonData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWMOUSE
    {
        public SharpLib.Win32.RawInputMouseFlags usFlags;

        [StructLayout(LayoutKind.Explicit)]
        public struct RAWMOUSEDATA
        {
            [FieldOffset(0)]
            public uint ulButtons;

            [FieldOffset(0)]
            public RAWMOUSEBUTTONS buttonsStr;
        }

        public RAWMOUSEDATA mouseData;

        public uint ulRawButtons;
        public int lLastX;
        public int lLastY;
        public uint ulExtraInformation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWKEYBOARD
    {
        [MarshalAs(UnmanagedType.U2)]
        public ushort MakeCode;
        [MarshalAs(UnmanagedType.U2)]
        public RawInputKeyFlags Flags;
        [MarshalAs(UnmanagedType.U2)]
        public ushort Reserved;
        [MarshalAs(UnmanagedType.U2)]
        public ushort VKey;
        [MarshalAs(UnmanagedType.U4)]
        public uint Message;
        [MarshalAs(UnmanagedType.U4)]
        public uint ExtraInformation;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RAWINPUT
    {
        public RAWINPUTHEADER header;
        public RAWINPUTDATA data;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RID_DEVICE_INFO_MOUSE
    {
        public uint dwId;
        public uint dwNumberOfButtons;
        public uint dwSampleRate;
        public bool fHasHorizontalWheel;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RID_DEVICE_INFO_KEYBOARD
    {
        public uint dwType;
        public uint dwSubType;
        public uint dwKeyboardMode;
        public uint dwNumberOfFunctionKeys;
        public uint dwNumberOfIndicators;
        public uint dwNumberOfKeysTotal;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RID_DEVICE_INFO_HID
    {
        public uint dwVendorId;
        public uint dwProductId;
        public uint dwVersionNumber;
        public ushort usUsagePage;
        public ushort usUsage;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct RID_DEVICE_INFO
    {
        [FieldOffset(0)]
        public uint cbSize;
        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U4)]
        public RawInputDeviceType dwType;
        [FieldOffset(8)]
        public RID_DEVICE_INFO_MOUSE mouse;
        [FieldOffset(8)]
        public RID_DEVICE_INFO_KEYBOARD keyboard;
        [FieldOffset(8)]
        public RID_DEVICE_INFO_HID hid;
    }


}