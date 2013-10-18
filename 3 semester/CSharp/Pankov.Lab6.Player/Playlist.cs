using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Pankov.Lab6.Player.Tracks;

namespace Pankov.Lab6.Player
{
    [CollectionDataContract]
    public class Playlist : List<Track>, IDisposable
    {
        [DataMember]
        public string Name { get; private set; }

        private static string GetPath(string name)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"\" + name + ".json";
        }

        public static Playlist Load(string name)
        {
            Playlist p;
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(Playlist));
            using (Stream str = File.OpenRead(GetPath(name)))
                p = (Playlist)s.ReadObject(str);
            p.Name = name;
            return p;
        }

        public static Playlist Create(string name)
        {
            Playlist pls = new Playlist();
            pls.Name = name;
            pls.Save();
            return pls;
        }

        public void Save()
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(Playlist));
            using (Stream str = File.OpenWrite(GetPath(Name)))
                s.WriteObject(str, this);
        }

        public void DeleteFile()
        {
            File.Delete(GetPath(Name));
        }

        public void Dispose()
        {
            foreach (Track t in this)
                t.Dispose();
        }
    }
}
