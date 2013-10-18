using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Pankov.Lab2.Quiz.Storage
{
    public class ZipCryptoStorage : BaseQuizStorage
    {
        public byte[] DESKey { get; set; }

        public override void Save(Quiz q)
        {
            FileStream fs = File.OpenWrite(q.Title + ".zip");
            GZipStream gzs = new GZipStream(fs, CompressionMode.Compress, true);
            DES des = DES.Create();
            des.Key = DESKey;
            des.IV = DESKey;
            CryptoStream cs = new CryptoStream(gzs, des.CreateEncryptor(), CryptoStreamMode.Write);
            WalkSave(q, cs);
            cs.Close();
            gzs.Close();
            fs.Close();
        }

        public override Quiz Load(string title)
        {
            FileStream fs = File.OpenRead(title + ".zip");
            GZipStream gzs = new GZipStream(fs, CompressionMode.Decompress, true);
            DES des = DES.Create();
            des.Key = DESKey;
            des.IV = DESKey;
            CryptoStream cs = new CryptoStream(gzs, des.CreateDecryptor(), CryptoStreamMode.Read);

            Quiz q = new Quiz();
            q.Title = title;
            q.Items.AddRange(((QuizItemContainer)WalkLoad(cs)).Items);
            cs.Close();
            gzs.Close();
            fs.Close();

            return q;
        }
    }
}
