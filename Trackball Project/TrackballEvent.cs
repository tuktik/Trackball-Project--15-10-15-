using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    class TrackballEvent
    {
        //private static MainWindow mMainWindow = null;

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
        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x27;
        const int VK_BROWSER_BACK = 0xA6;
        const int VK_BROWSER_FORWARD = 0xA7;

        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;


        #endregion media control dll

        //public TrackballEvent(MainWindow mainWindow)
        public TrackballEvent()
        {
            //mMainWindow = mainWindow;
        }


        public void VirtualEvent(VEKey vEvent)
        {
            switch (vEvent)
            {
                case VEKey.VK_VOLUME_MUTE:
                    keybd_event((byte)VK_VOLUME_MUTE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_VOLUME_DOWN:
                    keybd_event((byte)VK_VOLUME_DOWN, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_VOLUME_UP:
                    keybd_event((byte)VK_VOLUME_UP, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_MEDIA_PLAY_PAUSE:
                    keybd_event((byte)VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_LAUNCH_MEDIA_SELECT:
                    keybd_event((byte)VK_LAUNCH_MEDIA_SELECT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_MEDIA_NEXT_TRACK:
                    keybd_event((byte)VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_MEDIA_PREV_TRACK:
                    keybd_event((byte)VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_MEDIA_STOP:
                    keybd_event((byte)VK_MEDIA_STOP, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_UP:
                    keybd_event((byte)VK_UP, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_DOWN:
                    keybd_event((byte)VK_DOWN, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_LEFT:
                    keybd_event((byte)VK_LEFT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_RIGHT:
                    keybd_event((byte)VK_RIGHT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_BROWSER_BACK:
                    keybd_event((byte)VK_BROWSER_BACK, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
                case VEKey.VK_BROWSER_FORWARD:
                    keybd_event((byte)VK_BROWSER_FORWARD, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    break;
            }
        }
    }
}