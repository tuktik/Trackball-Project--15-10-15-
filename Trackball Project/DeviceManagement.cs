using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using MahApps.Metro.Native;

namespace Trackball_Project
{
    public sealed class InputDevice
    {
        #region const definitions


        private const int RIDEV_INPUTSINK = 0x00000100;
        private const int RIDEV_NOLEGACY = 0x00000030;
        private const int RIDEV_CAPTUREMOUSE = 0x00000200;
        private const int RIDEV_EXINPUTSINK = 0x00001000;
        private const int RIDEV_DEVNOTIFY = 0x00002000;

        private const int RID_INPUT = 0x10000003;

        private const int FAPPCOMMAND_MASK = 0xF000;
        private const int FAPPCOMMAND_MOUSE = 0x8000;
        private const int FAPPCOMMAND_OEM = 0x1000;

        private const int RIM_TYPEMOUSE = 0;
        private const int RIM_TYPEKEYBOARD = 1;
        private const int RIM_TYPEHID = 2;

        private const int RIDI_DEVICENAME = 0x20000007;
        private const int RIDI_DEVICEINFO = 0x2000000b;
        private const int RIDI_PREPARSEDDATA = 0x20000005;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_INPUT = 0x00FF;
        private const int VK_OEM_CLEAR = 0xFE;
        private const int VK_LAST_KEY = VK_OEM_CLEAR;

        #endregion const definitions

        #region mouse event register

        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        private const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
        private const int MOUSEEVENTF_XUP = 0x0100; /* x button up */
        private const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */

        // 커서 이동
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        // 마우스 이벤트
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        // 현재 커서 가져오기
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        #endregion mouse event register

        #region media control dll
        // media control
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int VK_VOLUME_MUTE = 0xAD;
        const int VK_VOLUME_DOWN = 0xAE;
        const int VK_VOLUME_UP = 0xAF;

        const int VK_MEDIA_PLAY_PAUSE = 0xB3; // window media player
        const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        const int VK_MEDIA_NEXT_TRACK = 0xB0;
        const int VK_MEDIA_PREV_TRACK = 0xB1;
        const int VK_MEDIA_STOP = 0xB2;

        const int VK_UP = 0x26;
        const int VK_DOWN = 0x28;
        const int VK_BROWSER_BACK = 0xA6;
        const int VK_BROWSER_FORWARD = 0xA7;

        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        private int LEFT_RIGHT_EVENT = 0;
        private int UP_DOWN_EVENT = 0;
        private int WHEEL_EVENT = 0;


        #endregion media control dll

        #region structs & enums

        public enum DeviceType
        {
            Key,
            Mouse,
            OEM
        }

        public class DeviceInfo
        {
            public string deviceName;
            public string deviceType;
            public IntPtr deviceHandle;
            public string Name;
            public string source;
            public ushort key;
            public string vKey;
            public uint mButton;
            public uint setDevice;
        }

        #region Windows.h structure declarations

        // The following structures are defined in Windows.h

