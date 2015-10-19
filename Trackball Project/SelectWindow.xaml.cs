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
    /// SelectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectWindow : MetroWindow
    {
        private static MainWindow mMainWindow = null;

        public SelectWindow()
        {
            InitializeComponent();
        }
        public SelectWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mMainWindow = mainWindow;
            // 초기 테마 설정
            ThemeManager.ChangeAppStyle(Application.Current, mMainWindow.CurrentAccent, mMainWindow.Light);
            this.FontFamily = mMainWindow.sFont;

            InitializeModeButtons();
            //ModeList.Items.Add()
        }

        public void InitializeModeButtons()
        {
            int maxIdx = mMainWindow.mUserSetting.Count;
            Thickness border = new Thickness(0);

            for (var i = 0; i < maxIdx; i++)
            {
                // Image 생성
                Image newImage = new Image();
                newImage.Source = new BitmapImage(mMainWindow.mUserSetting[i].image);

                int leftPosition = (30 + (i % 4) * 100);
                int topPosition = (10 + (i / 4) * 100);
                newImage.Width = 60;
                newImage.Height = 60;
                newImage.HorizontalAlignment = HorizontalAlignment.Left;
                newImage.VerticalAlignment = VerticalAlignment.Top;
                newImage.Margin = new Thickness(leftPosition, topPosition, 0, 0);
                Board.Children.Add(newImage);

                //Console.WriteLine("left : " + (i % 4).ToString() + " -> " + leftPosition.ToString());
                //Console.WriteLine("top : " + (i / 4).ToString() + " -> " + topPosition.ToString());
                //Console.WriteLine("(" + leftPosition.ToString() + "," + topPosition.ToString() + ")");
                //Console.WriteLine("----------------------------------------------------------");
                //newGrid.Children.Add(newImage);

                // Button 생성
                Button newButton = new Button();
                newButton.Width = 100;
                newButton.Height = 80;
                leftPosition -= 20;
                //topPosition += 60;
                newButton.HorizontalAlignment = HorizontalAlignment.Left;
                newButton.VerticalAlignment = VerticalAlignment.Top;
                newButton.Margin = new Thickness(leftPosition, topPosition, 0, 0);
                newButton.Content = mMainWindow.mUserSetting[i].name;
                newButton.Click += new RoutedEventHandler(buttonClick);
                newButton.BorderThickness = border;
                newButton.Focusable = false;
                newButton.Opacity = 0;
                Board.Children.Add(newButton);

                // TextBlock 생성
                TextBlock newText = new TextBlock();
                newText.Width = 100;
                newText.Height = 20;
                newText.FontSize = 14;
                newText.TextAlignment = TextAlignment.Center;
                //leftPosition -= 20;
                topPosition += 60;
                newText.HorizontalAlignment = HorizontalAlignment.Left;
                newText.VerticalAlignment = VerticalAlignment.Top;
                newText.Text = mMainWindow.mUserSetting[i].name;
                newText.Margin = new Thickness(leftPosition, topPosition, 0, 0);
                Board.Children.Add(newText);
            }
        }

        // Click Event 함수 구현부
        private void buttonClick(object sender, RoutedEventArgs e)
        {
            int maxIdx = mMainWindow.mUserSetting.Count;
            //Console.WriteLine(sender.ToString());
            Button clickedButton = (Button)sender;
            for (var i = 0; i < maxIdx; i++) {
                String buttonName = mMainWindow.mUserSetting[i].name;
                if (clickedButton.Content.Equals(buttonName))
                    mMainWindow.ModeComboBox.SelectedIndex = i;
            }
            this.Close();
        }        
    }
}
