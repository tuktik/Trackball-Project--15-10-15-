using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    public class Data
    {
        public String mode;
        public int index;
        public String value;

        public Data(int idx, String val)
        {
            this.index = idx;
            this.value = val;
        }
    }
}
