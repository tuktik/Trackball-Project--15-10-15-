using MahApps.Metro;
using MahApps.Metro.Controls;   // Metro Import
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
using System.Windows.Shapes;

namespace Trackball_Project
{
    /// <summary>
    /// IconWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IconWindow : MetroWindow
    {
        private MainWindow mMainWindow = null;
        private int mainIdx = 0;
        public IconWindow()
        {
            InitializeComponent();
        }
        public IconWindow(MainWindow mMainWindow, int mainIdx)
        {
            InitializeComponent();
            this.mMainWindow = mMainWindow;
            this.mainIdx = mainIdx;
            // 초기 테마 설정
            ThemeManager.ChangeAppStyle(Application.Current, mMainWindow.CurrentAccent, mMainWindow.Light);
            this.FontFamily = mMainWindow.sFont;
        }

        private void ChangeModeImage(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Count : " + mainIdx.ToString());
            Button clickedButton = (Button)sender;
            //Console.WriteLine("clicked name = " + clickedButton.Name);
            for (var i = 0; i < 28; i++)
            {
                String buttonName = "button" + i.ToString();
                //Console.WriteLine("local name = " + buttonName);
                
                if (clickedButton.Name.Equals(buttonName))
                {
                    mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[i];
                    mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[i]);
                    this.Close();
                }
            }

            // 소스가 지저분해서 대체함
            /*
            if (sender == button0) {
                int idx = 0;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button1)
            {
                int idx = 1;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button2)
            {
                int idx = 2;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button3)
            {
                int idx = 3;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button4)
            {
                int idx = 4;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button5)
            {
                int idx = 5;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button6)
            {
                int idx = 6;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button7)
            {
                int idx = 7;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button8)
            {
                int idx = 8;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button9)
            {
                int idx = 9;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button10)
            {
                int idx = 10;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button11)
            {
                int idx = 11;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button12)
            {
                int idx = 12;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button13)
            {
                int idx = 13;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button14)
            {
                int idx = 14;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button15)
            {
                int idx = 15;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button16)
            {
                int idx = 16;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button17)
            {
                int idx = 17;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button18)
            {
                int idx = 18;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button19)
            {
                int idx = 19;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button20)
            {
                int idx = 20;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button21)
            {
                int idx = 21;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button22)
            {
                int idx = 22;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button23)
            {
                int idx = 23;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button24)
            {
                int idx = 24;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button25)
            {
                int idx = 25;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button26)
            {
                int idx = 26;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            else if (sender == button27)
            {
                int idx = 27;
                mMainWindow.mUserSetting[mainIdx].image = mMainWindow.icons.uris[idx];
                mMainWindow.ModeIconImage.Source = new BitmapImage(mMainWindow.icons.uris[idx]);
            }
            this.Close();
            */
        }
    }
}
