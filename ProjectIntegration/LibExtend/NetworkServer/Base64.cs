using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibExtend.NetworkServer
{
    public static class Base64
    {
        public static string Base64Decoder(this string value)
        {
            byte[] bytes = Encoding.Default.GetBytes("要转换的字符串");
            return Convert.ToBase64String(bytes);
        }
        public static string Base64Encoder(this string value)
        {
            byte[] outputb = Convert.FromBase64String(value);
            return Encoding.Default.GetString(outputb);
        }
    }
}
