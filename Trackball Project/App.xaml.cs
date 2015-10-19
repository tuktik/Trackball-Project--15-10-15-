using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MahApps.Metro
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/MahAppsMetroThemesSample;component/CustomAccents/CustomAccent1.xaml"));

            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // now use the custom accent
            ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent("CustomAccent1"),
                                    theme.Item1);

            base.OnStartup(e);
        }
    }
}