        [Flags()]
        public enum RawMouseFlags : ushort
        {
            MoveRelative = 0,
            MoveAbsolute = 1,
            VirtualDesktop = 2,
            AttributesChanged = 4
        }
        [Flags()]
        public enum RawMouseButtons : ushort
        {
            None = 0,
            LeftDown = 0x0001,
            LeftUp = 0x0002,
            RightDown = 0x0004,
            RightUp = 0x0008,
            MiddleDown = 0x0010,
            MiddleUp = 0x0020,
            Button4Down = 0x0040,
            Button4Up = 0x0080,
            Button5Down = 0x0100,
            Button5Up = 0x0200,
            MouseWheel = 0x0400
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICELIST
        {
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;
            [FieldOffset(16)]
            public RAWMOUSE mouse;
            [FieldOffset(16)]
            public RAWKEYBOARD keyboard;
            [FieldOffset(16)]
            public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int wParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWHID
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwSizHid;
            [MarshalAs(UnmanagedType.U4)]
            public int dwCount;
            public byte bRawData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTONSSTR
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonFlags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonData;
        }
        /*
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWMOUSE
        {
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)]
            public ushort usFlags;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)]
            public uint ulButtons;
            [FieldOffset(4)]
            public BUTTONSSTR buttonsStr;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(8)]
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(20)]
            public uint ulExtraInformation;
        }
        */
        [StructLayout(LayoutKind.Explicit)]
        public struct RAWMOUSE
        {
            [FieldOffset(0)]
            public ushort usFlags;
            [FieldOffset(4)]
            public uint ulButtons;
            [FieldOffset(4)]
            public ushort usButtonFlags;
            [FieldOffset(6)]
            public short usButtonData;
            [FieldOffset(8)]
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [FieldOffset(20)]
            public uint ulExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWKEYBOARD
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort MakeCode;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Flags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Reserved;
            [MarshalAs(UnmanagedType.U2)]
            public ushort VKey;
            [MarshalAs(UnmanagedType.U4)]
            public uint Message;
            [MarshalAs(UnmanagedType.U4)]
            public uint ExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICE
        {
            [MarshalAs(UnmanagedType.U2)]
            internal HidUsagePage usUsagePage;
            [MarshalAs(UnmanagedType.U2)]
            internal HidUsage usUsage;
            [MarshalAs(UnmanagedType.U4)]
            internal RawInputDeviceFlags dwFlags;
            public IntPtr hwndTarget;
        }

        #endregion Windows.h structure declarations


        #endregion structs & enums

        #region DllImports

        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

        [DllImport("User32.dll", SetLastError = true)]
        extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll", SetLastError = true)]
        extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        #endregion DllImports

        #region Variables and event handling

        private Hashtable deviceList = new Hashtable();

        public delegate void DeviceEventHandler(object sender, KeyControlEventArgs e);
        public delegate void MouseEventHandler(object sender, MouseControlEventArgs e);

        public event DeviceEventHandler KeyPressed;
        public event MouseEventHandler MouseEvent;

        public class MouseControlEventArgs : EventArgs
        {
            private DeviceInfo m_deviceInfo;
            private DeviceType m_device;

            public MouseControlEventArgs(DeviceInfo dinfo, DeviceType device)
            {
                m_deviceInfo = dinfo;
                m_device = device;
            }

            public MouseControlEventArgs() { }

            public DeviceInfo Mouse
            {
                get { return m_deviceInfo; }
                set { m_deviceInfo = value; }
            }

            public DeviceType Device
            {
                get { return m_device; }
                set { m_device = value; }
            }
        }

        public class KeyControlEventArgs : EventArgs
        {
            private DeviceInfo m_deviceInfo;
            private DeviceType m_device;

            public KeyControlEventArgs(DeviceInfo dInfo, DeviceType device)
            {
                m_deviceInfo = dInfo;
                m_device = device;
            }

            public KeyControlEventArgs()
            {
            }

            public DeviceInfo Keyboard
            {
                get { return m_deviceInfo; }
                set { m_deviceInfo = value; }
            }

            public DeviceType Device
            {
                get { return m_device; }
                set { m_device = value; }
            }
        }

        #endregion Variables and event handling

        #region InputDevice( IntPtr hwnd )

        // mainwindow 싱글톤
        private static MainWindow mMainWindow = null;

