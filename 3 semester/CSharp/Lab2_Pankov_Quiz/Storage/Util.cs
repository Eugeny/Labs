using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pankov.Lab2.Quiz.Storage
{
    public static class Util
    {
        internal static void WriteString(Stream s, string str)
        {
            s.WriteByte((byte)str.Length);
            byte[] buf = Encoding.UTF8.GetBytes(str);
            s.Write(buf, 0, buf.Length);
        }

        internal static string ReadString(Stream s)
        {
            int len = s.ReadByte();
            byte[] buf = new byte[len];
            s.Read(buf, 0, len);
            return Encoding.UTF8.GetString(buf);
        }
    }
}
