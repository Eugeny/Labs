using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pankov.Lab2.Quiz.Serialization
{
    public class BinarySerializer : IQuizSerializer<byte[]>
    {
        public Quiz Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return (Quiz)new BinaryFormatter().Deserialize(ms);
        }

        public byte[] Serialize(Quiz q)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, q);
            return ms.ToArray();
        }
    }
}
