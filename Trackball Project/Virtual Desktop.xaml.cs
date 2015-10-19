using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using winforms = System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Threading;
using System.Diagnostics;

using System.Management;
using System.Windows.Media.Effects;
using System.Runtime.InteropServices;


using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace virtual_desktop
{
    /// <summary>
    /// Interaction logic for Virtual_Desktop.xaml
    /// </summary>
    public partial class Virtual_Desktop : Window
    {
        [DllImport("user32.dll")]//SetLastError = true
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        //string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, long dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentProcessId();

        private const int WM_SYSCOMMAND = 274;
        private const int SC_MAXIMIZE = 61488;
        // Private Const SC_MAXIMIZE As Integer = 61488

        private string currentDesktop = "Default";
        public static string Start = string.Empty;

        private int focus = 0;

        public Virtual_Desktop()
        {
            Start = "start";

            InitializeComponent();

            //var blur = new BlurEffect();
            //var current = virtual_window.Background;
            //blur.Radius = 5;
            //virtual_window.Background = new SolidColorBrush(Colors.Blue);
            //virtual_window.Effect = blur;
            ////MessageBox.Show("hello");
            //virtual_window.Effect = null;
            //virtual_window.Background = current;

            windowSizeInit();
          //  System.Windows.SystemParameters.PrimaryScreenWidth;
          //  System.Windows.SystemParameters.PrimaryScreenHeight;
            
        }
                               
        private void windowSizeInit()
        {
            //Window win = System.Windows.Application.Current.MainWindow;

            virtual_window.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            virtual_window.Width = System.Windows.SystemParameters.PrimaryScreenWidth;

            virtual_window.Left = 0;
            virtual_window.Top = 0;

            preview_grid.Height = System.Windows.SystemParameters.PrimaryScreenHeight * 0.8;
            preview_grid.Width = (System.Windows.SystemParameters.PrimaryScreenWidth ) * 4;

            preview_image1.Height = System.Windows.SystemParameters.PrimaryScreenHeight*0.7;
            preview_image1.Width = System.Windows.SystemParameters.PrimaryScreenWidth*0.7;

            //preview_clip
            //win.Topmost = true;
        }

        private System.Windows.Controls.Image ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg)
        {
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = 500;
            img.Height = 600;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            return img;
        }

        private void LoadScreenshots()
        {
            //Console.WriteLine("loadScreens");
            for (int index = 1; index < 5; index++)
            {
                string path = System.IO.Path.GetDirectoryName(winforms.Application.ExecutablePath) + "\\Desktop" + index.ToString(); // Name of the image

                if (File.Exists(path))  // If file exists
                {
                    MemoryStream stream = new MemoryStream();
                    System.Drawing.Image image = Bitmap.FromFile(path);
                    image.Save(stream, ImageFormat.Jpeg);  // Load image file to memory stream
                    image.Dispose();     // Dispose and unlock the file

                    stream.Position = 0;

                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = stream;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                    // Get the PictureBox name and load in it image from memory

                    //Image1.Source = bitmapimage;
                    //Image1.Stretch = Stretch.Uniform;

                    switch (index)
                    {
                        case 2:
                            Image2.Source = bitmapimage;
                            Image2.Stretch = Stretch.Uniform;
                            //ConvertDrawingImageToWPFImage(Bitmap.FromStream(stream));
                            break;
                        case 3:
                            Image3.Source = bitmapimage;
                            Image3.Stretch = Stretch.Uniform;
                            //= ConvertDrawingImageToWPFImage(Bitmap.FromStream(stream));
                            break;
                        case 4:
                            Image4.Source = bitmapimage;
                            Image4.Stretch = Stretch.Uniform;
                            //= ConvertDrawingImageToWPFImage(Bitmap.FromStream(stream));
                            break;
                        default:
                            //default_image.Source = Screenshot.CaptureScreen();
                            Image1.Source = bitmapimage;
                            Image1.Stretch = Stretch.Uniform;

                            //Image1.imagebrush
                            //= ConvertDrawingImageToWPFImage(Bitmap.FromStream(stream));
                            break;
                    }

                }// if
                else
                {
                    switch (index)
                    {
                        case 2:
                            Image2.Source = (ImageSource)new BitmapImage((new Uri("black.jpg", UriKind.Relative)));
                            break;
                        case 3:
                            Image3.Source = (ImageSource)new BitmapImage((new Uri("/black.jpg", UriKind.Relative)));
                            break;
                        case 4:
                            Image4.Source = (ImageSource)new BitmapImage((new Uri("/black.jpg", UriKind.Relative)));
                            break;
                        default:
                            break;
                    }
                }
            }// for
        }

        private void LoadScreenshot()
        {
            switch (currentDesktop)
            {
                case "Default":
                    Image1.Source = Screenshot.CaptureScreen();
                    //default_image = ConvertDrawingImageToWPFImage(Screenshot.CaptureScreen());
                    break;
                case "Desktop2":
                    Image2.Source = Screenshot.CaptureScreen();
                    //Image2 = ConvertDrawingImageToWPFImage(Screenshot.CaptureScreen());
                    break;
                case "Desktop3":
                    Image3.Source = Screenshot.CaptureScreen();
                    //Image3 = ConvertDrawingImageToWPFImage(Screenshot.CaptureScreen());
                    break;
                case "Desktop4":
                    Image4.Source = Screenshot.CaptureScreen();
                    //Image4 = ConvertDrawingImageToWPFImage(Screenshot.CaptureScreen());
                    break;
            }// switch
        }

        private void DesktopInitialize(string name)
        {
            DesktopSave();

            if (!Desktops.DesktopExists(name))
            {
                Console.WriteLine("create" + name);
                Desktops.DesktopCreate(name);
                Desktops.ProcessCreate(name, System.Reflection.Assembly.GetExecutingAssembly().Location, "start");     // Start VirtualDesktop application
                Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);

            }

            Console.WriteLine(name + " desktop Handle ID" + Desktops.get_DesktopHandle(name));
            Desktops.DesktopSwitch(name);
        }

        private void DesktopSave()
        {
            string path = currentDesktop;
            if (path == "Default") { path = "Desktop1"; }
            path = ".\\" + path;
            Thread.Sleep(300);     // Wait to minimize current window
            Screenshot.SaveScreen(path, ImageFormat.Jpeg); // Save file to disk
        }

        void DeleteScreenshots()
        {
            for (int index = 2; index < 5; index++)
            {
                string desktopName = "Desktop" + index.ToString();
                string path = ".\\" + desktopName;

                // If file exists and desktop do not exists, delete the file
                if (File.Exists(path) && !Desktops.DesktopExists(desktopName))
                {
                    File.Delete(path);
                }
            }// for
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Start == "start")
            {
                Process myProcess = new Process();

                myProcess.StartInfo.FileName = "C:\\Windows\\explorer.exe";
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                myProcess.StartInfo.CreateNoWindow = true;


                myProcess.Start();
                Thread.Sleep(500);                                                         // Wait explorer to initialize

                // Reposition window on taskbar   
                this.Opacity = 0;
                this.WindowState = WindowState.Normal;
                this.WindowState = WindowState.Minimized;
                Thread.Sleep(300);
                this.Opacity = 100;
            }



            // Initialize
            currentDesktop = Desktops.DesktopName(Desktops.DesktopOpenInput());
            DeleteScreenshots();

            
            //Console.WriteLine(currentDesktop);
        }

        void window_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Active");

            LoadScreenshots();
            LoadScreenshot();

            // Set form caption
            string caption = currentDesktop;
            if (caption == "Default") { caption = "Desktop1"; }

            set_focus(1);
        }

        void window_Deactivated(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (Desktops.DesktopExists("Desktop2"))
            {
                Desktops.DesktopClose("Desktop2");
            }
            if (Desktops.DesktopExists("Desktop3"))
            {
                Desktops.DesktopClose("Desktop3");
            }
            if (Desktops.DesktopExists("Desktop4"))
            {
                Desktops.DesktopClose("Desktop4");
            }
            this.Close();
        }

       
        void Image1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("image1click");
            this.WindowState = WindowState.Minimized;

            DesktopInitialize("Default");

        }

        void Image2_Click(object sender, EventArgs e)
        {

            //Console.WriteLine("image2click");
            this.WindowState = WindowState.Minimized;

            DesktopInitialize("Desktop2");
        }

        void Image3_Click(object sender, EventArgs e)
        {
            // Start = "start";
            this.WindowState = WindowState.Minimized;

            DesktopInitialize("Desktop3");
        }

        void Image4_Click(object sender, EventArgs e)
        {
            // Start = "start";
            this.WindowState = WindowState.Minimized;

            DesktopInitialize("Desktop4");
        }

        void preview_Click(object sender, EventArgs e)
        {
            Move_to_right();
        }

        public void move_grid()
        {
            Storyboard sb = this.FindResource("move_preview_Storyboard") as Storyboard;
            sb.Begin();
        }
        void Move_to_right()
        {
            set_focus(focus + 1);
            move_grid();
        }

        void Move_to_left()
        {
            set_focus(focus - 1);
        }

        void set_focus(int focus_idx)
        {
            if(focus_idx>4)
            {
                focus = focus_idx- 4;
            }
            else if(focus_idx<=0)
            {
                focus = focus_idx + 4;
            }
            else
            {
                focus = focus_idx;
            }

            switch (focus)
            {
                case 1:
                    preview_image1.Source = Image1.Source;
                    preview_image2.Source = Image2.Source;
                    Image1.Opacity = 100;
                    Image2.Opacity = 30;
                    Image3.Opacity = 30;
                    Image4.Opacity = 30;
                    break;
                case 2:
                    preview_image1.Source = Image1.Source;
                    preview_image2.Source = Image2.Source;
                    preview_image3.Source = Image3.Source;
                    Image1.Opacity = 30;
                    Image2.Opacity = 100;
                    Image3.Opacity = 30;
                    Image4.Opacity = 30;
                    break;
                case 3:
                    preview_image2.Source = Image2.Source;
                    preview_image3.Source = Image3.Source;
                    preview_image4.Source = Image4.Source;
                    Image1.Opacity = 30;
                    Image2.Opacity = 30;
                    Image3.Opacity = 100;
                    Image4.Opacity = 30;
                    break;
                case 4:
                    preview_image2.Source = Image2.Source;
                    preview_image3.Source = Image3.Source;
                    preview_image4.Source = Image4.Source;
                    Image1.Opacity = 30;
                    Image2.Opacity = 30;
                    Image3.Opacity = 30;
                    Image4.Opacity = 100;
                    break;
            }
        }
    }
}
