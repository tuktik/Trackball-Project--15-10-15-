using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    public class FileStructure
    {
        public int trackball_UP;
        public int trackball_DOWN;
        public int trackball_LEFT;
        public int trackball_RIGHT;
        public int trackball_WHEEL_UP;
        public int trackball_WHEEL_DOWN;
        public int trackball_LCLICK;
        public int trackball_RCLICK;

        string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static MainWindow mMainWindow = null;

        public FileStructure(MainWindow mainWindow)
        {
            mMainWindow = mainWindow;
            trackball_UP = 2;
        }

        public void FileSave()
        {

        }

        public void FileLoad()
        {
            String PID, SID;

        }
    }
}
