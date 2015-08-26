using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyActivator
{
    class Global
    {
        public const Int32 ServerID = 5;

        public static Version Version 
        {
            get { return new Version("1.2.1"); }
        }
    }
}
