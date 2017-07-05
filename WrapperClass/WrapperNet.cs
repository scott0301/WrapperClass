using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WrapperUnion
{
    class WrapperNet
    {
        public static string GetHostName()
        {
            String hostName = Dns.GetHostName();

            return hostName;

        }
    }
}
