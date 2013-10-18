using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Pankov.Lab6.Player.Tracks;

namespace Pankov.Lab6.Player.UI
{
    public class Application : Singleton<Application>
    {
        public delegate void InputStringCallback(string why, string what);
        private string inputtingString = null;
        private string inputtingStringPrompt = null;
        private string inputtingStringBuffer = "";
        private InputStringCallback inputStringCallback;
        private List<Playlist> lists = new List<Playlist>();
        private Playlist currentList = null;
        private Track currentTrack = null;
        private int _blinkCnt = 0;

        public Application()
        {
            foreach (FileInfo f in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)).GetFiles("*.json"))
                lists.Add(Playlist.Load(f.Name.Split('.')[0]));
            if (lists.Count > 0)
                currentList = lists[0];
            if (currentList != null && currentList.Count > 0)
                currentTrack = currentList[0];

            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    lock (Display.Get())
                        Update();
                    Thread.Sleep(100);
                }
            })).Start();

            lock (Display.Get())
            {
                for (int i = 0; i < Display.WIDTH; i++)
                {
                    Display.Get().Write(i, 1, "_");
                    Display.Get().Write(i, Display.HEIGHT - 4, "_");
                }
                Display.Get().Write(0, Display.HEIGHT - 3, "Playlists: [C] create [D] delete [<] previous [>] next [Spc]");
                Display.Get().Write(0, Display.HEIGHT - 2, "Tracks:    [A] add [R] remove [Space] play/pause [Backspace] rewind");
            }
        }

        public void Update()
        {
            Display.Get().Clear(0, Display.HEIGHT - 1, Display.WIDTH);
            if (inputtingString != null)
                Display.Get().Write(0, Display.HEIGHT - 1, inputtingStringPrompt + " > " + inputtingStringBuffer);

            Display.Get().Clear(0, 0, Display.WIDTH);
            string slists = "Playlists: ";
            foreach (Playlist p in lists)
            {
                if (currentList == p)
                    slists += String.Format(">{0}< ", p.Name);
                else
                    slists += String.Format(" {0}  ", p.Name);
            }


            for (int i = 2; i < Display.HEIGHT - 5; i++)
                Display.Get().Clear(0, i, Display.WIDTH);

            if (currentList != null)
                for (int i = 0; i < currentList.Count; i++)
                {
                    Track t = currentList[i];
                    Display.Get().Write(2, 2 + i, t.Artist + " - " + t.Name);
                    if (t == currentTrack)
                        Display.Get().Write(0, 2 + i, ">");

                    Display.Get().Write(Display.WIDTH - 21, 2 + i,
                        FormatTime(t.Position) + " / " + FormatTime(t.Length));
                    if (_blinkCnt < 3 && t.IsPlaying())
                        Display.Get().Write(Display.WIDTH - 23, 2 + i, ">");
                }

            Display.Get().Write(0, 0, slists);
            _blinkCnt = (_blinkCnt + 1) % 6;
        }

        public void InputString(string why, string prompt, InputStringCallback cb)
        {
            inputtingStringPrompt = prompt;
            inputtingStringBuffer = "";
            inputtingString = why;
            inputStringCallback = cb;
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            if (inputtingString != null)
            {
                if (key.KeyChar >= (char)32)
                    inputtingStringBuffer += key.KeyChar;
                if (key.Key == ConsoleKey.Backspace && inputtingStringBuffer.Length > 0)
                    inputtingStringBuffer = inputtingStringBuffer.Substring(0, inputtingStringBuffer.Length - 1);
                if (key.Key == ConsoleKey.Enter)
                {
                    inputStringCallback(inputtingString, inputtingStringBuffer);
                    inputtingString = null;
                }
            }
            else
            {
                if (key.Key == ConsoleKey.C)
                    InputString(INPUT_NEW_PLAYLIST, "Playlist name", new InputStringCallback(OnStringInput));
                if (key.Key == ConsoleKey.LeftArrow)
                    SwitchPlaylist(-1);
                if (key.Key == ConsoleKey.RightArrow)
                    SwitchPlaylist(1);
                if (key.Key == ConsoleKey.D && currentList != null)
                {
                    currentList.DeleteFile();
                    currentList.Dispose();
                    lists.Remove(currentList);
                    SwitchPlaylist(1);
                }
                if (key.Key == ConsoleKey.A && currentList != null)
                    InputString(INPUT_NEW_TRACK, "Track title", new InputStringCallback(OnStringInput));
                if (key.Key == ConsoleKey.UpArrow && currentList != null)
                    SwitchTrack(-1);
                if (key.Key == ConsoleKey.DownArrow && currentList != null)
                    SwitchTrack(1);
                if (key.Key == ConsoleKey.R && currentTrack != null)
                {
                    currentList.Remove(currentTrack);
                    currentTrack.Dispose();
                    SwitchTrack(1);
                }
                if (key.Key == ConsoleKey.Spacebar && currentTrack != null)
                    if (currentTrack.IsPlaying())
                        currentTrack.Pause();
                    else
                        currentTrack.Play();
                if (key.Key == ConsoleKey.Backspace && currentTrack != null)
                    currentTrack.Position = 0;
            }
        }

        private void SwitchPlaylist(int offset)
        {
            if (lists.Count == 0)
            {
                currentList = null;
                return;
            }

            int idx = currentList == null ? 0 : lists.IndexOf(currentList);
            idx = (idx + offset + lists.Count) % lists.Count;
            currentList = lists[idx];

            currentTrack = null;
            if (currentList.Count > 0)
                currentTrack = currentList[0];
        }


        private void SwitchTrack(int offset)
        {
            if (currentList == null || currentList.Count == 0)
            {
                currentTrack = null;
                return;
            }
            int idx = currentTrack == null ? 0 : currentList.IndexOf(currentTrack);
            idx = (idx + offset + currentList.Count) % currentList.Count;
            currentTrack = currentList[idx];
        }

        public const string INPUT_NEW_PLAYLIST = "newlist";
        public const string INPUT_NEW_TRACK = "newtrack";

        public void OnStringInput(string why, string what)
        {
            if (why == INPUT_NEW_PLAYLIST)
            {
                Playlist pls = Playlist.Create(what);
                lists.Add(pls);
            }
            if (why == INPUT_NEW_TRACK)
            {
                Track t = TrackFactory.GetFactory().CreateTrack(what);
                currentList.Add(t);
                currentList.Save();
                currentTrack = t;
            }
        }

        private string FormatTime(int time)
        {
            return String.Format("{0}:{1:00}.{2:000}", time / 60000, time / 1000 % 60, time % 1000);
        }
    }
}
