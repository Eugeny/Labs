using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pankov.Lab6.Player.UI
{
    public class Display : Singleton<Display>
    {
        public const int WIDTH = 80;
        public const int HEIGHT = 50;
        private StringBuilder[] buffer = new StringBuilder[HEIGHT];
        public Thread Updater { get; private set; }

        public Display()
        {
            Console.SetWindowSize(WIDTH, HEIGHT + 1);
            Console.SetBufferSize(WIDTH, HEIGHT + 1);
            Console.CursorVisible = false;

            for (int i = 0; i < HEIGHT; i++)
            {
                buffer[i] = new StringBuilder(80);
                for (int p = 0; p < 80; p++)
                    buffer[i].Append(" ");
            }
            Updater = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    lock (this)
                        Flush();
                    Thread.Sleep(100);
                }
            }));
            Updater.Start();
        }

        public void Flush()
        {
            for (int p = 0; p < HEIGHT; p++)
            {
                Console.SetCursorPosition(0, p);
                Console.Write(buffer[p]);
            }
            Console.SetCursorPosition(0, 0);
        }

        public void Clear(int x, int y, int w)
        {
            for (int i = 0; i < w; i++)
                buffer[y][x + i] = ' ';
        }

        public void Write(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
                buffer[y][x + i] = s[i];
        }
    }
}
