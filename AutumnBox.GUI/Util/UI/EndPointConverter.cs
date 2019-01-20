using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutumnBox.GUI.Util.UI
{
    class EndPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var ipStr = values[0] as string;
                var portStr = values[1] as string;
                if (ipStr == "iloveyou") return ipStr;
                var ip = IPAddress.Parse(ipStr);
                var port = ushort.Parse(portStr);
                return new IPEndPoint(ip, port);
            }
            catch
            {
                return false;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
