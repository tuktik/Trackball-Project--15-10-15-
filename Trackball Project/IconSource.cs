using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trackball_Project
{
    public class IconSource
    {
        public const int ImageCount = 28;
        public List<Uri> uris = new List<Uri>();

        public IconSource()
        {
            for (var i = 0; i < ImageCount; i++)
            {
                String filePath = @"/Image/icon/" + i.ToString() + ".png";
                uris.Add(new Uri(filePath, UriKind.Relative));
            }
        }
    }
}
