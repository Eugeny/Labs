using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pankov.Lab2.Quiz
{
    public class FileMonitor : IDisposable
    {
        public string Path { get; set; }
        public event FileChanged Changed = delegate { };
        public event FileRenamed Renamed = delegate { };
        private FileSystemWatcher _watcher;

        public delegate void FileChanged(string path, FileSystemEventArgs e);
        public delegate void FileRenamed(string path, RenamedEventArgs e);


        public FileMonitor(string path)
        {
            Path = path;
            _watcher = new FileSystemWatcher(path);
            _watcher.Changed += new FileSystemEventHandler(_watcher_Event);
            _watcher.Deleted += new FileSystemEventHandler(_watcher_Event);
            _watcher.Renamed += new RenamedEventHandler(_watcher_Renamed);
            _watcher.EnableRaisingEvents = true;
        }

        void _watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Renamed(Path, e);
        }

        void _watcher_Event(object sender, FileSystemEventArgs e)
        {
            Changed(Path, e);
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
