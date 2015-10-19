using MahApps.Metro.Controls;   // Metro를 사용하기 위한 Import
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

using MahApps.Metro;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using WinForms = System.Windows.Forms;  // Tray Icon 사용 시 설정
using System.Windows.Interop;
using System.Runtime.InteropServices;

using virtual_desktop;

namespace Trackball_Project
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static MainWindow sMainWindow = null;
        static readonly object padlock = new object();

        private InputDevice id;
        int NumKeyboard;
        IntPtr nHwnd;
        public int hwnd;
        public String setVID, setPID;
        MouseHook mouseHook = new MouseHook();

        // hWnd 가져오기
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);

        public IconSource icons = new IconSource();
        public Uri trackball_background = new Uri(@"/Image/Trackball_Background.png", UriKind.Relative);
        public Uri trackball_enterted = null;
        public Accent CurrentAccent = null;
        public AppTheme Light = null;

        public FontFamily sFont = new FontFamily("./Fonts/HYKANM.TTF");
        public List<Mode> mUserSetting = new List<Mode>();
        public int mCurrentMode = 0;
        private String[] functionNames = {"음소거", "볼륨줄이기", "볼륨키우기", "미디어 재생/일시정지", "미디어 실행", "미디어 다음트랙", "미디어 이전트랙", "미디어 정지", "방향키 위", "방향키 아래", "방향키 왼쪽", "방향키 오른쪽", "이전 페이지", "다음 페이지"};

        // TrackballEvent 전역관리
        TrackballEvent tEvent = new TrackballEvent();

        // File Structure 관리
        public FileStructure fStructure;

        // TrayIcon 관련 Member 변수
        public System.Windows.Forms.NotifyIcon mNotify;
        //mNotify.Icon = Trackball_Project.Properties.Resources.ICON;

        public MainWindow()
        {
            sMainWindow = this;                 // 싱클톤 패턴 구현을 위한 static 멤버 변수에 자기 자신 객체 할당
            InitializeComponent();              // WindowsForm을 실행 할 떄 필수적으로 호출 
            TrayIconSetting();                  // TrayIcon 관련 객체 생성 및 기본 세팅

            // FileStructure 생성
            fStructure = new FileStructure(sMainWindow);
            fStructure.FileLoad();

            InitializeUserSetting();
            InitializeThemeObject();
            InitializeModeComboBox();

            //this.DataContext = new AppearanceViewModel();

            
            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // 초기 테마 설정
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Blue"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            sMainWindow.FontFamily = sFont;

            //hwnd = FindWindowEx(0, 0, 0, "데굴데굴");
            RegisterMouseEvnet();
        }
        public void InitializeThemeObject()
        {
            Light = ThemeManager.GetAppTheme("BaseLight");
            CurrentAccent = ThemeManager.GetAccent("Blue");
        }


        #region Theme Change

        private void ColorChange(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            String selectedColor = selectedButton.Name.ToString();
            Console.WriteLine(selectedColor);
            CurrentAccent = ThemeManager.GetAccent(selectedColor);
            ThemeManager.ChangeAppStyle(Application.Current, CurrentAccent, Light);
        }
        #endregion Theme Change

        #region Singleton pattern
        // 싱글톤을 위해 사용했지만 무슨 의도인지 아직 모름
        public static MainWindow Instance
        {
            get
            {
                lock (padlock)
                {
                    if (sMainWindow == null)
                    {
                        sMainWindow = new MainWindow();
                    }
                    return sMainWindow;
                }
            }
        }
        #endregion Singleton pattern

        #region UserSetting & ComboBox
        public void InitializeUserSetting()
        {
            mUserSetting.Add(new Mode("UserSetting1", icons.uris[0]));
            ModeComboBox.Items.Add("UserSetting1");
            ModeComboBox.SelectedIndex = 0;
        }
        public void InitializeModeComboBox()
        {
            for (var i = 0; i < functionNames.Length; i++) {
                UpComboBox.Items.Add(functionNames[i]);
                DownComboBox.Items.Add(functionNames[i]);
                LeftComboBox.Items.Add(functionNames[i]);
                RightComboBox.Items.Add(functionNames[i]);
                DragUpComboBox.Items.Add(functionNames[i]);
                DragDownComboBox.Items.Add(functionNames[i]);
                Button1ComboBox.Items.Add(functionNames[i]);
                Button2ComboBox.Items.Add(functionNames[i]);
            }
        }
        private void ModeImageChange(object sender, RoutedEventArgs e)
        {
            IconWindow iconWindow = new IconWindow(sMainWindow, mCurrentMode);
            iconWindow.Top = this.Top;
            iconWindow.Left = this.Left - 285;
            iconWindow.ShowDialog();
        }
        private void ChangedIndex(object sender, EventArgs e)       // 나머지 ComboBox의 값이 변경되었을 때 호출
        {
            ///Console.WriteLine(sender.ToString());
            //Console.WriteLine("Current Mode : " + mCurrentMode);
            int mainIdx = ModeComboBox.SelectedIndex;
            int subIdx = ((ComboBox)sender).SelectedIndex;
            if (mainIdx >= 0 && subIdx >= 0)
            {
                if (sender == UpComboBox)
                    mUserSetting[mainIdx].up = subIdx;
                else if (sender == DownComboBox)
                    mUserSetting[mainIdx].down = subIdx;
                else if (sender == LeftComboBox)
                    mUserSetting[mainIdx].left = subIdx;
                else if (sender == RightComboBox)
                    mUserSetting[mainIdx].right = subIdx;
                else if (sender == DragUpComboBox)
                    mUserSetting[mainIdx].dragUp = subIdx;
                else if (sender == DragDownComboBox)
                    mUserSetting[mainIdx].dragDown = subIdx;
                else if (sender == Button1ComboBox)
                    mUserSetting[mainIdx].button1 = subIdx;
                else if (sender == Button2ComboBox)
                    mUserSetting[mainIdx].button2 = subIdx;
            }
        }
        private void SelectedIndexChanged(object sender, EventArgs e)   // ModeComboBox의 값이 변경되었을 때 호출
        {
            int mainIdx = ModeComboBox.SelectedIndex;
            mCurrentMode = mainIdx;                                     // 현재 모드를 선택한 모드로 변경
            if (mainIdx >= 0)
            {
                ModeIconImage.Source = new BitmapImage(mUserSetting[mainIdx].image);
                UpComboBox.SelectedIndex = mUserSetting[mainIdx].up;
                DownComboBox.SelectedIndex = mUserSetting[mainIdx].down;
                LeftComboBox.SelectedIndex = mUserSetting[mainIdx].left;
                RightComboBox.SelectedIndex = mUserSetting[mainIdx].right;
                DragUpComboBox.SelectedIndex = mUserSetting[mainIdx].dragUp;
                DragDownComboBox.SelectedIndex = mUserSetting[mainIdx].dragDown;
                Button1ComboBox.SelectedIndex = mUserSetting[mainIdx].button1;
                Button2ComboBox.SelectedIndex = mUserSetting[mainIdx].button2;
            }
            //Console.WriteLine("Select Mode : " + ModeComboBox.SelectedIndex);
        }
        private void ModeButtonClick(object sender, RoutedEventArgs e)  // UserSetting 추가 및 삭제 버튼 이벤트 함수
        {
            if (sender == ModeAddButton)
            {
                String newModeName = ModeNameTextBox.Text;
                ModeNameTextBox.Text = "";
                int modeNum = mUserSetting.Count;
                if (newModeName.Equals(""))
                    newModeName = "UserSetting" + (modeNum + 1).ToString();
                mUserSetting.Add(new Mode(newModeName, icons.uris[0]));

                ModeComboBox.Items.Add(newModeName);
                ModeComboBox.SelectedIndex = modeNum;
                UpComboBox.SelectedIndex = 0;
                DownComboBox.SelectedIndex = 0;
                LeftComboBox.SelectedIndex = 00;
                RightComboBox.SelectedIndex = 0;
                DragUpComboBox.SelectedIndex = 0;
                DragDownComboBox.SelectedIndex = 0;
                Button1ComboBox.SelectedIndex = 0;
                Button2ComboBox.SelectedIndex = 0;
            }
            else if (sender == ModeRemoveButton)    // 현재 선택된 모드 삭제
            {
                if (mUserSetting.Count == 1)  // 삭제 할 모드가 없을 때
                    return;

                int mainIdx = ModeComboBox.SelectedIndex;
                String removeModeName = (String)ModeComboBox.Items[mainIdx];
                //Console.WriteLine(removeModeName);
                if (mainIdx == 0)                   // 삭제하고자 하는 모드가 첫 번째 일 때
                    ModeComboBox.SelectedIndex = mainIdx + 1;
                else
                    ModeComboBox.SelectedIndex = mainIdx - 1;
                ModeComboBox.Items.Remove(ModeComboBox.Items[mainIdx]); // ComboBox에서 삭제
                mUserSetting.RemoveAt(mainIdx);                         // List에서 삭제
            }
        }
        private void Init_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("현재 설정을 모두 초기화 합니다.");
            int maxIdx = mUserSetting.Count;
            ModeComboBox.SelectedIndex = 0;
            UpComboBox.SelectedIndex = 0;
            DownComboBox.SelectedIndex = 0;
            LeftComboBox.SelectedIndex = 0;
            RightComboBox.SelectedIndex = 0;
            DragUpComboBox.SelectedIndex = 0;
            DragDownComboBox.SelectedIndex = 0;
            Button1ComboBox.SelectedIndex = 0;
            Button2ComboBox.SelectedIndex = 0;
            mUserSetting[0].image = icons.uris[0];
            ModeIconImage.Source = new BitmapImage(mUserSetting[0].image);

            if (mUserSetting.Count == 1)
                return;
            for (int i = 1; i < maxIdx; i++)
            {
                ModeComboBox.Items.RemoveAt(1);
                mUserSetting.RemoveAt(1);
            }
        }
        #endregion

        #region TrayIcon 구현부
        private void TrayIconSetting()
        {
            try
            {
                // ContextMenu
                // 메뉴 패널이 등장하도록 설정하는 부분
                // (트레이 아이콘에서 마우스 우클릭 시 나타나는 메뉴 설정)
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
                System.Windows.Forms.MenuItem menuProgramExit = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem menuTrackballMode = new System.Windows.Forms.MenuItem();
                menu.MenuItems.Add(menuProgramExit);
                menu.MenuItems.Add(menuTrackballMode);
                menuProgramExit.Index = 0;
                menuProgramExit.Text = "프로그램 종료";
                menuProgramExit.Click += delegate(object click, EventArgs eClick)
                {
                    mNotify.Visible = false;
                    this.Close();
                };

                menuTrackballMode.Index = 0;
                menuTrackballMode.Text = "트랙볼 모드";
                menuTrackballMode.Click += delegate(object click, EventArgs eClick)
                {
                    SelectWindow selectWindow = new SelectWindow(sMainWindow);
                    selectWindow.ShowDialog();
                };


                mNotify = new System.Windows.Forms.NotifyIcon();
                mNotify.Icon = Trackball_Project.Properties.Resources.Connected;
                mNotify.Visible = true;
                mNotify.DoubleClick += delegate(object senders, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
                mNotify.ContextMenu = menu;
                mNotify.Text = "데굴데굴 실행중";

                mNotify.BalloonTipTitle = "데굴데굴";
                mNotify.BalloonTipText = "프로그램이 실행 중입니다.";
                mNotify.ShowBalloonTip(1000);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        // 윈도우 종료 이벤트 호출 시 트레이 아이콘 강제로 삭제
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            mNotify.ShowBalloonTip(1000);
            //mNotify.Visible = false;
        }
        // 윈도우가 최소화 되었을 때 최소화 메세지 호출
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState.Minimized.Equals(WindowState))
            {
                //this.Hide();
                mNotify.ShowBalloonTip(1000);
            }
            base.OnStateChanged(e);
        }
        
        // TrayIcon 클릭 시 윈도우 프로그램 원상복귀
        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = System.Windows.WindowState.Normal;
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // when window is unvisible, change visible option tray icon
            if (IsVisible == false)
                mNotify.Visible = true;
        }
        #endregion TrayIcon 구현부


        // Button Click Methods...
        private void Trackball_Button_Click(object sender, RoutedEventArgs e)
        {
            TrackballPage.IsOpen = true;
        }
        private void Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingPage.IsOpen = true;
        }
        private void Trackball_Button_Pressed(object sender, RoutedEventArgs e)
        {

        }
        private void Trackball_Button_Over(object sender, RoutedEventArgs e)
        {

        }
        private void Trackball_Button_Base(object sender, RoutedEventArgs e)
        {
            //Trackball_Background_Image.Source = 
        }

        
        private void Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            mouseHook.Uninstall();
            DriverWindow driverWindow = new DriverWindow(sMainWindow);
            driverWindow.ShowDialog();
        }

        private void Window_Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        // 동녁오빠!
        private void Device_Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            if (setVID == null && setPID == null)
            {
                MessageBox.Show("선택한 마우스가 없습니다.");
                return;
            }
            var window = new Window();

            //int hwnd = this.hwnd;
            int hwnd = FindWindowEx(0, 0, 0, "데굴데굴");
            Console.WriteLine(hwnd);
            //var handle = new WindowInteropHelper(window).EnsureHandle();
            //IntPtr myHandle = handle;
            //var handle = new WindowInteropHelper(window);
            //IntPtr myHandle = handle.Handle;
            IntPtr myHandle = (IntPtr)hwnd;
            id = new InputDevice(myHandle, sMainWindow);
            NumKeyboard = id.EnumerateDevices();
            id.MouseEvent += new InputDevice.MouseEventHandler(m_MouseEvent);
            id.KeyPressed += new InputDevice.DeviceEventHandler(m_KeyEvent);

            Console.WriteLine("\nNum " + NumKeyboard);
            mouseHook.Install();
            MessageBox.Show("you click desktop button!!");
        }
        // 현규오빠!
        private void Virtual_Desktop_Button_Click(object sender, RoutedEventArgs e)
        {
            Virtual_Desktop test = new Virtual_Desktop();
            test.Show();
        }

        public void auto_start()
        {
            Auto_RID_Event.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, Auto_RID_Event));
        }

        #region Trackball Event Function

        //VK_VOLUME_MUTE = 0,
        //VK_VOLUME_DOWN,
        //VK_VOLUME_UP,
        //VK_MEDIA_PLAY_PAUSE,
        //VK_LAUNCH_MEDIA_SELECT,
        //VK_MEDIA_NEXT_TRACK,
        //VK_MEDIA_PREV_TRACK,
        //VK_MEDIA_STOP,
        //VK_UP,
        //VK_DOWN,
        //VK_LEFT,
        //VK_RIGHT,
        //VK_BROWSER_BACK,
        //VK_BROWSER_FORWARD

        public void UP_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_VOLUME_UP); // List item을 가져와야한다.
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].up);
            tEvent.VirtualEvent((VEKey)fStructure.trackball_UP);
        }

        public void DOWN_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_VOLUME_DOWN);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].down);
        }

        public void LEFT_Evnet()
        {
            //tEvent.VirtualEvent(VEKey.VK_BROWSER_BACK);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].left);
        }

        public void RIGHT_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_BROWSER_FORWARD);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].right);
        }

        public void WHEEL_UP_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_UP);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].dragUp);
        }

        public void WHEEL_DOWN_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_DOWN);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].dragDown);
        }

        public void LCLICK_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_LAUNCH_MEDIA_SELECT);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].button1);
        }

        public void RCLICK_Event()
        {
            //tEvent.VirtualEvent(VEKey.VK_MEDIA_PLAY_PAUSE);
            tEvent.VirtualEvent((VEKey)mUserSetting[mCurrentMode].button2);
        }

        #endregion Trackball Event Function

        #region hWnd 가져오기
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var message = (WindowMessage)msg;
            this.nHwnd = hwnd;
            if (id != null)
            {
                id.ProcessMessage(msg, lParam);
            }
            return IntPtr.Zero;
        }
        #endregion hWnd 가져오기
        
        // mouseEvent Handler
        private void m_MouseEvent(object sender, InputDevice.MouseControlEventArgs e)
        {
            String logMsg = "Handler - " + e.Mouse.deviceHandle.ToString() + "\nType - " + e.Mouse.deviceType + "\nName - " + e.Mouse.deviceName + "\nDescription - " + e.Mouse.Name + "\n";
            String ID = e.Mouse.deviceName;
            String vID = ID.Substring(8, 8);
            String pID = ID.Substring(17, 8);
            //Console.WriteLine(logMsg);
            Console.WriteLine(vID);
            Console.WriteLine(pID);
            //if (vID.Equals("VID_047D") && pID.Equals("PID_2041")) { e.Mouse.setDevice = 1; mouseHook.checkDevice = true; }
            if (vID.Equals(setVID) && pID.Equals(setPID)) { e.Mouse.setDevice = 1; mouseHook.checkDevice = true; }
            else { e.Mouse.setDevice = 0; mouseHook.checkDevice = false; }
            if (e.Mouse.mButton == 2)
            {
                //keybd_event((byte)VK_BROWSER_BACK, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                //keybd_event((byte)VK_VOLUME_MUTE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }

        }

        private void m_KeyEvent(object sender, InputDevice.KeyControlEventArgs e)
        {
            String logMsg = "Handler - " + e.Keyboard.deviceHandle.ToString() + "\nType - " + e.Keyboard.deviceType + "\nName - " + e.Keyboard.deviceName + "\nDescription - " + e.Keyboard.Name + "\n";
            //Console.WriteLine(logMsg);
        }
        // RAW EVENT 종료


        #region mouse hook event 등록
        public void RegisterMouseEvnet()
        {
            mouseHook.MouseMove += new MouseHook.MouseHookCallback(mouseHook_MouseMove);
            mouseHook.LeftButtonDown += new MouseHook.MouseHookCallback(mouseHook_LeftButtonDown);
            mouseHook.LeftButtonUp += new MouseHook.MouseHookCallback(mouseHook_LeftButtonUp);
            mouseHook.RightButtonDown += new MouseHook.MouseHookCallback(mouseHook_RightButtonDown);
            mouseHook.RightButtonUp += new MouseHook.MouseHookCallback(mouseHook_RightButtonUp);
            mouseHook.MiddleButtonDown += new MouseHook.MouseHookCallback(mouseHook_MiddleButtonDown);
            mouseHook.MiddleButtonUp += new MouseHook.MouseHookCallback(mouseHook_MiddleButtonUp);
            mouseHook.MouseWheel += new MouseHook.MouseHookCallback(mouseHook_MouseWheel);

            //mouseHook.Install();
        }

        void mouseHook_MouseWheel(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MouseWheel Event");
        }

        void mouseHook_MiddleButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MiddleButtonUp Event");
        }

        void mouseHook_MiddleButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MiddleButtonDown Event");
        }

        void mouseHook_RightButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("RightButtonUp Event");
        }

        void mouseHook_RightButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("RightButtonDown Event");
        }

        void mouseHook_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("LeftButtonUp Event");
        }

        void mouseHook_LeftButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("LeftButtonDown Event");
        }

        void mouseHook_MouseMove(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            //Console.WriteLine("X - " + mouseStruct.pt.x.ToString() + "   Y - " + mouseStruct.pt.y.ToString());
        }
        #endregion mouse hook event 등록 종료
    }
}