        public InputDevice(IntPtr hwnd, MainWindow mainWindow)
        {
            mMainWindow = mainWindow;

            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[2];

            rid[0].usUsagePage = HidUsagePage.GENERIC;
            rid[0].usUsage = HidUsage.Keyboard;
            rid[0].dwFlags = RawInputDeviceFlags.INPUTSINK;// | RawInputDeviceFlags.DEVNOTIFY;
            //rid[0].dwFlags = RawInputDeviceFlags.INPUTSINK;// | RawInputDeviceFlags.DEVNOTIFY;
            //rid[0].dwFlags = 0;
            rid[0].hwndTarget = hwnd;

            rid[1].usUsagePage = HidUsagePage.GENERIC;
            rid[1].usUsage = HidUsage.Mouse;
            rid[1].dwFlags = RawInputDeviceFlags.INPUTSINK;// | RawInputDeviceFlags.DEVNOTIFY;
            //rid[1].dwFlags = RawInputDeviceFlags.INPUTSINK;// | RawInputDeviceFlags.DEVNOTIFY;
            //rid[1].dwFlags = 0;
            rid[1].hwndTarget = hwnd;


            /*
            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
            rid[0].usUsagePage = 0x01;
            rid[0].usUsage = 0x02;
            //rid[1].dwFlags = RIDEV_INPUTSINK;
            //rid[0].dwFlags = RIDEV_NOLEGACY;
            rid[0].dwFlags = 0;
            rid[0].hwndTarget = hwnd;
            */


            if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
            {
                throw new ApplicationException("Failed to register raw input device(s).");
            }
        }

        #endregion InputDevice( IntPtr hwnd )

        #region ReadReg( string item, ref bool isKeyboard )

        private string ReadReg(string item, ref bool isKeyboard)
        {
            item = item.Substring(4);

            string[] split = item.Split('#');

            string id_01 = split[0];
            string id_02 = split[1];
            string id_03 = split[2];

            RegistryKey OurKey = Registry.LocalMachine;

            string findme = string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", id_01, id_02, id_03);

            OurKey = OurKey.OpenSubKey(findme, false);


            string deviceDesc = (string)OurKey.GetValue("DeviceDesc");
            string deviceClass = (string)OurKey.GetValue("Class");

            if (deviceClass.ToUpper().Equals("KEYBOARD"))
            {
                isKeyboard = true;
            }
            else
            {
                isKeyboard = false;
            }
            return deviceDesc;
        }

        #endregion ReadReg( string item, ref bool isKeyboard )

        #region int EnumerateDevices()


        public int EnumerateDevices()
        {

            int NumberOfDevices = 0;
            uint deviceCount = 0;
            int dwSize = (Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));

            if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0)
            {
                IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                for (int i = 0; i < deviceCount; i++)
                {
                    DeviceInfo dInfo;
                    string deviceName;
                    uint pcbSize = 0;

                    RAWINPUTDEVICELIST rid = (RAWINPUTDEVICELIST)Marshal.PtrToStructure(
                                               new IntPtr((pRawInputDeviceList.ToInt32() + (dwSize * i))),
                                               typeof(RAWINPUTDEVICELIST));

                    GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                    if (pcbSize > 0)
                    {
                        IntPtr pData = Marshal.AllocHGlobal((int)pcbSize);
                        GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, pData, ref pcbSize);
                        deviceName = (string)Marshal.PtrToStringAnsi(pData);


                        if (deviceName.ToUpper().Contains("ROOT"))
                        {
                            continue;
                        }


                        if (rid.dwType == RIM_TYPEKEYBOARD || rid.dwType == RIM_TYPEHID || rid.dwType == RIM_TYPEMOUSE)
                        {
                            Console.WriteLine("\nMouseName");
                            Console.WriteLine(deviceName);
                            dInfo = new DeviceInfo();

                            dInfo.deviceName = (string)Marshal.PtrToStringAnsi(pData);
                            dInfo.deviceHandle = rid.hDevice;
                            dInfo.deviceType = GetDeviceType(rid.dwType);


                            bool IsKeyboardDevice = false;
                            string DeviceDesc = ReadReg(deviceName, ref IsKeyboardDevice);
                            dInfo.Name = DeviceDesc;

                            if (!deviceList.Contains(rid.hDevice))
                            {
                                NumberOfDevices++;
                                deviceList.Add(rid.hDevice, dInfo);
                            }
                        }
                        Marshal.FreeHGlobal(pData);
                    }
                }


