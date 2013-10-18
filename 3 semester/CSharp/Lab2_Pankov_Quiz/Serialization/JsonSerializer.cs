using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Pankov.Lab2.Quiz.Serialization
{
    public class JsonSerializer : IQuizSerializer<string>
    {
        public Quiz Deserialize(string data)
        {
            MemoryStream ms = new MemoryStream();
            byte[] d = Encoding.UTF8.GetBytes(data);
            ms.Write(d, 0, d.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return (Quiz)new DataContractJsonSerializer(typeof(Quiz)).ReadObject(ms);
        }

        public string Serialize(Quiz q)
        {
            MemoryStream ms = new MemoryStream();
            new DataContractJsonSerializer(typeof(Quiz)).WriteObject(ms, q);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
