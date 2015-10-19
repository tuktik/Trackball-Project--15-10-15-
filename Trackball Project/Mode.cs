using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    public class Mode
    {
        public String name = "UserSetting";
        public Uri image = null;
        public int up = 0;
        public int down = 0;
        public int left = 0;
        public int right = 0;
        public int dragUp = 0;
        public int dragDown = 0;
        public int button1 = 0;
        public int button2 = 0;

        public Mode(String modeName, Uri modeImage) {
            this.name = modeName;
            this.image = modeImage;
            this.up = 0;
            this.down = 0;
            this.left = 0;
            this.right = 0;
            this.dragUp = 0;
            this.dragDown = 0;
            this.button1 = 0;
            this.button2 = 0;
        }
    }
}