                Marshal.FreeHGlobal(pRawInputDeviceList);

                return NumberOfDevices;

            }
            else
            {
                throw new ApplicationException("An error occurred while retrieving the list of devices.");
            }

        }

        #endregion EnumerateDevices()

        #region ProcessInputCommand(IntPtr LParam)

        public void ProcessInputCommand(IntPtr LParam)
        {
            uint dwSize = 0;

            GetRawInputData(LParam,
                             RID_INPUT, IntPtr.Zero,
                             ref dwSize,
                             (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                if (buffer != IntPtr.Zero &&
                    GetRawInputData(LParam,
                                     RID_INPUT,
                                     buffer,
                                     ref dwSize,
                                     (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                {

                    RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                    if (raw.header.dwType == RIM_TYPEKEYBOARD)
                    {
                        if (raw.keyboard.Message == WM_KEYDOWN || raw.keyboard.Message == WM_SYSKEYDOWN)
                        {

                            ushort key = raw.keyboard.VKey;

                            if (key > VK_LAST_KEY)
                            {
                                return;
                            }

                            DeviceInfo dInfo = null;

                            if (deviceList.Contains(raw.header.hDevice))
                            {
                                Keys myKey;

                                dInfo = (DeviceInfo)deviceList[raw.header.hDevice];

                                myKey = (Keys)Enum.Parse(typeof(Keys), Enum.GetName(typeof(Keys), key));
                                dInfo.vKey = myKey.ToString();
                                dInfo.key = key;
                            }
                            else
                            {
                                string errMessage = String.Format("Handle :{0} was not in hashtable. The device may support more than one handle or usage page, and is probably not a standard keyboard.", raw.header.hDevice);
                                //throw new ApplicationException(errMessage);
                            }

                            if (KeyPressed != null && dInfo != null)
                            {
                                KeyPressed(this, new KeyControlEventArgs(dInfo, GetDevice(LParam.ToInt32())));
                            }
                            else
                            {
                                string errMessage = String.Format("Received Unknown Key: {0}. Possibly an unknown device", key);
                                //throw new ApplicationException(errMessage);
                            }
                        }
                    }
                    else if (raw.header.dwType == RIM_TYPEMOUSE)
                    {
                        DeviceInfo dInfo = null;

                        if (deviceList.ContainsKey(raw.header.hDevice))
                        {

                            dInfo = (DeviceInfo)deviceList[raw.header.hDevice];

                        }
                        else
                        {
                            string errMessage = String.Format("Handle :{0} was not in hashtable. The device may support more than one handle or usage page, and is probably not a standard keyboard.", raw.header.hDevice);
                            //throw new ApplicationException(errMessage);
                        }

                        if (MouseEvent != null && dInfo != null)
                        {
                            MouseEvent(this, new MouseControlEventArgs(dInfo, GetDevice(LParam.ToInt32())));
                            String mouseMove = "\nX - " + raw.mouse.lLastX + "    Y - " + raw.mouse.lLastY;
                            String mouseButton = "ulButtons - " + raw.mouse.ulButtons + "    usButtonFlags - " + raw.mouse.usButtonFlags;
                            String mouseWheel = "usButtonData - " + raw.mouse.usButtonData + "    usFlags - " + raw.mouse.usFlags + "\n";
                            Console.WriteLine(mouseMove);
                            Console.WriteLine(mouseButton);
                            Console.WriteLine(mouseWheel);
                            dInfo.mButton = raw.mouse.ulButtons;

                            if (dInfo.setDevice == 1)
                            {
                                Console.WriteLine("LR - " + LEFT_RIGHT_EVENT);
                                Console.WriteLine("UD - " + UP_DOWN_EVENT);
                                LEFT_RIGHT_EVENT += raw.mouse.lLastX;
                                UP_DOWN_EVENT += raw.mouse.lLastY;
                                if (LEFT_RIGHT_EVENT < -200)
                                {
                                    mMainWindow.LEFT_Evnet();
                                    //keybd_event((byte)VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (LEFT_RIGHT_EVENT > 200)
                                {
                                    mMainWindow.RIGHT_Event();
                                    //keybd_event((byte)VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (UP_DOWN_EVENT < -200)
                                {
                                    mMainWindow.UP_Event();
                                    //keybd_event((byte)VK_VOLUME_UP, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (UP_DOWN_EVENT > 200)
                                {
                                    mMainWindow.DOWN_Event();
                                    //keybd_event((byte)VK_VOLUME_DOWN, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (raw.mouse.usButtonFlags == 2)
                                {
                                    mMainWindow.LCLICK_Event();
                                    //keybd_event((byte)VK_LAUNCH_MEDIA_SELECT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (raw.mouse.usButtonFlags == 8)
                                {
                                    mMainWindow.RCLICK_Event();
                                    //keybd_event((byte)VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (raw.mouse.usButtonData == 120)
                                {
                                    mMainWindow.WHEEL_UP_Event();
                                    //keybd_event((byte)VK_UP, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                else if (raw.mouse.usButtonData == -120)
                                {
                                    mMainWindow.WHEEL_DOWN_Event();
                                    //keybd_event((byte)VK_DOWN, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                    LEFT_RIGHT_EVENT = UP_DOWN_EVENT = 0;
                                }
                                //VK_ZOOM = 0xFB;
                                //VK_PLAY = 0xFA;
                            }
                            /*
                            POINT p;
                            if (GetCursorPos(out p))
                            {
                                if (dInfo.setDevice == 0)
                                {
                                    SetCursorPos(p.X + raw.mouse.lLastX, p.Y + raw.mouse.lLastY);
                                }
                                else if (dInfo.setDevice == 0 && raw.mouse.usButtonFlags == 1)
                                {
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                }
                                else if (dInfo.setDevice == 0 && raw.mouse.usButtonFlags == 2)
                                {
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                else if (dInfo.setDevice == 0 && raw.mouse.usButtonFlags == 4)
                                {
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                }
                                else if (dInfo.setDevice == 0 && raw.mouse.usButtonFlags == 8)
                                {
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                            }
                            */
                        }
                        else
                        {
                            string errMessage = String.Format("Received Unknown Key: . Possibly an unknown device");
                            //throw new ApplicationException(errMessage);
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        #endregion ProcessInputCommand(IntPtr LParam)

        #region DeviceType GetDevice( int param )

        private DeviceType GetDevice(int param)
        {
            DeviceType deviceType;

            switch ((int)(((ushort)(param >> 16)) & FAPPCOMMAND_MASK))
            {
                case FAPPCOMMAND_OEM:
                    deviceType = DeviceType.OEM;
                    break;
                case FAPPCOMMAND_MOUSE:
                    deviceType = DeviceType.Mouse;
                    break;
                default:
                    deviceType = DeviceType.Key;
                    break;
            }

            return deviceType;
        }

        #endregion DeviceType GetDevice( int param )

        #region ProcessMessage(int Msg, IntPtr LParam)

        public void ProcessMessage(int Msg, IntPtr LParam)
        {
            switch (Msg)
            {
                case WM_INPUT:
                    {
                        ProcessInputCommand(LParam);
                    }
                    break;
            }
        }

        #endregion ProcessMessage(int Msg, IntPtr LParam)

        #region GetDeviceType( int device )

        private string GetDeviceType(int device)
        {
            string deviceType;
            switch (device)
            {
                case RIM_TYPEMOUSE: deviceType = "MOUSE"; break;
                case RIM_TYPEKEYBOARD: deviceType = "KEYBOARD"; break;
                case RIM_TYPEHID: deviceType = "HID"; break;
                default: deviceType = "UNKNOWN"; break;
            }
            return deviceType;
        }

        #endregion GetDeviceType( int device )

    }
}
